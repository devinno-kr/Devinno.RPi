using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Devinno.RPi.Native
{
    public partial class WiringPi
    {
        [DllImport(LIB, EntryPoint = "wiringPiSPIGetFd", SetLastError = true)]                  /**/    public static extern int WiringPiSPIGetFd(int channel);
        [DllImport(LIB, EntryPoint = "wiringPiSPIDataRW", SetLastError = true)]                 /**/    public static extern int WiringPiSPIDataRW(int channel, byte[] data, int len);
        [DllImport(LIB, EntryPoint = "wiringPiSPISetupMode", SetLastError = true)]              /**/    public static extern int WiringPiSPISetupMode(int channel, int speed, int mode);
        [DllImport(LIB, EntryPoint = "wiringPiSPISetup", SetLastError = true)]                  /**/    public static extern int WiringPiSPISetup(int channel, int speed);
    }
}
