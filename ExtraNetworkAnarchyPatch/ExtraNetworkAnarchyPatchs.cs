using HarmonyLib;

namespace ExtraNetworkAnarchyPatch
{
    // 检查 Network Anarchy 的 Anarchy 开关（通过反射，无需引用 NA.dll）
    static class AnarchyHelper
    {
        public static bool IsAnarchy()
        {
            try
            {
                var naType = System.Type.GetType(
                    "NetworkAnarchy.NetworkAnarchy, NetworkAnarchy");
                var prop = naType?.GetProperty(
                    "Anarchy",
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Static);
                return prop != null && (bool)prop.GetValue(null, null);
            }
            catch { return false; }
        }

        public static bool IsMapEditor()
        {
            try
            {
                var tm = ColossalFramework.Singleton<ToolManager>.instance;
                return (tm?.m_properties?.m_mode &
                    ItemClass.Availability.MapEditor) != 0;
            }
            catch { return false; }
        }
    }

    #region MetroTrackAI
    // ── GetElevationLimits ──────────────────────────────────────────────────
    [HarmonyPatch(typeof(MetroTrackAI), "GetElevationLimits")]
    static class MTAI_GetElevationLimits
    {
        public static bool Prefix(out int min, out int max)
        {
            min = -3;
            max = 5;
            if (AnarchyHelper.IsAnarchy())
            {
                min = -999;
                max = 999;
                return false;
            }
            return true;
        }

        public static void Postfix(ref int min)
        {
            if (AnarchyHelper.IsMapEditor())
            {
                min = AnarchyHelper.IsAnarchy() ? -999 : 0;
            }
        }
    }

    // ── CheckBuildPosition ─────────────────────────────────────────────────
    [HarmonyPatch(typeof(MetroTrackAI), "CheckBuildPosition")]
    static class MTAI_CheckBuildPosition
    {
        public static bool Prefix(ref ToolBase.ToolErrors __result)
        {
            if (AnarchyHelper.IsAnarchy())
            {
                __result = ToolBase.ToolErrors.None;
                return false;
            }
            return true;
        }
    }
    #endregion

    #region MonorailTrackAI
    [HarmonyPatch(typeof(MonorailTrackAI), "GetElevationLimits")]
    static class MnrAI_GetElevationLimits
    {
        public static bool Prefix(out int min, out int max)
        {
            min = -3;
            max = 5;
            if (AnarchyHelper.IsAnarchy())
            {
                min = -999;
                max = 999;
                return false;
            }
            return true;
        }

        public static void Postfix(ref int min)
        {
            if (AnarchyHelper.IsMapEditor())
            {
                min = AnarchyHelper.IsAnarchy() ? -999 : 0;
            }
        }
    }
    #endregion

    #region CableCarPathAI
    [HarmonyPatch(typeof(CableCarPathAI), "GetElevationLimits")]
    static class CCPAI_GetElevationLimits
    {
        public static bool Prefix(out int min, out int max)
        {
            min = -3;
            max = 5;
            if (AnarchyHelper.IsAnarchy())
            {
                min = -999;
                max = 999;
                return false;
            }
            return true;
        }

        public static void Postfix(ref int min, ref int max)
        {
            if (AnarchyHelper.IsAnarchy())
            {
                min = -999;
                max = 999;
            }
        }
    }

    [HarmonyPatch(typeof(CableCarPathAI), "CheckBuildPosition")]
    static class CCPAI_CheckBuildPosition
    {
        public static bool Prefix(ref ToolBase.ToolErrors __result)
        {
            if (AnarchyHelper.IsAnarchy())
            {
                __result = ToolBase.ToolErrors.None;
                return false;
            }
            return true;
        }
    }
    #endregion

    #region PowerLineAI
    [HarmonyPatch(typeof(PowerLineAI), "GetElevationLimits")]
    static class PLAI_GetElevationLimits
    {
        public static bool Prefix(out int min, out int max)
        {
            min = -3;
            max = 5;
            if (AnarchyHelper.IsAnarchy())
            {
                min = -999;
                max = 999;
                return false;
            }
            return true;
        }

        public static void Postfix(ref int min, ref int max)
        {
            if (AnarchyHelper.IsAnarchy())
            {
                min = -999;
                max = 999;
            }
        }
    }
    #endregion

    #region RaceRoadAI
    [HarmonyPatch(typeof(RaceRoadAI), "CheckBuildPosition")]
    static class RRAI_CheckBuildPosition
    {
        public static bool Prefix(ref ToolBase.ToolErrors __result)
        {
            if (AnarchyHelper.IsAnarchy())
            {
                __result = ToolBase.ToolErrors.None;
                return false;
            }
            return true;
        }
    }
    #endregion
}