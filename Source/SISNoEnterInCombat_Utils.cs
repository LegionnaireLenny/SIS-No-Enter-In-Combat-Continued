using System;

// Token: 0x02000009 RID: 9
public class SISNoEnterInCombat_Utils
{
	// Token: 0x06000017 RID: 23 RVA: 0x000027BC File Offset: 0x000009BC
	public static bool IsCharacterInCombat(Character character)
	{
		bool flag = character == null;
		return !flag && GameCursor.IsTargetCharacterInCombat(character);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000027E0 File Offset: 0x000009E0
	public static void SetStatusBarMsgWithTimeout(string msg, float timeout)
	{
		HudBehaviour.Instance.StatusBarMsg = msg;
		HudBehaviour.Instance.StatusBarMsgTimeout = timeout;
		HudBehaviour.Instance.StatusBarMsgShowForOneFrame = false;
		HudBehaviour.Instance.StatusBarMsgAction = default(AvailableAction);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002814 File Offset: 0x00000A14
	public static float NextFloat()
	{
		double num = SISNoEnterInCombat_Utils.random.NextDouble();
		return (float)num;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002834 File Offset: 0x00000A34
	public static int NextBetweenInts(int min, int max)
	{
		return SISNoEnterInCombat_Utils.random.Next(min, max);
	}

	// Token: 0x04000010 RID: 16
	private static Random random = new Random();
}
