using System;
using System.ComponentModel;
using System.Xml.Serialization;

// Token: 0x02000007 RID: 7
[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISNoEnterInCombat_Settings
{
	// Token: 0x0600000D RID: 13 RVA: 0x00002384 File Offset: 0x00000584
	public static SISNoEnterInCombat_Settings GetDefault()
	{
		return new SISNoEnterInCombat_Settings();
	}

	// Token: 0x04000002 RID: 2
	[NotVisible]
	[DefaultValue(false)]
	public bool LoggingActive = false;

	// Token: 0x04000003 RID: 3
	public bool EnabledForPlayer = true;

	// Token: 0x04000004 RID: 4
	public bool EnabledForCommunity = false;

	// Token: 0x04000005 RID: 5
	public bool EnabledForNPCs = false;

	// Token: 0x04000006 RID: 6
	public bool CanEnterCommunityBuildingsInCombat = true;
}
