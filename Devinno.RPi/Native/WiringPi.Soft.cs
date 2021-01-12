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
        [DllImport(LIB, EntryPoint = "softPwmCreate", SetLastError = true)]     /**/    public static extern int SoftPwmCreate(int pin, int value, int range);
        [DllImport(LIB, EntryPoint = "softPwmWrite", SetLastError = true)]      /**/    public static extern void SoftPwmWrite(int pin, int value);
        [DllImport(LIB, EntryPoint = "softPwmStop", SetLastError = true)]       /**/    public static extern void SoftPwmStop(int pin);

        [DllImport(LIB, EntryPoint = "softToneCreate", SetLastError = true)]    /**/    public static extern int SoftToneCreate(int pin);
        [DllImport(LIB, EntryPoint = "softToneStop", SetLastError = true)]      /**/    public static extern void SoftToneStop(int pin);
        [DllImport(LIB, EntryPoint = "softToneWrite", SetLastError = true)]     /**/    public static extern void SoftToneWrite(int pin, int freq);

        [DllImport(LIB, EntryPoint = "softServoWrite", SetLastError = true)]    /**/    public static extern void SoftServoWrite(int pin);
        [DllImport(LIB, EntryPoint = "softServoSetup", SetLastError = true)]    /**/    public static extern void SoftServoSetup(int p0, int p1, int p2, int p3, int p4, int p5, int p6, int p7);
    }
}
