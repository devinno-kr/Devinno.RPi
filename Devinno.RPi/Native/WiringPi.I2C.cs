using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Devinno.RPi.Native
{
    public partial class WiringPi
    {
        [DllImport(LIB, EntryPoint = "wiringPiI2CRead", SetLastError = true)]                   /**/    public static extern int WiringPiI2CRead(int fd);
        [DllImport(LIB, EntryPoint = "wiringPiI2CReadReg8", SetLastError = true)]               /**/    public static extern int WiringPiI2CReadReg8(int fd, int reg);
        [DllImport(LIB, EntryPoint = "wiringPiI2CReadReg16", SetLastError = true)]              /**/    public static extern int WiringPiI2CReadReg16(int fd, int reg);
        [DllImport(LIB, EntryPoint = "wiringPiI2CWrite", SetLastError = true)]                  /**/    public static extern int WiringPiI2CWrite(int fd, int data);
        [DllImport(LIB, EntryPoint = "wiringPiI2CWriteReg8", SetLastError = true)]              /**/    public static extern int WiringPiI2CWriteReg8(int fd, int reg, int data);
        [DllImport(LIB, EntryPoint = "wiringPiI2CWriteReg16", SetLastError = true)]             /**/    public static extern int WiringPiI2CWriteReg16(int fd, int reg, int data);
        [DllImport(LIB, EntryPoint = "wiringPiI2CSetup", SetLastError = true)]                  /**/    public static extern int WiringPiI2CSetup(int devId);
        //[DllImport(LIB, EntryPoint = "wiringPiI2CSetupInterface", SetLastError = true)]         /**/    public static extern int wiringPiI2CSetupInterface(string device, int devId);
    }
}
