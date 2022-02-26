using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject2
{
    /// <summary>
    /// A default class to control game screens
    /// </summary>
    public class Default : Game
    {
        private GraphicsDeviceManager _graphics;

        private MenuScreen menu;
        private GamePlayScreen game;

        /// <summary>
        /// Constructor For Default
        /// </summary>
        public Default()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initializes game screens
        /// </summary>
        protected override void Initialize()
        {
            menu = new MenuScreen(_graphics);
            game = new GamePlayScreen(_graphics);
            base.Initialize();
        }

        /// <summary>
        /// Loads the content for each game screen
        /// </summary>
        protected override void LoadContent()
        {
            menu.LoadContent(Content);
            game.LoadContent(Content);
        }

        /// <summary>
        /// Determines what screen's game loop to enter, then loops.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (menu.play)
            {
                game.Update(gameTime);
            }
            else
            {
                game.exit = false;
                menu.play = false;
                menu.Update(gameTime);
            }
            if(menu.exit || game.exit)
            {
                Exit();
            }
        }
        /// <summary>
        /// Determines which screen to draw
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            if (menu.play)
            {
                game.Draw(gameTime);
            }
            else
            {
                game.exit = false;
                menu.play = false;
                menu.Draw(gameTime);
            }
        }
    }
}
