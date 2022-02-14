namespace pvz_goty_tools
{
    public class Sun
    {
        private static readonly int firstOffsetAddr = 0x868;
        private static readonly int secondOffsetAddr = 0x5578;


        private static int ReadMemoryValue(int addr)
        {
            return MemoryUtil.ReadMemoryValue(addr, GlobeData.windowTitle);
        }

        private static bool WriteMemoryValue(int addr, int value)
        {
            return MemoryUtil.WriteMemoryValue(addr, GlobeData.windowTitle, value);
        }

        public static int GetSunValue()
        {
            int addr = ReadMemoryValue(GlobeData.baseAddr);
            addr = ReadMemoryValue(addr + firstOffsetAddr);
            addr = ReadMemoryValue(addr + secondOffsetAddr);
            return addr;
        }

        public static bool WriteSunValue(int value)
        {
            int addr = ReadMemoryValue(GlobeData.baseAddr);
            addr = ReadMemoryValue(addr + firstOffsetAddr);
            if (WriteMemoryValue(addr + secondOffsetAddr, value))
            {
                return true;
            }
            return false;
        }
    }
}
