using System;

public class SISNoEnterInCombat_Utils
{
	public static void SetStatusBarMsgWithTimeout(string msg, float timeout)
	{
		HudBehaviour.Instance.StatusBarMsg = msg;
		HudBehaviour.Instance.StatusBarMsgTimeout = timeout;
		HudBehaviour.Instance.StatusBarMsgShowForOneFrame = false;
		HudBehaviour.Instance.StatusBarMsgAction = default(AvailableAction);
	}

	public static float NextFloat()
	{
		double num = SISNoEnterInCombat_Utils.random.NextDouble();
		return (float)num;
	}

	public static int NextBetweenInts(int min, int max)
	{
		return SISNoEnterInCombat_Utils.random.Next(min, max);
	}

	private static Random random = new Random();
}
