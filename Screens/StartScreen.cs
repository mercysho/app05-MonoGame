using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace App05MonoGame.Screens
{
    public class StartScreen : IUpdateableInterface, IDrawableInterface
    {
        #region fields

        private App05Game game;
        private Texture2D backgroundImage;
        private SpriteFont arialFont;

        private Button coinsButton;
        private Button asteroidsButton;

        private List<string> instructions;

        #endregion
        public StartScreen(App05Game game)
        {
            this.game = game;
            LoadContent();
        }

        public void LoadContent()
        {
            backgroundImage = game.Content.Load<Texture2D>(
                "backgrounds/green_background720p");

            CreateInstructions();

            arialFont = game.Content.Load<SpriteFont>("fonts/arial");
            
            SetupCoinsButton();
                
        }


        private void SetupCoinsButton()
        {
            coinsButton = new Button(arialFont,
                game.Content.Load<Texture2D>("Controls/button-icon-png-200"))
            {
                Position = new Vector2(1130, 580),
                Text = "Start",
                Scale = 0.6f
            };

            coinsButton.click += StartCoinsGame;
        }

        /// <summary>
        /// A short summary explaining how to play the game
        /// </summary>
        private void CreateInstructions()
        {
            instructions = new List<string>();

            instructions.Add("Player can move around by using the arrows on keyboard");
            instructions.Add("Every time the player collides with a coin, player gets a score");
            instructions.Add("A dog moves around, acts as enemy to also get coin");
            instructions.Add("Every time the dog collides with the player, player loses life");
            instructions.Add("Energy is lost player loses life");
            instructions.Add("The game is won when player collects more coins than enemy");
            instructions.Add("The game is lost when enemy collects more coins than player");
        }

        private void StartCoinsGame(object sender, System.EventArgs e)
        {
            game.GameState = GameStates.PlayingLevel1;
        }

        

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            int y = 100;
            foreach(string line in instructions)
            {
                y += 40;
                int x = 200;
                spriteBatch.DrawString(arialFont, line,
                    new Vector2(x, y), Color.White);
            }

            coinsButton.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            coinsButton.Update(gameTime);
        }
    }
}
