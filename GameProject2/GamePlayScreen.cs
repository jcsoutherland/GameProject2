using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace GameProject2
{
    /// <summary>
    /// A class representing the games gameplay screen
    /// </summary>
    public class GamePlayScreen
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private CoinSprite[] coins;
        private PumpkinSprite pumpkin;
        SpriteFont spriteFont;
        private int coinsLeft;

        private SoundEffect coinPickup;
        private Song gameMusic;
        public bool exit;

        /// <summary>
        /// Constructor for the gameplay screen
        /// </summary>
        /// <param name="g"></param>
        public GamePlayScreen(GraphicsDeviceManager g)
        {
            _graphics = g;
            System.Random rand = new System.Random();
            coins = new CoinSprite[]
                {
                new CoinSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height)),
                new CoinSprite(new Vector2((float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * _graphics.GraphicsDevice.Viewport.Height))
                };
            pumpkin = new PumpkinSprite();
            coinsLeft = coins.Length;
        }

        /// <summary>
        /// Loads content from ContentManager
        /// </summary>
        /// <param name="Content"></param>
        public void LoadContent(ContentManager Content)
        {
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            foreach (var coin in coins) coin.LoadContent(Content);
            pumpkin.LoadContent(Content);
            gameMusic = Content.Load<Song>("DeeYan-Key-TheGame");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(gameMusic);
            MediaPlayer.Volume = .05f;
            coinPickup = Content.Load<SoundEffect>("Pickup_Coin15");
            SoundEffect.MasterVolume = .03f;
            spriteFont = Content.Load<SpriteFont>("arial");
        }
        /// <summary>
        /// Game Loop updating screen every tick
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                exit = true;

            pumpkin.Update(gameTime);
            foreach (var coin in coins)
            {
                coin.Update(gameTime, _graphics);
                if (!coin.Collected && coin.Bounds.CollidesWith(pumpkin.Bounds))
                {
                    coinPickup.Play();
                    coin.Collected = true;
                    coinsLeft--;
                }
            }
        }
        /// <summary>
        /// Displays all assets of the screen
        /// </summary>
        /// <param name="gameTime"></param>
       public void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.MediumPurple);

            _spriteBatch.Begin();
            if (coinsLeft == 0)
            {
                _spriteBatch.DrawString(spriteFont, "You Win!", new Vector2((_graphics.GraphicsDevice.Viewport.Width / 2) - 110, (_graphics.GraphicsDevice.Viewport.Height / 2) - 96), Color.White);
                _spriteBatch.DrawString(spriteFont, "Press Esc or Back to Exit", new Vector2((_graphics.GraphicsDevice.Viewport.Width / 2) - 98, (_graphics.GraphicsDevice.Viewport.Height / 2) - 32), Color.White, 0, new Vector2(0, 0), .333f, SpriteEffects.None, 0);
            }
            else
            {
                foreach (var coin in coins) coin.Draw(gameTime, _spriteBatch);
                pumpkin.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();
        }
    }
}