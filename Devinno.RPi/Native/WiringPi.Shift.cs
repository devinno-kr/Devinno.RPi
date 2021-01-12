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
        [DllImport(LIB, EntryPoint = "shiftIn", SetLastError = true)]                           /**/    public static extern byte ShiftIn(byte dPin, byte cPin, byte order);
        [DllImport(LIB, EntryPoint = "shiftOut", SetLastError = true)]                          /**/    public static extern void ShiftOut(byte dPin, byte cPin, byte order, byte val);
    }
}
