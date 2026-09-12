using HarmonyLib;
using Player;

namespace TrainingWheels.Patches;

[HarmonyPatch(typeof(PlayerAgent))]
internal class PlayerAgentRegenPatch
{
    [HarmonyPatch(nameof(PlayerAgent.Setup))]
    [HarmonyPostfix]
    private static void Postfix_Setup(PlayerAgent __instance)
    {
        if (!ModConf.RegenDisabled) return;
        __instance.Damage.m_nextRegen = float.PositiveInfinity;
    }
}
