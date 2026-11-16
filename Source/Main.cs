using System;
using HarmonyLib;

// Token: 0x02000005 RID: 5
public class Main
{
	// Token: 0x06000008 RID: 8 RVA: 0x0000232F File Offset: 0x0000052F
	public static void Load()
	{
		Main.HarmonyInstance = new Harmony("SISNoEnterInCombat");
		Main.HarmonyInstance.PatchAll();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000234C File Offset: 0x0000054C
	public static void Unload()
	{
		bool flag = Main.HarmonyInstance != null;
		if (flag)
		{
			Main.HarmonyInstance.UnpatchAll(null);
		}
	}

	// Token: 0x04000001 RID: 1
	public static Harmony HarmonyInstance;
}
