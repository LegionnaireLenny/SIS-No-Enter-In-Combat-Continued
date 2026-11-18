using HarmonyLib;

[HarmonyPatch(typeof(Hud), "OnSelectedDisabledAction")]
public class Hud_OnSelectedDisabledAction__Patch
{
	private static void ShowStatusBarMsg()
	{
        SISNoEnterInCombat_Utils.SetStatusBarMsgWithTimeout(GameImpl.Translate(SISNoEnterInCombat_Config.StatusMsgNoEnterInCombat), 1.5f);
	}

	private static void Postfix(Hud __instance, AvailableAction action)
    {
        Character actor = action.Actor;
        BaseObject target = action.Target;
        if (actor == null || 
            target == null ||
            actor != Hud.Instance.LocalControlledCharacter)
        {
            return;
        }

        if (action.ActionType == CursorAction.EnterBuilding)
        {
            ShowStatusBarMsg();
        }
        else if (action.ActionType == CursorAction.EnterBuildingNotInvestigated)
        {
            TerrainCoord entranceTile = (target as Building).GetEntranceTile(action.Amount);
            TileObject fixedObjectOnTile = GameTerrain.Instance.GetFixedObjectOnTile(entranceTile.x, entranceTile.y);
            if (fixedObjectOnTile != null)
            {
                ShowStatusBarMsg();
            }
        }
    }
}
