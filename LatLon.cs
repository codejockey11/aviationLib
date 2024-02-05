// http://edwilliams.org/avform.htm

using System;

namespace aviationLib
{
    public class LatLon
    {
        public String formattedLat { get; set; }
        public String formattedLatFSX { get; set; }
        public Double decimalLat { get; set; }
        public Char hemiLat { get; set; }

        public String formattedLon { get; set; }
        public String formattedLonFSX { get; set; }
        public Double decimalLon { get; set; }
        public Char hemiLon { get; set; }

        public LatLon(String lat, String lon)
        {
            String t = "";

            if ((lat.IndexOf(".") < 5) && (lon.IndexOf(".") > 0))
            {
			    t = "N";
                if (Convert.ToDouble(lat) < 0)
			    {
				    t = "S";
                }

			    //get absolute value of decimal
			    Double d = Math.Abs(Convert.ToDouble(lat));

                //get degrees
                Double degrees = Math.Floor(d);

                //get seconds
                Double seconds = (d - degrees) *3600;

                //get minutes
                Double minutes = Math.Floor(seconds / 60);

			    //reset seconds
			    seconds = seconds - (minutes * 60);

			    formattedLat = degrees.ToString("F0").Trim() + "-" + minutes.ToString("F0").Trim() + "-" + seconds.ToString("F4").Trim() + t;
            }
            else
            {
    			formattedLat = lat;
            }

            if ((lon.IndexOf(".") < 5) && (lon.IndexOf(".") > 0))
            {
                t = "E";
                if (Convert.ToDouble(lon) < 0)
                {
                    t = "W";
                }

                //get absolute value of decimal
                Double d = Math.Abs(Convert.ToDouble(lon));

                //get degrees
                Double degrees = Math.Floor(d);

                //get seconds
                Double seconds = (d - degrees) * 3600;

                //get minutes
                Double minutes = Math.Floor(seconds / 60);

                //reset seconds
                seconds = seconds - (minutes * 60);

                formattedLon = degrees.ToString("F0").Trim() + "-" + minutes.ToString("F0").Trim() + "-" + seconds.ToString("F4").Trim() + t;
            }
            else
            {
                formattedLon = lon;
            }

            String[] partsLat = formattedLat.Split('-');

            String a = partsLat[2];
            hemiLat = a[a.Length - 1];
            partsLat[2] = a;
            String s = partsLat[2].Replace('N', ' ');
            partsLat[2] = s.Replace('S', ' ');

            formattedLatFSX = hemiLat + partsLat[0] + " " + partsLat[1] + " " + partsLat[2];
            s = formattedLatFSX.TrimEnd(' ');
            formattedLatFSX = s;

            String fls = String.Format("{0:00.0000}", Convert.ToDouble(partsLat[2]));

            formattedLat = Convert.ToInt32(partsLat[0]).ToString("D2") + "-" +
                        Convert.ToInt32(partsLat[1]).ToString("D2") + "-" +
                        fls + hemiLat;

            //N + S - W - E +
            //Decimal Degrees = Degrees + minutes/60 + seconds/3600    
            decimalLat = Convert.ToDouble(partsLat[0]) + (Convert.ToDouble(partsLat[1]) / 60) + (Convert.ToDouble(partsLat[2]) / 3600);

            if (hemiLat == 'S' || hemiLat == 'W')
            {
                decimalLat *= -1;
            }


            String[] partsLon = formattedLon.Split('-');

            a = partsLon[2];
            hemiLon = a[a.Length - 1];
            partsLon[2] = a;
            s = partsLon[2].Replace('W', ' ');
            partsLon[2] = s.Replace('E', ' ');

            formattedLonFSX = hemiLon + partsLon[0] + " " + partsLon[1] + " " + partsLon[2];
            s = formattedLonFSX.TrimEnd(' ');
            formattedLonFSX = s;

            fls = String.Format("{0:00.0000}", Convert.ToDouble(partsLon[2]));

            formattedLon = Convert.ToInt32(partsLon[0]).ToString("D3") + "-" +
                        Convert.ToInt32(partsLon[1]).ToString("D2") + "-" +
                        fls + hemiLon;

            decimalLon = Convert.ToDouble(partsLon[0]) + (Convert.ToDouble(partsLon[1]) / 60) + (Convert.ToDouble(partsLon[2]) / 3600);

            if (hemiLon == 'S' || hemiLon == 'W')
            {
                decimalLon *= -1;
            }

        }

        public Double DistanceInMiles(LatLon toLatLon)
        {
            Double lat2 = toLatLon.decimalLat * Math.PI / 180;
            Double lon2 = toLatLon.decimalLon * Math.PI / 180;

            Double lat1 = decimalLat * Math.PI / 180;
            Double lon1 = decimalLon * Math.PI / 180;

            Double d = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon1 - lon2));

            Double distance_nm = ((180 * 60) / Math.PI) * d;

            return distance_nm;
        }

        public Double TrueCourse(LatLon toLatLon)
        {
            Double d = DistanceInMiles(toLatLon);

            Double LatA = decimalLat * (Math.PI / 180);
            Double LatB = toLatLon.decimalLat * (Math.PI / 180);
            Double LonA = decimalLon * (Math.PI / 180);
            Double LonB = toLatLon.decimalLon * (Math.PI / 180);

            Double y = (Math.Sin(LonB - LonA) * Math.Cos(LatB));
            Double x = (Math.Cos(LatA) * Math.Sin(LatB) - Math.Sin(LatA) * Math.Cos(LatB) * Math.Cos(LonB - LonA));
            Double b = (Math.Atan2(y, x));

            Double h = b * (180 / Math.PI);

            Double tc = h;

            if (h < 0)
            {
                tc = h + 360;
            }

            return tc;
        }

        public LatLon PointFromHeadingDistance(Double distance, Double bearing)
        {
		    // radius of earth in miles 7,926.41 miles
		    Double radius = (7926.41 / 2);

            // assuming input is in nm
            // distance must be in statute miles (6076/5280) = 1.15
            Double d = ((distance * (6076 / 5280)) / radius);

            Double b = (bearing) * (Math.PI / 180);

            Double lat1 = this.decimalLat * (Math.PI / 180);
            Double lon1 = this.decimalLon * (Math.PI / 180);

            Double sinlat1 = Math.Sin(lat1);
            Double coslat1 = Math.Cos(lat1);

            Double sind = Math.Sin(d);
            Double cosd = Math.Cos(d);

            Double sinb = Math.Sin(b);
            Double cosb = Math.Cos(b);

            Double sinlat2 = sinlat1 * cosd + coslat1 * sind * cosb;
            Double lat2 = Math.Asin(sinlat2);

            Double y = sinb * sind * coslat1;
            Double x = cosd - sinlat1 * sinlat2;

            Double lon2 = lon1 + Math.Atan2(y, x);

            lat2 = lat2 * (180 / Math.PI);
            lon2 = lon2 * (180 / Math.PI) + 540 % 360 - 180;

            return new LatLon(lat2.ToString("F10"), lon2.ToString("F10"));
        }

        public LatLon IntersectingRadials(LatLon p1, Double crs13, LatLon p2, Double crs23)
        {
            Double crs12 = 0;
            Double crs21 = 0;

            Double dst12 = 2 * Math.Asin(Math.Pow(Math.Sqrt(Math.Sin((p1.decimalLat - p2.decimalLat) / 2)), 2) +
                   Math.Pow(Math.Cos(p1.decimalLat) * Math.Cos(p2.decimalLat) * Math.Sin((p1.decimalLon - p2.decimalLon) / 2), 2));

            if (Math.Sin(p2.decimalLon - p1.decimalLon) < 0)
            {
                crs12 = Math.Acos((Math.Sin(p2.decimalLat) - Math.Sin(p1.decimalLat) * Math.Cos(dst12)) / (Math.Sin(dst12) * Math.Cos(p1.decimalLat)));
                crs21 = 2 * Math.PI - Math.Acos((Math.Sin(p1.decimalLat) - Math.Sin(p2.decimalLat) * Math.Cos(dst12)) / (Math.Sin(dst12) * Math.Cos(p2.decimalLat)));
            }
            else
            {
                crs12 = 2 * Math.PI - Math.Acos((Math.Sin(p2.decimalLat) - Math.Sin(p1.decimalLat) * Math.Cos(dst12)) / (Math.Sin(dst12) * Math.Cos(p1.decimalLat)));
                crs21 = Math.Acos((Math.Sin(p1.decimalLat) - Math.Sin(p1.decimalLat) * Math.Cos(dst12)) / (Math.Sin(dst12) * Math.Cos(p1.decimalLat)));
            }

            Double ang1 = ((crs13 - crs12 + Math.PI) % (2 * Math.PI)) - Math.PI;
            Double ang2 = ((crs21 - crs23 + Math.PI) % (2 * Math.PI)) - Math.PI;

            if ((Math.Sin(ang1) == 0) && (Math.Sin(ang2) == 0))
            {
                // "infinity of intersections"
                return null;
            }
            else if ((Math.Sin(ang1) * Math.Sin(ang2)) < 0)
            {
                // "intersection ambiguous"
                return null;
            }

            ang1 = Math.Abs(ang1);
            ang2 = Math.Abs(ang2);

            Double ang3 = Math.Acos(-Math.Cos(ang1) * Math.Cos(ang2) + Math.Sin(ang1) * Math.Sin(ang2) * Math.Cos(dst12));

            Double dst13 = Math.Atan2(Math.Sin(dst12) * Math.Sin(ang1) * Math.Sin(ang2), Math.Cos(ang2) + Math.Cos(ang1) * Math.Cos(ang3));

            Double lat3 = Math.Asin(Math.Sin(p1.decimalLat) * Math.Cos(dst13) + Math.Cos(p1.decimalLat) * Math.Sin(dst13) * Math.Cos(crs13));

            Double dlon = Math.Atan2(Math.Sin(crs13) * Math.Sin(dst13) * Math.Cos(p1.decimalLat), Math.Cos(dst13) - Math.Sin(p1.decimalLat) * Math.Sin(lat3));

            Double lon3 = ((p1.decimalLon - dlon + Math.PI) % (2 * Math.PI)) - Math.PI;

            return new LatLon(lat3.ToString("F10"), lon3.ToString("F10"));

        
/*
http://edwilliams.org/avform.htm#Intro

Intersecting radials 
Now how to compute the latitude, lat3, and longitude, lon3 of an intersection formed by the crs13 true bearing from point 1 and the crs23 true bearing from point 2: 
dst12=2*asin(sqrt((sin((lat1-lat2)/2))^2+
                   cos(lat1)*cos(lat2)*sin((lon1-lon2)/2)^2))
IF sin(lon2-lon1)<0
   crs12=acos((sin(lat2)-sin(lat1)*cos(dst12))/(sin(dst12)*cos(lat1)))
   crs21=2.*pi-acos((sin(lat1)-sin(lat2)*cos(dst12))/(sin(dst12)*cos(lat2)))
ELSE
   crs12=2.*pi-acos((sin(lat2)-sin(lat1)*cos(dst12))/(sin(dst12)*cos(lat1)))
   crs21=acos((sin(lat1)-sin(lat2)*cos(dst12))/(sin(dst12)*cos(lat2)))
ENDIF

ang1=mod(crs13-crs12+pi,2.*pi)-pi
ang2=mod(crs21-crs23+pi,2.*pi)-pi

IF (sin(ang1)=0 AND sin(ang2)=0)
   "infinity of intersections"
ELSEIF sin(ang1)*sin(ang2)<0
   "intersection ambiguous"
ELSE
   ang1=abs(ang1)
   ang2=abs(ang2)
   ang3=acos(-cos(ang1)*cos(ang2)+sin(ang1)*sin(ang2)*cos(dst12)) 
   dst13=atan2(sin(dst12)*sin(ang1)*sin(ang2),cos(ang2)+cos(ang1)*cos(ang3))
   lat3=asin(sin(lat1)*cos(dst13)+cos(lat1)*sin(dst13)*cos(crs13))
   dlon=atan2(sin(crs13)*sin(dst13)*cos(lat1),cos(dst13)-sin(lat1)*sin(lat3))
   lon3=mod(lon1-dlon+pi,2*pi)-pi
ENDIF
The points 1,2 and the (if unique) intersection 3 form a spherical triangle with interior angles abs(ang1), abs(ang2) and ang3. To find the pair of antipodal intersections of two great circles uses the following reference.

*/
        }
    }
}
