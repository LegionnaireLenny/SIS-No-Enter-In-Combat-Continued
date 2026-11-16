using System;
using HarmonyLib;

// Token: 0x02000004 RID: 4
[HarmonyPatch(typeof(Hud), "OnSelectedDisabledAction")]
public class Hud_OnSelectedDisabledAction__Patch
{
	// Token: 0x06000005 RID: 5 RVA: 0x00002254 File Offset: 0x00000454
	private static void ShowStatusBarMsg()
	{
		string text = GameImpl.Translate("HUD_SISNoEnterInCombat");
		SISNoEnterInCombat_Utils.SetStatusBarMsgWithTimeout(text, 1.5f);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000227C File Offset: 0x0000047C
	private static void Postfix(Hud __instance, AvailableAction action)
	{
		Character actor = action.Actor;
		BaseObject target = action.Target;
		bool flag = actor == null || target == null;
		if (!flag)
		{
			bool flag2 = actor != Hud.Instance.LocalControlledCharacter;
			if (!flag2)
			{
				bool flag3 = action.ActionType == 63;
				if (flag3)
				{
					Hud_OnSelectedDisabledAction__Patch.ShowStatusBarMsg();
				}
				else
				{
					bool flag4 = action.ActionType == 65;
					if (flag4)
					{
						TerrainCoord entranceTile = ((Building)target).GetEntranceTile(action.Amount);
						TileObject fixedObjectOnTile = GameTerrain.Instance.GetFixedObjectOnTile(entranceTile.x, entranceTile.y);
						bool flag5 = fixedObjectOnTile != null;
						if (flag5)
						{
							Hud_OnSelectedDisabledAction__Patch.ShowStatusBarMsg();
						}
					}
				}
			}
		}
	}
}
