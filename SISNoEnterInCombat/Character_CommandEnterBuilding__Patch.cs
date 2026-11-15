using System;
using HarmonyLib;

// Token: 0x02000002 RID: 2
[HarmonyPatch(typeof(Character), "CommandEnterBuilding")]
public class Character_CommandEnterBuilding__Patch
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private static bool Prefix(Character __instance, PlayerRecord playerRecord, Building target, bool isDoubleClick)
	{
		bool flag = __instance == null;
		bool flag2;
		if (flag)
		{
			flag2 = true;
		}
		else
		{
			bool flag3 = __instance.InCombat || SISNoEnterInCombat_Utils.IsCharacterInCombat(__instance);
			bool flag4 = (SISNoEnterInCombat_Config.EnabledForCommunity && __instance.IsOnPlayersTeam() && __instance != Hud.Instance.LocalControlledCharacter && flag3) || (SISNoEnterInCombat_Config.EnabledForNPCs && !__instance.IsOnPlayersTeam() && flag3);
			if (flag4)
			{
				bool flag5 = SISNoEnterInCombat_Config.CanEnterCommunityBuildingsInCombat && target.Community == __instance.Community;
				if (flag5)
				{
					SISNoEnterInCombat_Config.Log("Character " + __instance.GetDisplayNameString() + " can enter building from community even in combat");
					flag2 = true;
				}
				else
				{
					SISNoEnterInCombat_Config.Log("Character " + __instance.GetDisplayNameString() + " cant enter building because is in combat");
					flag2 = false;
				}
			}
			else
			{
				flag2 = true;
			}
		}
		return flag2;
	}
}
