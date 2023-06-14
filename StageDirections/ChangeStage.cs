using System;
using UnityEngine;

namespace StageDirections
{
    public class ChangeStage : StageDirection
    {
        public ChangeStage()
        {
            name = "changeStage stageName";
        }
        public override void Execute(string[] args, object[] utils)
        {
            foreach(Manager.Location location in (Manager.Location[])utils[1])
            {
                if(string.Equals(location.name.ToLower(), args[0].ToLower(), StringComparison.CurrentCultureIgnoreCase))
                {
                    RenderSettings.skybox = location.skybox;
                }
            }
        }
    }
}
