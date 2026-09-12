using GameData;
using HarmonyLib;
using Player;
using SNetwork;
using System;

namespace TrainingWheels.Patches;

[HarmonyPatch(typeof(RundownManager))]
internal class SetActiveExpeditionPatches
{
    [HarmonyPatch(nameof(RundownManager.SetActiveExpedition))]
    [HarmonyPostfix]
    private static void Postfix_SetActiveExpedition(pActiveExpedition expPackage, ExpeditionInTierData expTierData, bool forceUpdate)
    {
        var health = Math.Clamp(ModConf.StartingHealth / 100f, .0f, 1f);
        var inf = Math.Clamp(ModConf.StartingInfection / 100f, .0f, 1f);

        var pInf = new pInfection
        {
            amount = inf,
            mode = pInfectionMode.Set,
            effect = pInfectionEffect.None
        };

        if (SNet.IsMaster)
        {
            foreach (var player in PlayerManager.PlayerAgentsInLevel)
            {

                player.Damage.SendSetHealth(health);
                player.Damage.m_receiveModifyInfectionPacket.Send(pInf, SNet_ChannelType.GameOrderCritical);
                player.Damage.ReceiveModifyInfection(pInf);
            }
        }
    }
}
