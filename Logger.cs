using System;
using System.IO;

namespace aviationLib
{
    public class Logger
    {
        StreamWriter log;

        public Logger(String logname)
        {
            log = new StreamWriter(logname);
        }

        public void WriteLine(String s)
        {
            log.WriteLine(s);
            log.Flush();
        }

        public void Write(String s)
        {
            log.Write(s);
            log.Flush();
        }

    }
}
