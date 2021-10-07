using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.Universal.Sound
{
    public class PlayProperties
    {
        public float volume = 1;
        public float pitch = 1;

        public float GetVolume => volume.MinMaxFloat(0, 1);
        public float GetPitch => volume.MinMaxFloat(-3, 3);
    }
}
