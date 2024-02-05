using System;
using System.Text;

namespace aviationLib
{
    public class MagVar
    {
        public Double magVar;
        
        public MagVar(String mvi)
        {
            magVar = 0.0;
            
            if (String.Compare(mvi, "") != 0)
            {
                Char[] mva = mvi.ToCharArray();
                StringBuilder sb = new StringBuilder();
                sb.Append(mva[0]);
                sb.Append(mva[1]);
                magVar = Convert.ToDouble(sb.ToString());
                if (mva[2] == 'W')
                {
                    magVar *= -1;
                }
            }

        }
    }
}
