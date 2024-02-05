using System;
using System.Text;

namespace aviationLib
{
    public class OneSpace
    {
        public String MakeOneSpace(String s)
        {
            Char[] rsa = s.Trim().ToCharArray();
            StringBuilder rsr = new StringBuilder();
            Boolean needSpace = false;

            foreach (Char c in rsa)
            {
                if (((byte)c > 32) && ((byte)c < 127))
                {
                    rsr.Append(c);
                    needSpace = true;
                }
                else if (needSpace == true)
                {
                    rsr.Append(" ");
                    needSpace = false;
                }
            }

            return rsr.ToString();
        }
    }
}
