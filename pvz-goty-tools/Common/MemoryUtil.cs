using System;
using System.Runtime.InteropServices;

namespace pvz_goty_tools
{
    internal class MemoryUtil
    {
        /// <summary>
        /// 打开进程，返回进程句柄
        /// </summary>
        /// <param name="dwDesiredAccess">渴望得到的访问权限(标志)，0x1F0FFF表示最高权限</param>
        /// <param name="bInheritHandle">是否继承句柄</param>
        /// <param name="dwProcessId">进程标示符</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        /// <summary>
        /// 读内存
        /// </summary>
        /// <param name="hProcess">远程进程句柄。 被读取者</param>
        /// <param name="lpBaseAddress">远程进程中内存地址。 从具体何处读取</param>
        /// <param name="lpBuffer">本地进程中内存地址. 函数将读取的内容写入此处 </param>
        /// <param name="nSize">要传送的字节数。要写入多少</param>
        /// <param name="lpNumberOfBytesRead">实际传送的字节数. 函数返回时报告实际写入多少</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);


        /// <summary>
        /// 写内存
        /// </summary>
        /// <param name="hProcess">由OpenProcess返回的进程句柄</param>
        /// <param name="lpBaseAddress">要写的内存首地址</param>
        /// <param name="lpBuffer">指向要写的数据的指针</param>
        /// <param name="nSize">要写入的字节数</param>
        /// <param name="lpNumberOfBytesWritten">实际数据的长度</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, int[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        /// <summary>
        /// 关闭内核对象
        /// </summary>
        /// <param name="hObject">欲关闭的对象句柄</param>
        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);

        /// <summary>
        /// 得到窗口句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 得到窗口进程 PID
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        private static extern int GetWindowThreadProcessId(IntPtr hwnd, out int pid);

        /// <summary>
        /// 根据窗口标题获取进程 PID
        /// </summary>
        /// <param name="windowsTitle"></param>
        /// <returns></returns>
        private static int GetPidByWindowTitle(string windowTitle)
        {
            GetWindowThreadProcessId(FindWindow(null, windowTitle), out int pid);
            return pid;
        }

        /// <summary>
        /// 读取指定内存的值
        /// </summary>
        /// <param name="address">基值</param>
        /// <param name="processName">进程名</param>
        /// <returns></returns>
        public static int ReadMemoryValue(int address, string windowTitle)
        {
            byte[] buffer = new byte[4];
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByWindowTitle(windowTitle));
            ReadProcessMemory(hProcess, (IntPtr)address, byteAddress, 4, IntPtr.Zero);
            CloseHandle(hProcess);
            return Marshal.ReadInt32(byteAddress);
        }

        /// <summary>
        /// 向指定的内存写入值
        /// </summary>
        /// <param name="address">基址</param>
        /// <param name="processName">进程名</param>
        /// <param name="value">要写入的值</param>
        public static bool WriteMemoryValue(int address, string windowTitle, int value)
        {
            IntPtr hProcess = OpenProcess(0x1F0FFF, false, GetPidByWindowTitle(windowTitle)); //0x1F0FFF 最高权限
            bool a = WriteProcessMemory(hProcess, (IntPtr)address, new int[] { value }, 4, IntPtr.Zero);
            CloseHandle(hProcess);
            return a;
        }

    }
}
