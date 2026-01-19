using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Jade;

abstract class App : Game {
    protected GraphicsDeviceManager _graphics;
    SpriteBatch _spriteBatch;
    SceneManager _sceneManager;
    InputManager _inputManager;
    Renderer _renderer;
    SpriteLoader _spriteLoader;

    public App() {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = 1280,
            PreferredBackBufferHeight = 720,
        };
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public abstract void Init();
    public abstract void RegisterScenes();
    protected void AddScene<T>(T scene) where T : Scene { _sceneManager.AddScece(scene); }

    protected override void Initialize() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _sceneManager = new SceneManager();
        _inputManager = new InputManager();
        _renderer = new Renderer(_spriteBatch);
        _spriteLoader = new SpriteLoader(this);
        Keyboard.SetManager(_inputManager);
        Mouse.SetManager(_inputManager);
        RegisterServices();
        Init();
        base.Initialize();
    }

    protected override void LoadContent() {
        // Load content from files
        _spriteLoader.Initialize("sprites.json");
        RegisterScenes();
    }

    void RegisterServices() {
        Services.AddService(_graphics);
        Services.AddService(_sceneManager);
        Services.AddService(_renderer);
        Services.AddService(_spriteLoader);
        Services.AddService(new Generator());
        Services.AddService(new Mixer(this));
    }

    protected override void Update(GameTime gameTime) {
        _inputManager.Update();
        _sceneManager.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(new Color(20, 20, 20, 1));
        _sceneManager.Draw();
        base.Draw(gameTime);
    }
}
