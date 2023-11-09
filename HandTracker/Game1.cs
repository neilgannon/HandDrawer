using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HandTracker
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        Camera camera;
        BasicModel leftHand;
        BasicModel rightHand;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //approximate centimetre values
            //right hand coordinates, +Z is towards the observer (Vector3.Forward = 0,0,-1)
            camera = new Camera(new Vector3(0, 100, 500), Vector3.Forward);
            camera.Initialize();

            leftHand = new BasicModel(new Vector3(-50f, 1, 0), false);
            rightHand = new BasicModel(new Vector3(50f, 1, 0), true);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            leftHand.Initialize(Content);
            rightHand.Initialize(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            camera.UpdateView();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            leftHand.Draw(camera);
            rightHand.Draw(camera);

            base.Draw(gameTime);
        }
    }
}
