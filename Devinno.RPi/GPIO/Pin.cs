using Devinno.RPi.Native;
using System;
using System.Collections.Generic;
using System.Text;

namespace Devinno.RPi.GPIO
{
    #region class : PIN
    public class Pin
    {
        #region Properties
        public int PinNumber { get; private set; }
        public int GpioNumber { get; private set; }
        public PinCapability Capabilities { get; private set; }
        public PinMode? Mode { get; private set; }
        #endregion

        #region Constructor
        internal Pin(int PinNumber)
        {
            this.PinNumber = PinNumber;
            this.GpioNumber = WiringPi.PhysPinToGpio(PinNumber);
            #region this.Capability
            switch (this.GpioNumber)
            {
                case 0: this.Capabilities = PinCapability.GP | PinCapability.I2CSDA; break;
                case 1: this.Capabilities = PinCapability.GP | PinCapability.I2CSCL; break;
                case 2: this.Capabilities = PinCapability.GP | PinCapability.I2CSDA; break;
                case 3: this.Capabilities = PinCapability.GP | PinCapability.I2CSCL; break;
                case 4: this.Capabilities = PinCapability.GP | PinCapability.GPCLK; break;
                case 5: this.Capabilities = PinCapability.GP; break;
                case 6: this.Capabilities = PinCapability.GP; break;
                case 7: this.Capabilities = PinCapability.GP | PinCapability.SPICS; break;
                case 8: this.Capabilities = PinCapability.GP | PinCapability.SPICS; break;
                case 9: this.Capabilities = PinCapability.GP | PinCapability.SPIMISO; break;
                case 10: this.Capabilities = PinCapability.GP | PinCapability.SPIMOSI; break;
                case 11: this.Capabilities = PinCapability.GP | PinCapability.SPICLK; break;
                case 12: this.Capabilities = PinCapability.GP | PinCapability.PWM; break;
                case 13: this.Capabilities = PinCapability.GP | PinCapability.PWM; break;
                case 14: this.Capabilities = PinCapability.UARTTXD; break;
                case 15: this.Capabilities = PinCapability.UARTRXD; break;
                case 16: this.Capabilities = PinCapability.GP; break;
                case 17: this.Capabilities = PinCapability.GP | PinCapability.UARTRTS; break;
                case 18: this.Capabilities = PinCapability.GP | PinCapability.PWM; break;
                case 19: this.Capabilities = PinCapability.GP | PinCapability.PWM | PinCapability.SPIMISO; break;
                case 20: this.Capabilities = PinCapability.GP | PinCapability.SPIMOSI; break;
                case 21: this.Capabilities = PinCapability.GP | PinCapability.SPICLK; break;
                case 22: this.Capabilities = PinCapability.GP; break;
                case 23: this.Capabilities = PinCapability.GP; break;
                case 24: this.Capabilities = PinCapability.GP; break;
                case 25: this.Capabilities = PinCapability.GP; break;
                case 26: this.Capabilities = PinCapability.GP; break;
                case 27: this.Capabilities = PinCapability.GP; break;
                case 28: this.Capabilities = PinCapability.GP | PinCapability.I2CSDA; break;
                case 29: this.Capabilities = PinCapability.GP | PinCapability.I2CSCL; break;
                case 30: this.Capabilities = PinCapability.GP; break;
                case 31: this.Capabilities = PinCapability.GP; break;

                default: throw new Exception("유효하지 못한 핀번호 입니다");
            }
            #endregion
        }
        #endregion

        #region Method
        public Input UseInput() { SetMode(PinMode.Input); return new Input(this); }
        public Output UseOutput() { SetMode(PinMode.Output); return new Output(this); }
        public PwmOutput UsePwm() { SetMode(PinMode.PwmOutput); return new PwmOutput(this); }
        public ClockOutput UseClock() { SetMode(PinMode.GpioClock); return new ClockOutput(this); }

        public void Unuse() => Mode = null;

        private void SetMode(PinMode mode)
        {
            if ((mode == PinMode.GpioClock && !HasCapability(PinCapability.GPCLK)) || (mode == PinMode.PwmOutput && !HasCapability(PinCapability.PWM)) || (mode == PinMode.Input && !HasCapability(PinCapability.GP)) || (mode == PinMode.Output && !HasCapability(PinCapability.GP)))
            {
                throw new NotSupportedException($"{PinNumber}번 핀은 {mode} 모드를 지원하지 않습니다");
            }

            WiringPi.PinMode(GpioNumber, (int)mode);
            Mode = mode;
        }

        private bool HasCapability(PinCapability capability) => (Capabilities & capability) == capability;
        #endregion
    }
    #endregion

    #region class : Input
    public class Input : GP
    {
        #region Properties
        public bool Value => Read();

        public PullMode InputPullMode
        {
            get => InputPullMode;
            set
            {
                if (pin == null) throw new Exception("사용 해지된 GP입니다.");
                else
                {
                    WiringPi.PullUpDnControl(pin.GpioNumber, (int)value);
                    _InputPullMode = value;
                }
            }
        }
        #endregion

        #region Event
        public event EventHandler<EdgeEventArgs> Edge;
        #endregion

        #region Member Variable
        PullMode _InputPullMode;
        #endregion

        #region Constructor
        public Input(Pin pin) : base(pin)
        {
        }
        #endregion

        #region Method
        bool Read()
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                return WiringPi.DigitalRead(GpioNumber) != 0;
            }
        }

        public void BeginInterrupt(Edge State)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                var registerResult = WiringPi.WiringPiISR(pin.GpioNumber, (int)GPIO.Edge.Both, () =>
                {
                    if (pin != null)
                    {
                        var v = WiringPi.DigitalRead(pin.GpioNumber) != 0;
                        if (v) Edge?.Invoke(this, new EdgeEventArgs(GPIO.Edge.Rising));
                        else Edge?.Invoke(this, new EdgeEventArgs(GPIO.Edge.Falling));
                    }
                });
                if (registerResult != 0) throw new Exception($"{pin.PinNumber}번 핀이 인터럽트 등록에 실패하였습니다. Error Code : {registerResult}.");
            }
        }
        #endregion
    }
    #endregion
    #region class : Output
    public class Output : GP
    {
        #region Properties
        public bool Value { get => Read(); set => Write(value); }

        public bool IsStartSoftPwm { get; private set; }
        public bool IsStartTone { get; private set; }
        #endregion

        #region Constructor
        public Output(Pin pin) : base(pin)
        {
        }
        #endregion

        #region Method
        #region Read / Write
        bool Read()
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                return WiringPi.DigitalRead(GpioNumber) != 0;
            }
        }

        void Write(bool v)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                WiringPi.DigitalWrite(GpioNumber, v ? 1 : 0);
            }
        }
        #endregion
        #region SoftPwm
        public void StartSoftPwm(int value, int range)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                var ret = WiringPi.SoftPwmCreate(pin.GpioNumber, value, range);
                if (ret == 0) IsStartSoftPwm = true;
                else throw new Exception($"SoftPwm 시작에 실패하였습니다.");
            }
        }

        public void SoftPwm(int value)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                if (IsStartSoftPwm) WiringPi.SoftPwmWrite(pin.GpioNumber, value);
                else throw new Exception($"SoftPwm이 시작되어 있지 않습니다.");
            }
        }

        public void StopSoftPwm()
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                if (IsStartSoftPwm) WiringPi.SoftPwmStop(pin.GpioNumber);
                else throw new Exception($"SoftPwm이 시작되어 있지 않습니다");
            }
        }
        #endregion
        #region Tone
        public void StartTone()
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                var ret = WiringPi.SoftToneCreate(pin.GpioNumber);
                if (ret == 0) IsStartTone = true;
                else throw new Exception($"Tone 시작에 실패하였습니다.");
            }
        }

        public void Tone(int freq)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                if (IsStartTone) WiringPi.SoftToneWrite(pin.GpioNumber, freq);
                else throw new Exception($"Tone 시작되어 있지 않습니다.");
            }
        }

        public void StopTone()
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                if (IsStartTone) WiringPi.SoftToneStop(pin.GpioNumber);
                else throw new Exception($"Tone이 시작되어 있지 않습니다");
            }
        }
        #endregion
        #endregion
    }
    #endregion
    #region class : PwmOutput
    public class PwmOutput : GP
    {
        #region Member Variable
        private PwmMode mode = PwmMode.Balanced;
        private uint range = 1024;
        private int value = 0, divisor = 1;
        #endregion

        #region Constructor
        public PwmOutput(Pin pin) : base(pin)
        {

        }
        #endregion

        #region Method
        public void Setup(PwmMode Mode, int divisor, uint range)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {

                this.mode = Mode;
                this.range = range;
                this.divisor = divisor;

                WiringPi.PwmSetMode((int)Mode);
                WiringPi.PwmSetClock(divisor);
                WiringPi.PwmSetRange(range);
            }
        }

        public void SetupHz(PwmMode Mode, int hz, uint range)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                var m = Pi.Model;
                var clk = m == PiModel.PI_MODEL_400 || m == PiModel.PI_MODEL_4B || m == PiModel.PI_MODEL_CM4 ? 54000000 : 19200000;
                var divisor = Convert.ToInt32(clk / hz / range);

                this.mode = Mode;
                this.range = range;
                this.divisor = divisor;

                WiringPi.PwmSetMode((int)Mode);
                WiringPi.PwmSetClock(divisor);
                WiringPi.PwmSetRange(range);
            }
        }

        public void Pwm(int value, uint range)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                this.range = range;
                this.value = value;

                WiringPi.PwmSetRange(range);
                WiringPi.PwmWrite(GpioNumber, value);
            }
        }

        public void Pwm(int value)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                this.value = value;

                WiringPi.PwmWrite(GpioNumber, value);
            }
        }

        public void PwmHz(int hz)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                var m = Pi.Model;
                var clk = m == PiModel.PI_MODEL_400 || m == PiModel.PI_MODEL_4B || m == PiModel.PI_MODEL_CM4 ? 54000000 : 19200000;
                var divisor = Convert.ToInt32(clk / hz / range);

                this.divisor = divisor;
                this.range = 1024;
                this.value = 512;

                WiringPi.PwmSetClock(divisor);
                WiringPi.PwmSetRange(range);
                WiringPi.PwmWrite(GpioNumber, value);
            }
        }
        #endregion
    }
    #endregion
    #region class : Clock
    public class ClockOutput : GP
    {
        #region Constructor
        public ClockOutput(Pin pin) : base(pin)
        {
        }
        #endregion

        #region Method
        public void Clock(int freq)
        {
            if (pin == null) throw new Exception("사용 해지된 GP입니다.");
            else
            {
                WiringPi.GpioClockSet(pin.GpioNumber, freq);
            }
        }
        #endregion
    }
    #endregion
    #region class : GP
    public class GP
    {
        #region Properties
        public int PinNumber
        {
            get
            {
                if (pin != null) return pin.PinNumber;
                else throw new Exception("사용 해지된 GP입니다.");
            }
        }
        public int GpioNumber
        {
            get
            {
                if (pin != null) return pin.GpioNumber;
                else throw new Exception("사용 해지된 GP입니다.");
            }
        }
        public PinMode Mode
        {
            get
            {
                if (pin != null)
                {
                    if (pin.Mode.HasValue) return pin.Mode.Value;
                    else throw new Exception("모드가 설정되지 않은 핀입니다.");
                }
                else throw new Exception("사용 해지된 GP입니다.");
            }
        }
        #endregion

        #region Member Variable
        protected Pin pin;
        #endregion

        #region Constructor
        public GP(Pin pin) => this.pin = pin;
        #endregion

        #region Unuse
        public void Unuse()
        {
            if (pin != null) pin.Unuse();
            pin = null;
        }
        #endregion
    }
    #endregion

    #region class : EdgeEventArgs
    public class EdgeEventArgs : EventArgs
    {
        public Edge State { get; private set; }

        public EdgeEventArgs(Edge state) => this.State = state;
    }
    #endregion
}
