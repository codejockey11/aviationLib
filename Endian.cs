using System;

namespace aviationLib
{
    public class Endian
    {
        public const Byte Little = 0;
        public const Byte Big = 1;

        private Byte[] result = { 0x00, 0x00, 0x00, 0x00 };

        private Byte machineType = 0;

        public Endian()
        {
            int i = 1;

            Byte[] bytes = BitConverter.GetBytes(i);

            this.machineType = Little;

            if (bytes[0] == 0x00)
            {
                this.machineType = Big;
            }
        }

        public Byte GetMachineType()
        {
            return this.machineType;
        }

        public Byte[] ConvertLittleToBig(int n)
        {
            Byte[] bytes = BitConverter.GetBytes(n);

            int y = 3;

            for (int x = 0; x < 4; x++)
            {
                this.result[y] = bytes[x];
                y--;
            }

            return this.result;
        }

        public Byte[] ConvertLittleToBig(Byte[] bytes)
        {
            int y = 3;

            for (int x = 0; x < 4; x++)
            {
                this.result[y] = bytes[x];
                y--;
            }

            return this.result;
        }

        public Byte[] ConvertBigToLittle(int n)
        {
            Byte[] bytes = BitConverter.GetBytes(n);

            int y = 3;

            for (int x = 0; x < 4; x++)
            {
                this.result[y] = bytes[x];
                y--;
            }

            return this.result;
        }

        public Byte[] ConvertBigToLittle(Byte[] bytes)
        {
            int y = 3;

            for (int x = 0; x < 4; x++)
            {
                this.result[y] = bytes[x];
                y--;
            }

            return this.result;
        }
    }
}
