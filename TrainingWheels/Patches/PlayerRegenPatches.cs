using HarmonyLib;

namespace TrainingWheels.Patches
{
    [HarmonyPatch(typeof(Dam_PlayerDamageBase))]
    internal static class PlayerRegenPatches
    {
        [HarmonyPatch(nameof(Dam_PlayerDamageBase.OnIncomingDamage))]
        [HarmonyPostfix]
        private static void Postfix_OnDamage(Dam_PlayerDamageBase __instance)
        {
            if (!ModConf.RegenDisabled) return;
            __instance.m_nextRegen = float.PositiveInfinity;
        }
    }
}
