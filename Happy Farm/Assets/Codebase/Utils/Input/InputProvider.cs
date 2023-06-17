using UnityEngine;

namespace Codebase.Utils.Input
{
    public class InputProvider : IInputProvider
    {
        public PlayerInput PlayerInput { get; private set; }
        public PlayerInput.PlayerActions PlayerActions { get; private set; }

        public Vector2 Axis =>
            PlayerActions.Movement.ReadValue<Vector2>();

        public Vector2 ReadMousePosition() => 
            PlayerActions.Mouse.ReadValue<Vector2>();

        public float ScrollAxis =>
            PlayerActions.Zoom.ReadValue<float>();

        public float MouseAxis =>
            PlayerActions.MouseLook.ReadValue<float>();

        public bool IsLeftButtonUp() =>
            PlayerActions.LeftClick.IsPressed();

        public bool IsRightButtonUp() => 
            PlayerActions.RightClick.IsPressed();

        public InputProvider()
        {
            PlayerInput = new PlayerInput();
            PlayerActions = PlayerInput.Player;
        }

        public void EnablePlayer()
        {
            PlayerActions.Enable();
        }
        
        public void DisablePlayer()
        {
            PlayerActions.Disable();
        }
    }
}