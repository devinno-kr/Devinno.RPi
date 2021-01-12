using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.RPi.GPIO
{
    public enum PullMode { Off = 0, PullDown = 1, PullUp = 2 }
    public enum PinMode { Input = 0, Output = 1, PwmOutput = 2, GpioClock = 3 }
    public enum Edge { Falling = 21, Rising = 1, Both = 3 }
    
    [Flags]
    public enum PinCapability
    {
        GP = 0x01, GPCLK = 0x02,
        I2CSDA = 0x04, I2CSCL = 0x08,
        SPIMOSI = 0x10, SPIMISO = 0x20, SPICLK = 0x40, SPICS = 0x80,
        UARTRTS = 0x100, UARTTXD = 0x200, UARTRXD = 0x400,
        PWM = 0x800,
    }

    public enum PwmMode { MarkSign = 0, Balanced = 1, }

    public enum PiModel
    {
        PI_MODEL_A = 0,
        PI_MODEL_B = 1,
        PI_MODEL_AP = 2,
        PI_MODEL_BP = 3,
        PI_MODEL_2 = 4,
        PI_ALPHA = 5,
        PI_MODEL_CM = 6,
        PI_MODEL_07 = 7,
        PI_MODEL_3B = 8,
        PI_MODEL_ZERO = 9,
        PI_MODEL_CM3 = 10,
        PI_MODEL_ZERO_W = 12,
        PI_MODEL_3BP = 13,
        PI_MODEL_3AP = 14,
        PI_MODEL_CM3P = 16,
        PI_MODEL_4B = 17,
        PI_MODEL_400 = 19,
        PI_MODEL_CM4 = 20,
    }
}
