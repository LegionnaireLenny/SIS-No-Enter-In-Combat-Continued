using HarmonyLib;

[HarmonyPatch(typeof(Character), "CommandEnterBuilding")]
public class Character_CommandEnterBuilding__Patch
{
	private static bool Prefix(Character __instance, PlayerRecord playerRecord, Building target, bool isDoubleClick)
	{
		if (__instance == null ||
            !GameCursor.IsTargetCharacterInCombat(__instance) ||
            (!SISNoEnterInCombat_Config.EnabledForPlayer && __instance.IsOnPlayersTeam() && __instance == Hud.Instance.LocalControlledCharacter) ||
			(!SISNoEnterInCombat_Config.EnabledForCommunity && __instance.IsOnPlayersTeam() && __instance != Hud.Instance.LocalControlledCharacter) ||
            (!SISNoEnterInCombat_Config.EnabledForNPCs && !__instance.IsOnPlayersTeam()))
		{
			return true;
		}

        if (SISNoEnterInCombat_Config.CanEnterCommunityBuildingsInCombat && target.Community == __instance.Community)
		{
			SISNoEnterInCombat_Config.Log($"Character {__instance.GetDisplayNameString()} can enter building from community even in combat");
			return true;
		}
		else
		{
			SISNoEnterInCombat_Config.Log($"Character {__instance.GetDisplayNameString()} cant enter building because is in combat");
			return false;
		}
	}
}
