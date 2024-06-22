using Gum.DataTypes;
using Gum.Managers;
using GumRuntime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RenderingLibrary;
using System.Linq;

namespace GumExample
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ScreenSave _gumScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.ApplyChanges();
            SystemManagers.Default = new SystemManagers();
            SystemManagers.Default.Initialize(GraphicsDevice, fullInstantiation: true);
            var gumProject = GumProjectSave.Load($"core/gum/gum_project_0.gumx");
            ObjectFinder.Self.GumProjectSave = gumProject;
            gumProject.Initialize();
            _gumScreen = gumProject.Screens.Where(x => x.Name == "screen_0").First();
            _gumScreen.ToGraphicalUiElement(SystemManagers.Default, addToManagers: true);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SystemManagers.Default.Activity(gameTime.TotalGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SystemManagers.Default.Draw();
            base.Draw(gameTime);
        }
    }
}
