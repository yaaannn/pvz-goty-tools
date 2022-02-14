using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvz_goty_tools
{
    internal class MemoryUtilTest
    {
        private static string windowTitle = "Plants vs. Zombies";
        public static int ReadMemoryValue(int baseAdd)
        {
            return MemoryUtil.ReadMemoryValue(baseAdd,windowTitle);
        }

        public static void WriteMemoryValue(int baseAdd,int value)
        {
            MemoryUtil.WriteMemoryValue(baseAdd,windowTitle,value);
        } 
        public static void Main()
        {
            
                int baseAddress = 0x00731C50;
                int firstOffsetAddress = 0x868;
                int secondOffsetAddress = 0x5578;

            int address = ReadMemoryValue(baseAddress);
            address += 0x868;
            address = ReadMemoryValue(address);
            address += 0x5578;
            //WriteMemoryValue(address, 60);
            address = ReadMemoryValue(address);

            Console.WriteLine(address);

        }
    }
}
