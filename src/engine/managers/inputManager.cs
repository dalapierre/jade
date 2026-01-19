using Microsoft.Xna.Framework;
using XNA = Microsoft.Xna.Framework.Input;

namespace Jade;

class InputManager {
    public XNA.KeyboardState currentState { get; private set; }
    public XNA.MouseState mouseState { get; private set; }
    XNA.KeyboardState previousState;
    bool canLeftClick;
    bool canRightClick;


    public InputManager() {
        currentState = XNA.Keyboard.GetState();
        canLeftClick = true;
        canRightClick = true;
    }

    public bool CheckKeyPressed(XNA.Keys key) {
        currentState = XNA.Keyboard.GetState();
        if (!previousState.IsKeyDown(key) && currentState.IsKeyDown(key)) {
            return currentState.IsKeyDown(key);
        }

        return false;
    }

    public bool CheckKeyDown(XNA.Keys key) {
        currentState = XNA.Keyboard.GetState();
        return currentState.IsKeyDown(key);
    }

    public bool CheckKeyUp(XNA.Keys key) {
        currentState = XNA.Keyboard.GetState();
        return currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
    }

    public bool CheckLeftButtonPressed() {
        mouseState = XNA.Mouse.GetState();
        if (mouseState.LeftButton == XNA.ButtonState.Pressed && canLeftClick) {
            canLeftClick = false;
            return true;
        }

        return false;
    }

    public bool CheckRightButtonPressed() {
        mouseState = XNA.Mouse.GetState();
        if (mouseState.RightButton == XNA.ButtonState.Pressed && canRightClick) {
            canRightClick = false;
            return true;
        }

        return false;
    }

    void CheckLeftButtonUp() {
        mouseState = XNA.Mouse.GetState();
        if (mouseState.LeftButton == XNA.ButtonState.Released) {
            canLeftClick = true;
        }
    }

    void CheckRightButtonUp() {
        mouseState = XNA.Mouse.GetState();
        if (mouseState.RightButton == XNA.ButtonState.Released) {
            canRightClick = true;
        }
    }

    public bool CheckLeftButtonDown() {
        mouseState = XNA.Mouse.GetState();
        return mouseState.LeftButton == XNA.ButtonState.Pressed;
    }

    public bool CheckRightButtonDown() {
        mouseState = XNA.Mouse.GetState();
        return mouseState.RightButton == XNA.ButtonState.Pressed;
    }

    public Vector2 GetMousePos() {
        mouseState = XNA.Mouse.GetState();
        var pos = new Vector2(mouseState.X, mouseState.Y);

        return pos;
    }

    public void Update() {
        previousState = currentState;
        CheckRightButtonUp();
        CheckLeftButtonUp();
    }

}