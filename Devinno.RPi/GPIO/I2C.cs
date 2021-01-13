using Devinno.RPi.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devinno.RPi.GPIO
{
    public class I2C
    {
        #region Properties
        public int DeviceID { get; private set; }
        public int FileDescriptor { get; private set; }
        #endregion

        #region Member Variable
        #endregion

        #region Constructor
        internal I2C(int DeviceID, int FileDescriptor)
        {
            this.DeviceID = DeviceID;
            this.FileDescriptor = FileDescriptor;
        }
        #endregion

        #region Method
        public byte Read()
        {
            var result = WiringPi.WiringPiI2CRead(FileDescriptor);
            if (result < 0) throw new Exception($"I2C {DeviceID} 의 읽기 작업에 실패하였습니다. Error Code: {result}.");
            return (byte)result;
        }

        public byte ReadAddressByte(int address)
        {
            var result = WiringPi.WiringPiI2CReadReg8(FileDescriptor, address);
            if (result < 0) throw new Exception($"I2C {DeviceID} 의 읽기 작업에 실패하였습니다. Error Code: {result}.");

            return (byte)result;
        }

        public ushort ReadAddressWord(int address)
        {
            var result = WiringPi.WiringPiI2CReadReg16(FileDescriptor, address);
            if (result < 0) throw new Exception($"I2C {DeviceID} 의 읽기 작업에 실패하였습니다. Error Code: {result}.");

            return Convert.ToUInt16(result);
        }

        public void Write(byte[] data)
        {
            foreach (var b in data)
            {
                var result = WiringPi.WiringPiI2CWrite(FileDescriptor, b);
                if (result < 0) throw new Exception($"I2C {DeviceID} 의 쓰기 작업에 실패하였습니다. Error Code: {result}.");
            }
        }

        public void WriteAddressByte(int address, byte data)
        {
            var result = WiringPi.WiringPiI2CWriteReg8(FileDescriptor, address, data);
            if (result < 0) throw new Exception($"I2C {DeviceID} 의 쓰기 작업에 실패하였습니다. Error Code: {result}.");
        }

        public void WriteAddressWord(int address, ushort data)
        {
            var result = WiringPi.WiringPiI2CWriteReg16(FileDescriptor, address, data);
            if (result < 0) throw new Exception($"I2C {DeviceID} 의 쓰기 작업에 실패하였습니다. Error Code: {result}.");
        }
        #endregion
    }
}
