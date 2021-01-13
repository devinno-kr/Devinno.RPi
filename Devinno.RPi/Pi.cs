using Devinno.RPi.GPIO;
using Devinno.RPi.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Devinno.RPi
{
    public class Pi
    {
        #region Properties
        #region Model
        public static PiModel Model
        {
            get
            {
                int model = 0, mem = 0, maker = 0, overVolted = 0;
                WiringPi.PiBoardId(ref model, ref mem, ref maker, ref overVolted);

                return (PiModel)model;
            }
        }
        #endregion
        #region Pin
        public static Pin GPIO2 => Pin3;       // Header P1 Physical Pin 3. GPIO 0 for rev1 or GPIO 2 for rev2.
        public static Pin GPIO3 => Pin5;       // Header P1 Physical Pin 5. GPIO 1 for rev1 or GPIO 3 for rev2.
        public static Pin GPIO4 => Pin7;       // Header P1 Physical Pin 7. GPIO 4.
        public static Pin GPIO14 => Pin8;      // Header P1 Physical Pin 8. GPIO 14.
        public static Pin GPIO => Pin10;       // Header P1 Physical Pin 10. GPIO 15.
        public static Pin GPIO17 => Pin11;     // Header P1 Physical Pin 11. GPIO 17.
        public static Pin GPIO18 => Pin12;     // Header P1 Physical Pin 12. GPIO 18.
        public static Pin GPIO27 => Pin13;     // Header P1 Physical Pin 13. GPIO 21 for rev1 or GPIO 27 for rev2.
        public static Pin GPIO22 => Pin15;     // Header P1 Physical Pin 15. GPIO 22.
        public static Pin GPIO23 => Pin16;     // Header P1 Physical Pin 16. GPIO 23.
        public static Pin GPIO24 => Pin18;     // Header P1 Physical Pin 18. GPIO 24.
        public static Pin GPIO10 => Pin19;     // Header P1 Physical Pin 19. GPIO 10.
        public static Pin GPIO9 => Pin21;      // Header P1 Physical Pin 21. GPIO 9.
        public static Pin GPIO25 => Pin22;     // Header P1 Physical Pin 22. GPIO 25.
        public static Pin GPIO11 => Pin23;     // Header P1 Physical Pin 23. GPIO 11.
        public static Pin GPIO8 => Pin24;      // Header P1 Physical Pin 24. GPIO 8.
        public static Pin GPIO7 => Pin26;      // Header P1 Physical Pin 26. GPIO 7.
        public static Pin GPIO0 => Pin27;      // Header P1 Physical Pin 27. GPIO 0.
        public static Pin GPIO1 => Pin28;      // Header P1 Physical Pin 28. GPIO 1.
        public static Pin GPIO5 => Pin29;      // Header P1 Physical Pin 29. GPIO 5.
        public static Pin GPIO6 => Pin31;      // Header P1 Physical Pin 31. GPIO 6.
        public static Pin GPIO12 => Pin32;     // Header P1 Physical Pin 32. GPIO 12.
        public static Pin GPIO13 => Pin33;     // Header P1 Physical Pin 33. GPIO 13.
        public static Pin GPIO19 => Pin35;     // Header P1 Physical Pin 35. GPIO 19.
        public static Pin GPIO16 => Pin36;     // Header P1 Physical Pin 36. GPIO 16.
        public static Pin GPIO26 => Pin37;     // Header P1 Physical Pin 37. GPIO 26.
        public static Pin GPIO20 => Pin38;     // Header P1 Physical Pin 38. GPIO 20.
        public static Pin GPIO21 => Pin40;     // Header P1 Physical Pin 40. GPIO 21.

        public static Pin Pin3 { get; } = new Pin(3);          
        public static Pin Pin5 { get; } = new Pin(5);          
        public static Pin Pin7 { get; } = new Pin(7);          
        public static Pin Pin8 { get; } = new Pin(8);          
        public static Pin Pin10 { get; } = new Pin(10);        
        public static Pin Pin11 { get; } = new Pin(11);        
        public static Pin Pin12 { get; } = new Pin(12);        
        public static Pin Pin13 { get; } = new Pin(13);        
        public static Pin Pin15 { get; } = new Pin(15);        
        public static Pin Pin16 { get; } = new Pin(16);        
        public static Pin Pin18 { get; } = new Pin(18);        
        public static Pin Pin19 { get; } = new Pin(19);        
        public static Pin Pin21 { get; } = new Pin(21);        
        public static Pin Pin22 { get; } = new Pin(22);        
        public static Pin Pin23 { get; } = new Pin(23);        
        public static Pin Pin24 { get; } = new Pin(24);        
        public static Pin Pin26 { get; } = new Pin(26);        
        public static Pin Pin27 { get; } = new Pin(27);        
        public static Pin Pin28 { get; } = new Pin(28);        
        public static Pin Pin29 { get; } = new Pin(29);        
        public static Pin Pin31 { get; } = new Pin(31);        
        public static Pin Pin32 { get; } = new Pin(32);        
        public static Pin Pin33 { get; } = new Pin(33);        
        public static Pin Pin35 { get; } = new Pin(35);        
        public static Pin Pin36 { get; } = new Pin(36);        
        public static Pin Pin37 { get; } = new Pin(37);        
        public static Pin Pin38 { get; } = new Pin(38);        
        public static Pin Pin40 { get; } = new Pin(40);
        #endregion
        #region SPI
        public static SPI SPI0 { get; } = new SPI(0);
        public static SPI SPI1 { get; } = new SPI(1);
        #endregion
        #region I2C
        static Dictionary<int, I2C> I2cDevices { get; set; } = new Dictionary<int, I2C>();
        #endregion
        #endregion

        #region Initialize
        public static void Initialize()
        {
            WiringPi.WiringPiSetupGpio();
        }
        #endregion
        #region I2C
        public static I2C AddI2C(int deviceID)
        {
            if (I2cDevices.ContainsKey(deviceID)) return I2cDevices[deviceID];

            var fileDescriptor = (int)WiringPi.WiringPiI2CSetup(deviceID);
            if (fileDescriptor < 0)
                throw new Exception($"I2C {deviceID} 를 등록할 수 없습니다. Error Code: {fileDescriptor}.");

            var device = new I2C(deviceID, fileDescriptor);
            I2cDevices[deviceID] = device;
            return device;
        }

        public static I2C GetI2C(int deviceID)
        {
            if (I2cDevices.ContainsKey(deviceID)) return I2cDevices[deviceID];
            else return null;
        }

        public List<I2C> GetI2Cs() => I2cDevices.Values.ToList();
        #endregion
    }
}
