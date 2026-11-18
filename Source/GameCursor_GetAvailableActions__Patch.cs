using HarmonyLib;

[HarmonyPatch(typeof(GameCursor), "GetAvailableActions")]
public class GameCursor_GetAvailableActions__Patch
{
	private static void Postfix(GameCursor __instance, ref BaseObject outTarget, ref bool wantTargetHeader)
	{
		for (int i = __instance.AvailableActions.Count - 1; i >= 0; i--)
        {
            Character actor = __instance.AvailableActions[i].Actor;
            BaseObject target = __instance.AvailableActions[i].Target;
            if (actor == null || 
                target == null ||
                (__instance.AvailableActions[i].ActionType != CursorAction.EnterBuilding && __instance.AvailableActions[i].ActionType != CursorAction.EnterBuildingNotInvestigated) ||
                (SISNoEnterInCombat_Config.CanEnterCommunityBuildingsInCombat && target.GetCommunity() != null && target.GetCommunity().CommunityType == CommunityType.Player) ||
                !GameCursor.IsTargetCharacterInCombat(actor) ||
                actor != Hud.Instance.LocalControlledCharacter)
            {
                continue;
            }

            AvailableAction availableAction = new AvailableAction(__instance.AvailableActions[i].ActionType, actor, target, CursorActionDisabledReason.Disabled);
            __instance.AvailableActions.RemoveAt(i);
            __instance.AvailableActions.Insert(i, availableAction);
        }
    }
}
