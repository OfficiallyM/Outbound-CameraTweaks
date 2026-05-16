using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace CameraTweaks
{
	[BepInPlugin("com.M-.CameraTweaks", "CameraTweaks", "1.0.0")]
	public class Plugin : BasePlugin
	{
		internal static ManualLogSource Logging;	

		public override void Load()
		{
			Logging = Log;
			Settings.Build(Config);
			AddComponent<CameraTweaks>();
		}
	}
}
