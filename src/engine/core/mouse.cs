using Microsoft.Xna.Framework;

namespace Jade;

class Mouse {

    static InputManager manager;

    public static Vector2 Pos {
        get {
            return manager.GetMousePos();
        }
    }

    public static void SetManager(InputManager _manager) { manager = _manager; }

    public static bool LeftClick() {
        return manager.CheckLeftButtonPressed();
    }

    public static bool LeftHeld() {
        return manager.CheckLeftButtonDown();
    }

    public static bool RightClick() {
        return manager.CheckRightButtonPressed();
    }

    public static bool RightHeld() {
        return manager.CheckRightButtonDown();
    }
}