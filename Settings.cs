using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraTweaks
{
	internal static class Settings
	{
		public static ConfigEntry<bool> EnableScrolling;
		public static ConfigEntry<float> ScrollSpeed;

		public static void Build(ConfigFile config)
		{
			EnableScrolling = config.Bind(
				"Camera.ThirdPerson",
				"Zooming",
				true,
				"Set to true to enable zooming in and out with the scroll wheel when in third person"
			);

			ScrollSpeed = config.Bind(
				"Camera.ThirdPerson",
				"ScrollSpeed",
				6f,
				"How fast the camera zooms in and out with the scroll wheel when in vehicle 3rd person if enabled"
			);
		}
	}
}
