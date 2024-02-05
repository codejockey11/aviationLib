using System;

namespace aviationLib
{
    public class Phonetic
    {
        public String PhoneticName(String s)
        {
            Char[] n = s.ToCharArray();

            Char[] r = new Char[s.Length];
            for (Int32 i = 0; i < s.Length; i++)
            {
                r[i] = (Char)0x00;
            }

            Int32 x = 0;

            foreach (Char c in n)
            {
                if ((c >= 'A') && (c <= 'Z'))
                {
                    if ((c != 'A') && (c != 'E') && (c != 'I') && (c != 'O') && (c != 'U'))
                    {
                        r[x] = c;
                        x++;
                    }
                }
            }

            Char[] tc = new Char[1];
            tc[0] = (Char)0x00;
            String rs = new String(r).Trim(tc);
            return rs;
        }
    }
}
