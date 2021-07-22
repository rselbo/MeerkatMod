using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MeerkatMod
{
    class MeerkatMod : Mod
    {
        public MeerkatMod(ModContentPack content) : base(content)
        {
            Log.Message("MEERKATS are invading");
        }
    }
}
