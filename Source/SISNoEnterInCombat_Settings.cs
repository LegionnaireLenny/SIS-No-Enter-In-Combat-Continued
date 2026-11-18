using System;
using System.ComponentModel;
using System.Xml.Serialization;

[DesignerCategory("code")]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
[Serializable]
public class SISNoEnterInCombat_Settings
{
	public static SISNoEnterInCombat_Settings GetDefault()
	{
		return new SISNoEnterInCombat_Settings();
	}

	public bool LoggingEnabled = false;
	public bool EnabledForPlayer = true;
	public bool EnabledForCommunity = false;
	public bool EnabledForNPCs = false;
	public bool CanEnterCommunityBuildingsInCombat = true;
}
