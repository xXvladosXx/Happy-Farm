using UnityEngine;

namespace Codebase.Utils.Input
{
    public interface IInputProvider
    {
        PlayerInput.PlayerActions PlayerActions { get; }
        Vector2 Axis { get; }
        Vector2 ReadMousePosition();
        float ScrollAxis { get; }
        float MouseAxis { get; }
        bool IsLeftButtonUp();
        bool IsRightButtonUp();
        void DisablePlayer();
        void EnablePlayer();
    }
}