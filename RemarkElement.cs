using System;
using System.Text;

namespace aviationLib
{
    public class RemarkElement
    {
        public String en;

        public RemarkElement(String s)
        {
            en = s;
        }

        public String ElementName()
        {
            Int32 nbr;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            Char[] c;
            Boolean r;
            String s;
            String final;

            String[] ena = en.Split('-');

            // no hyphen
            if(ena.Length < 2)
            {
                c = en.ToCharArray();
                sb.Append(c[0]);

                for (Int32 i = 1; i < c.Length; i++)
                {
                    sb2.Append(c[i]);
                }
                
                r = int.TryParse(sb2.ToString(), out nbr);
                if (r)
                {
                    s = Convert.ToInt32(nbr).ToString("D3");
                    sb.Append(s);
                }

                return sb.ToString();
            }

            c = ena[0].ToCharArray();
            sb.Append(c[0]);

            for(Int32 i =1;i < c.Length;i++)
            {
                sb2.Append(c[i]);
            }
            r = int.TryParse(sb2.ToString(), out nbr);
            if (r)
            {
                s = Convert.ToInt32(nbr).ToString("D3");
                sb.Append(s);
            }
            else
            {
                sb.Append(sb2.ToString());
            }

            sb.Append("-");

            r = int.TryParse(ena[1], out nbr);
            if (r)
            {
                s = Convert.ToInt32(nbr).ToString("D3");
                sb.Append(s);
            }
            else
            {
                sb.Append(ena[1]);

                return sb.ToString();
            }

            final = sb.ToString();

            return final;
        }
    }
}
