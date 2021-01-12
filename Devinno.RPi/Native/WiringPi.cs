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
        const string LIB = "libwiringPi.so.2.52";
   
        [DllImport(LIB, EntryPoint = "wiringPiSetup", SetLastError = true)]                     /**/    public static extern int WiringPiSetup();
        [DllImport(LIB, EntryPoint = "wiringPiSetupSys", SetLastError = true)]                  /**/    public static extern int WiringPiSetupSys();
        [DllImport(LIB, EntryPoint = "wiringPiSetupGpio", SetLastError = true)]                 /**/    public static extern int WiringPiSetupGpio();
        [DllImport(LIB, EntryPoint = "wiringPiSetupPhys", SetLastError = true)]                 /**/    public static extern int WiringPiSetupPhys();

        [DllImport(LIB, EntryPoint = "pinModeAlt", SetLastError = true)]                        /**/    public static extern void PinModeAlt(int pin, int mode);
        [DllImport(LIB, EntryPoint = "pinMode", SetLastError = true)]                           /**/    public static extern void PinMode(int pin, int mode);
        [DllImport(LIB, EntryPoint = "pullUpDnControl", SetLastError = true)]                   /**/    public static extern void PullUpDnControl(int pin, int pud);
        [DllImport(LIB, EntryPoint = "digitalRead", SetLastError = true)]                       /**/    public static extern int DigitalRead(int pin);
        [DllImport(LIB, EntryPoint = "digitalWrite", SetLastError = true)]                      /**/    public static extern void DigitalWrite(int pin, int value);
        [DllImport(LIB, EntryPoint = "digitalRead8", SetLastError = true)]                      /**/    public static extern uint DigitalRead8();
        [DllImport(LIB, EntryPoint = "digitalWrite8", SetLastError = true)]                     /**/    public static extern void DigitalWrite8(int value);
        [DllImport(LIB, EntryPoint = "pwmWrite", SetLastError = true)]                          /**/    public static extern void PwmWrite(int pin, int value);
        [DllImport(LIB, EntryPoint = "analogRead", SetLastError = true)]                        /**/    public static extern int AnalogRead(int pin);
        [DllImport(LIB, EntryPoint = "analogWrite", SetLastError = true)]                       /**/    public static extern void AnalogWrite(int pin, int value);

        //Deprecated [DllImport(LIB, EntryPoint = "wiringPiSetupPiFace", SetLastError = true)]               /**/    public static extern int wiringPiSetupPiFace();
        //Deprecated [DllImport(LIB, EntryPoint = "wiringPiSetupPiFaceForGpioProg", SetLastError = true)]    /**/    public static extern int wiringPiSetupPiFaceForGpioProg();

        [DllImport(LIB, EntryPoint = "piGpioLayout", SetLastError = true)]                      /**/    public static extern int PiGpioLayout();
        [DllImport(LIB, EntryPoint = "piBoardId", SetLastError = true)]                         /**/    public static extern int PiBoardId(ref int model, ref int mem, ref int maker, ref int overVolted);
        //Deprecated [DllImport(LIB, EntryPoint = "piBoardRev", SetLastError = true)]                        /**/    public static extern int PiBoardRev();
        [DllImport(LIB, EntryPoint = "wpiPinToGpio", SetLastError = true)]                      /**/    public static extern int WpiPinToGpio(int wPiPin);
        [DllImport(LIB, EntryPoint = "physPinToGpio", SetLastError = true)]                     /**/    public static extern int PhysPinToGpio(int physPin);
        [DllImport(LIB, EntryPoint = "setPadDrive", SetLastError = true)]                       /**/    public static extern int SetPadDrive(int group, int value);
        [DllImport(LIB, EntryPoint = "getAlt", SetLastError = true)]                            /**/    public static extern int GetAlt(int pin);
        [DllImport(LIB, EntryPoint = "pwmToneWrite", SetLastError = true)]                      /**/    public static extern int PwmToneWrite(int pin, int freq);
        [DllImport(LIB, EntryPoint = "pwmSetMode", SetLastError = true)]                        /**/    public static extern void PwmSetMode(int mode);
        [DllImport(LIB, EntryPoint = "pwmSetRange", SetLastError = true)]                       /**/    public static extern void PwmSetRange(uint range);
        [DllImport(LIB, EntryPoint = "pwmSetClock", SetLastError = true)]                       /**/    public static extern void PwmSetClock(int divisor);
        [DllImport(LIB, EntryPoint = "gpioClockSet", SetLastError = true)]                      /**/    public static extern void GpioClockSet(int pin, int freq);
        [DllImport(LIB, EntryPoint = "digitalReadByte", SetLastError = true)]                   /**/    public static extern uint DigitalReadByte();
        [DllImport(LIB, EntryPoint = "digitalReadByte2", SetLastError = true)]                  /**/    public static extern uint DigitalReadByte2();
        [DllImport(LIB, EntryPoint = "digitalWriteByte", SetLastError = true)]                  /**/    public static extern void DigitalWriteByte(int value);
        [DllImport(LIB, EntryPoint = "digitalWriteByte2", SetLastError = true)]                 /**/    public static extern void DigitalWriteByte2(int value);

        //Deprecated [DllImport(LIB, EntryPoint = "waitForInterrupt", SetLastError = true)]                  /**/    public static extern int WaitForInterrupt(int pin, int mS);
        [DllImport(LIB, EntryPoint = "wiringPiISR", SetLastError = true)]                       /**/    public static extern int WiringPiISR(int pin, int mode, Action method);

        [DllImport(LIB, EntryPoint = "piThreadCreate", SetLastError = true)]                    /**/    public static extern int PiThreadCreate(Action method);
        [DllImport(LIB, EntryPoint = "piLock", SetLastError = true)]                            /**/    public static extern void PiLock(int key);
        [DllImport(LIB, EntryPoint = "piUnlock", SetLastError = true)]                          /**/    public static extern void PiUnlock(int key);

        [DllImport(LIB, EntryPoint = "piHiPri", SetLastError = true)]                           /**/    public static extern int PiHiPri(int priority);

        [DllImport(LIB, EntryPoint = "delay", SetLastError = true)]                             /**/    public static extern void Delay(uint howLong);
        [DllImport(LIB, EntryPoint = "delayMicroseconds", SetLastError = true)]                 /**/    public static extern void DelayMicroseconds(uint howLong);
        [DllImport(LIB, EntryPoint = "millis", SetLastError = true)]                            /**/    public static extern uint Millis();
        [DllImport(LIB, EntryPoint = "micros", SetLastError = true)]                            /**/    public static extern uint Micros();

    }
}
