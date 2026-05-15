using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;

namespace CameraTweaks
{
	[BepInPlugin("com.M-.CameraTweaks", "CameraTweaks", "1.0.0")]
	public class Plugin : BasePlugin
	{
		internal static ManualLogSource Logging;
		internal static ConfigEntry<bool> EnableScrolling;
		internal static ConfigEntry<float> ScrollSpeed;

		public override void Load()
		{
			Logging = Log;
			EnableScrolling = Config.Bind(
				"Camera.ThirdPerson",
				"Zooming",
				true,
				"Set to true to enable zooming in and out with the scroll wheel when in third person"
			);

			ScrollSpeed = Config.Bind(
				"Camera.ThirdPerson",
				"ScrollSpeed",
				6f,
				"How fast the camera zooms in and out with the scroll wheel when in vehicle 3rd person if enabled"
			);
			AddComponent<CameraTweaks>();
		}
	}
}
