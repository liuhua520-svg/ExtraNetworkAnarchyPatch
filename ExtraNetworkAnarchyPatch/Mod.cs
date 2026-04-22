using CitiesHarmony.API;
using ICities;
using HarmonyLib;

namespace ExtraNetworkAnarchyPatch
{
    public class Mod : IUserMod
    {
        public string Name => "Extra Network Anarchy Patch";
        public string Description => "Adds elevation anarchy support for missing AI, supplements Network Anarchy";

        private static Harmony _harmony;

        public void OnEnabled()
        {
            HarmonyHelper.DoOnHarmonyReady(() =>
            {
                _harmony = new Harmony("extraanarchy.patch");
                _harmony.PatchAll();
            });
        }

        public void OnDisabled()
        {
            _harmony?.UnpatchAll("extraanarchy.patch");
        }
    }
}