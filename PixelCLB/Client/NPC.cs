using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PixelCLB
{
    public class NPC : LoadedLife
    {
        public NPC(int id)
        {
            this.ID = id;
        }

        public override string ToString()
        {
            return string.Concat(this.ID, " : ", this.ObjectID);
        }
    }
}
