using BepInEx.Configuration;

namespace CameraTweaks
{
	internal static class Settings
	{
		// Third person settings.
		public static ConfigEntry<bool> EnableScrolling;
		public static ConfigEntry<float> ScrollSpeed;

		// Zoom settings.
		public static ConfigEntry<bool> EnableZooming;
		public static ConfigEntry<float> ZoomedFOV;
		public static ConfigEntry<float> ZoomSpeed;
		public static ConfigEntry<float> ZoomScrollSpeed;

		public static void Build(ConfigFile config)
		{
			// Third person settings.
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

			// Zoom settings.
			EnableZooming = config.Bind(
				"Camera.Zoom",
				"Zooming",
				true,
				"Set to true to enable zooming in when holding right click"
			);
			ZoomedFOV = config.Bind(
				"Camera.Zoom",
				"ZoomedFOV",
				30f,
				"Default field of view when zoomed in with right click"
			);
			ZoomSpeed = config.Bind(
				"Camera.Zoom",
				"ZoomSpeed",
				15f,
				"How fast the zoom transitions in and out"
			);
			ZoomScrollSpeed = config.Bind(
				"Camera.Zoom",
				"ZoomScrollSpeed",
				30f,
				"How fast the camera zooms in and out with the scroll wheel when zoomed in if enabled"
			);
		}
	}
}
