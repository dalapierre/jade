using Microsoft.Xna.Framework.Input;

namespace Jade;

class Keyboard {

    static InputManager manager;

    public static void SetManager(InputManager _manager) { manager = _manager; }

    public static bool KeyPressed(Keys key) {
        return manager.CheckKeyPressed(key);
    }

    public static bool KeyDown(Keys key) {
        return manager.CheckKeyDown(key);
    }

    public static bool KeyUp(Keys key) {
        return manager.CheckKeyUp(key);
    }
}