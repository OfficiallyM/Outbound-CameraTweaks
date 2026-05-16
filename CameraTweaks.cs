using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraTweaks
{
	public class CameraTweaks : MonoBehaviour
	{
		public CameraTweaks(IntPtr ptr) : base(ptr) { }

		private const float MinDistance = -1f;
		private const float MaxDistance = -30f;

		private Player _player;
		private SettingsManager _gameSettings;
		private bool _initialised;

		private Vector3 _defaultPosition;

		private Camera _camera;
		private bool _cameraReady;
		private float _currentZoomedFOV;
		private bool _wasZoomed;
		private float _defaultSensitivity;
		private float _lastSensitivity;

		private void Update()
		{
			if (!_initialised)
			{
				if (Player.Current == null) return;
				_player = Player.Current;
				_gameSettings = SettingsManager.Current;
				_initialised = true;
				return;
			}

			HandleThirdPersonScroll();
			HandleZoom();
		}

		private void HandleThirdPersonScroll()
		{
			if (!Settings.EnableScrolling.Value) return;
			if (_player.GetPlayerState != PlayerState.Driving) return;
			if (_player.GetCurrentThirdPersonCharacterModel == null) return;
			if (Mouse.current.rightButton.isPressed) return;

			float scroll = Mouse.current.scroll.ReadValue().y;
			if (scroll == 0f) return;
			var position = _player.drivingThirdPersonCameraPosition;
			float newZ = Mathf.Clamp(position.z + scroll * Settings.ScrollSpeed.Value * Time.deltaTime, MaxDistance, MinDistance);
			_player.drivingThirdPersonCameraPosition = new Vector3(position.x, position.y, newZ);
		}

		private void HandleZoom()
		{
			if (!Settings.EnableZooming.Value) return;
			var playerState = _player.GetPlayerState;
			if (playerState != PlayerState.Driving && playerState != PlayerState.Locomotion) return;

			if (!_cameraReady)
			{
				_camera = _player.GetPlayerCamera;
				if (_camera == null) return;
				if (_player.cameraSensitivity <= 0) return;
				_cameraReady = true;
				_defaultSensitivity = _player.cameraSensitivity;
				_lastSensitivity = _gameSettings.LocalSettings.CameraSensitivity;
				return;
			}

			bool isZoomed = Mouse.current.rightButton.isPressed;

			// Reset zoom to default when starting a new zoom.
			if (isZoomed && !_wasZoomed)
			{
				_currentZoomedFOV = Settings.ZoomedFOV.Value;
				_defaultSensitivity = _player.cameraSensitivity;
			}

			// Player sensitivity changed, re-cache.
			if (_lastSensitivity != _gameSettings.LocalSettings.CameraSensitivity)
			{
				_defaultSensitivity = _player.cameraSensitivity;
				_lastSensitivity = _gameSettings.LocalSettings.CameraSensitivity;
				return;
			}

			if (isZoomed)
			{
				float scroll = Mouse.current.scroll.ReadValue().y;
				if (scroll != 0f)
					_currentZoomedFOV = Mathf.Clamp(
						_currentZoomedFOV - scroll * Settings.ZoomScrollSpeed.Value * Time.deltaTime,
						0f,
						_gameSettings.GetLocalSettings.FieldOfView
					);
			}

			// Scale sensitivity by FOV ratio.
			_player.cameraSensitivity = isZoomed
				? _defaultSensitivity * (_camera.fieldOfView / _gameSettings.GetLocalSettings.FieldOfView)
				: _defaultSensitivity;

			_wasZoomed = isZoomed;
			float targetFOV = isZoomed ? _currentZoomedFOV : _gameSettings.GetLocalSettings.FieldOfView;
			_camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, targetFOV, Settings.ZoomSpeed.Value * Time.deltaTime);
		}
	}
}
