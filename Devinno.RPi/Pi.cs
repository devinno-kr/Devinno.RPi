using Devinno.RPi.GPIO;
using Devinno.RPi.Native;
using System;

namespace Devinno.RPi
{
    public class Pi
    {
        #region Properties
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

        public void Initialize()
        {

        }
    }
}
