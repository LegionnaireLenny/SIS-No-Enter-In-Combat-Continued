using System;
using HarmonyLib;

// Token: 0x02000003 RID: 3
[HarmonyPatch(typeof(GameCursor), "GetAvailableActions")]
public class GameCursor_GetAvailableActions__Patch
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002130 File Offset: 0x00000330
	private static void Postfix(GameCursor __instance, ref BaseObject outTarget, ref bool wantTargetHeader)
	{
		for (int i = __instance.AvailableActions.Count - 1; i >= 0; i--)
		{
			BaseObject target = __instance.AvailableActions[i].Target;
			Character actor = __instance.AvailableActions[i].Actor;
			bool flag = actor == null;
			if (!flag)
			{
				bool flag2 = target == null;
				if (!flag2)
				{
					bool flag3 = SISNoEnterInCombat_Utils.IsCharacterInCombat(actor);
					bool flag4 = __instance.AvailableActions[i].ActionType == 63 || __instance.AvailableActions[i].ActionType == 64;
					if (flag4)
					{
						bool flag5 = flag3 && actor == Hud.Instance.LocalControlledCharacter;
						if (flag5)
						{
							bool flag6 = SISNoEnterInCombat_Config.CanEnterCommunityBuildingsInCombat && target.GetCommunity() != null && target.GetCommunity().CommunityType == 0;
							if (!flag6)
							{
								__instance.AvailableActions.RemoveAt(i);
								AvailableAction availableAction;
								availableAction..ctor(63, actor, target, 1);
								__instance.AvailableActions.Insert(i, availableAction);
							}
						}
					}
				}
			}
		}
	}
}
