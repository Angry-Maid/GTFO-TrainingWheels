using HarmonyLib;
using Player;
using System;
using System.Collections.Generic;

namespace TrainingWheels.Patches;

[HarmonyPatch]
internal static class AmmoStoragePatches
{
    [HarmonyPatch(typeof(PlayerAmmoStorage), nameof(PlayerAmmoStorage.AddLevelDefaultAmmoModifications))]
    [HarmonyWrapSafe]
    [HarmonyPostfix]
    private static void Post_SetStorage(PlayerAmmoStorage __instance, PlayerAgent owner)
    {
        if (RundownManager.ActiveExpedition == null) return;

        List<float> ammoMods =
        new()
        {
            Math.Clamp(ModConf.ModMainStartingAmmo / 100f, 0, 65535f),
            Math.Clamp(ModConf.ModSpecialStartingAmmo / 100f, 0, 65535f),
            Math.Clamp(ModConf.ModToolStartingAmmo / 100f, 0, 65535f),
        };

        var idArr = __instance.m_ammoModificationIDs;
        var specialOverride = RundownManager.ActiveExpedition.SpecialOverrideData;

        if (!AgentModifierManager.TryGetModifierInstance(owner, out var modifierInstance))
        {
            AgentModifierManager.AddModifierValue(owner, AgentModifier.InitialAmmoStandard, 0);
            AgentModifierManager.TryGetModifierInstance(owner, out modifierInstance);
        }

        float oldMod = modifierInstance.GetModifierValue(AgentModifier.InitialAmmoStandard) + 1;
        AgentModifierManager.ClearModifierChange(idArr[0]);
        idArr[0] = AgentModifierManager.AddModifierValue(owner, AgentModifier.InitialAmmoStandard, oldMod * ammoMods[0] - 1f);

        oldMod = modifierInstance.GetModifierValue(AgentModifier.InitialAmmoSpecial) + 1;
        AgentModifierManager.ClearModifierChange(idArr[1]);
        idArr[1] = AgentModifierManager.AddModifierValue(owner, AgentModifier.InitialAmmoSpecial, oldMod * ammoMods[1] - 1f);

        oldMod = modifierInstance.GetModifierValue(AgentModifier.InitialAmmoTool) + 1;
        AgentModifierManager.ClearModifierChange(idArr[2]);
        idArr[2] = AgentModifierManager.AddModifierValue(owner, AgentModifier.InitialAmmoTool, oldMod * ammoMods[2] - 1f);
    }
}
