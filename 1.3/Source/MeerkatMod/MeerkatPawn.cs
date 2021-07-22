using System.Linq;
using Verse;
using UnityEngine;

namespace MeerkatMod
{
    public class MeerkatPawn : Pawn
    {
        private ModContentHolder<Texture2D> contentHolder = LoadedModManager.RunningMods
            .FirstOrDefault(x => x.Name == "MeerkatMod")
            .GetContentHolder<Texture2D>();

        private const int Frameskip = 16; // only update every n-th tick
        private int FrameskipOffset { get; } = Rand.RangeInclusive(0, Frameskip - 1);

        private Graphic standing = null;
        private Graphic moving = null;

        bool isStanding = false;

        public override void Tick()
        {
            base.Tick();

            //if (GenTicks.TicksAbs % Frameskip != FrameskipOffset) return; // frameskip
            if (!Spawned) return;
            if (Destroyed) return;
            if (pather == null) return; // this can occur if the pawn leaves the map area
            if (Drawer?.renderer == null) return;

            if(standing == null)
            {
                {
                    var data = new GraphicData();
                    data.CopyFrom(ageTracker.CurKindLifeStage.bodyGraphicData);
                    standing = data.Graphic;
                }
                {
                    var data = new GraphicData();
                    data.CopyFrom(ageTracker.CurKindLifeStage.bodyGraphicData);
                    data.texPath = "Things/Pawn/Animal/Meerkat/Meerkat_moving";
                    moving = data.Graphic;
                }
            }
            
            if(pather.Moving)
            {
                if(isStanding)
                {
                    //Log.Message($"[MeerkatMod] {ToString()} is changing to moving");
                    Drawer.renderer.graphics.nakedGraphic = moving;
                    Drawer.renderer.graphics.ClearCache();
                    isStanding = false;
                }
            }
            else
            {
                if (!isStanding)
                {
                    //Log.Message($"[MeerkatMod] {ToString()} is changing to standing");
                    Drawer.renderer.graphics.nakedGraphic = standing;
                    Drawer.renderer.graphics.ClearCache();
                    isStanding = true;
                }
            }
        }
    }
}
