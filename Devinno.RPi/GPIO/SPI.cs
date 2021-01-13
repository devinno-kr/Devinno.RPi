using Devinno.RPi.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.RPi.GPIO
{
    public class SPI
    {
        #region Const
        public const int MinFrequency = 500000;
        public const int MaxFrequency = 32000000;
        #endregion
        #region Properties
        public int Channel { get; private set; }
        public int Frequency { get; private set; } = 8000000;
        public int FileDescriptor { get; private set; } = -1;
        #endregion
        #region Member Variable
        #endregion
        #region Constructor
        internal SPI(int Channel)
        {
            this.Channel = Channel;
        }
        #endregion
        #region Method
        public void Setup(int Frequency)
        {
            #region this.Frequency
            var freq = Frequency;
            if (freq < MinFrequency) freq = MinFrequency;
            if (freq > MaxFrequency) freq = MaxFrequency;
            this.Frequency = freq;
            #endregion
            this.FileDescriptor = WiringPi.WiringPiSPISetup(this.Channel, this.Frequency);
            if (this.FileDescriptor < 0) throw new Exception($"SPI{Channel}을 설정에 실패하였습니다. Error Code: {this.FileDescriptor}.");
        }

        public byte[] SendReceive(byte[] buffer)
        {
            if (buffer == null || buffer.Length == 0)
                return null;

            var spiBuffer = new byte[buffer.Length];
            Array.Copy(buffer, spiBuffer, buffer.Length);

            var result = WiringPi.WiringPiSPIDataRW(Channel, spiBuffer, spiBuffer.Length);
            if (result < 0) throw new Exception($"SPI{Channel}의 보내기/받기 작업에 실패하였습니다. Error Code: {this.FileDescriptor}.");

            return spiBuffer;
        }
        #endregion
    }
}
