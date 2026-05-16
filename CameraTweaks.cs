using UnityEngine;
using UnityEngine.InputSystem;

namespace CameraTweaks
{
	public class CameraTweaks : MonoBehaviour
	{
		public CameraTweaks(IntPtr ptr) : base(ptr) { }

		private Player _player;
		private bool _initialised;
		private Vector3 _defaultPosition;

		private const float MinDistance = -1f;
		private const float MaxDistance = -30f;

		private void Update()
		{
			if (!_initialised)
			{
				if (Player.Current == null) return;
				_player = Player.Current;
				_initialised = true;
				return;
			}

			HandleScroll();
		}

		private void HandleScroll()
		{
			if (!Settings.EnableScrolling.Value)
				return;

			if (_player.GetPlayerState != PlayerState.Driving)
				return;

			if (_player.GetCurrentThirdPersonCharacterModel == null)
				return;

			float scroll = Mouse.current.scroll.ReadValue().y;
			if (scroll == 0f) return;

			var position = _player.drivingThirdPersonCameraPosition;
			float newZ = Mathf.Clamp(position.z + scroll * Settings.ScrollSpeed.Value * Time.deltaTime, MaxDistance, MinDistance);
			_player.drivingThirdPersonCameraPosition = new Vector3(position.x, position.y, newZ);
		}
	}
}
