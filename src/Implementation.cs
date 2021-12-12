using MelonLoader;
using ModSettings;
using UnityEngine;

namespace ReadWhenHungry
{
    public class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            ReadWhenHungrySettings.options.AddToModSettings("Read When Hungry", MenuType.Both);
        }
    }
}
