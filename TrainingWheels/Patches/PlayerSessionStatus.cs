using HarmonyLib;
using Player;
using System;

namespace TrainingWheels.Patches;

[HarmonyPatch(typeof(PlayerSessionStatusManager))]
internal class PlayerSessionStatus
{
    [HarmonyPatch(nameof(PlayerSessionStatusManager.GetDefaultSessionStatus))]
    [HarmonyPostfix]
    private static void Postfix_GetDefaultSessionStatus(ref pPlayerSessionStatus __result)
    {
        __result.resourceData.health = Math.Clamp(ModConf.StartingHealth / 100f, .0f, 1f);
        __result.resourceData.infection = Math.Clamp(ModConf.StartingInfection / 100f, .0f, 1f);
    }
}
