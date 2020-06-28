using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Audio
{
    public class SoundHandleGroup : List<SoundHandle>
    {
        public void Stop()
        {
            foreach (var audioObject in this)
            {
                audioObject.Stop();
            }
        }
    }
}
