using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;


namespace App05MonoGame.Controllers
{
    /// <summary>
    /// Could be used for three different coloured coins
    /// </summary>
    public enum CoinColours
    {
        copper = 100,
        Silver = 200,
        Gold = 500
    }

    /// <summary>
    /// This class creates a list of coins which
    /// can be updated and drawn and checked for
    /// collisions with the player sprite
    /// </summary>
    /// <authors>
    /// Mercy Sholola
    /// </authors>
    public class CoinsController : IUpdateableInterface, 
        IDrawableInterface, ICollideableInterface
    {
        public const int Max_Coins = 15;

        private App05Game game;

        private Texture2D copperCoinSheet;
        private Texture2D silverCoinSheet;
        private Texture2D goldCoinSheet;

        private readonly List<AnimatedSprite> Coins;

        private Animation copperAnimation;
        private Animation silverAnimation;
        private Animation goldAnimation;

        private Random random;

        /// <summary>
        /// Create a new list of coins with one copper coin
        /// </summary>
        public CoinsController(App05Game game)
        {
            this.game = game;
            Coins = new List<AnimatedSprite>();

            copperCoinSheet = game.Content.Load<Texture2D>("Actors/coin_copper");
            silverCoinSheet = game.Content.Load<Texture2D>("Actors/coin_silver");
            goldCoinSheet = game.Content.Load<Texture2D>("Actors/coin_gold");


            CreateAnimations();
        }

        /// <summary>
        /// Create an animated sprite of a copper coin
        /// which could be collected by the player for a score
        /// </summary>
        public void CreateAnimations()
        {
            SoundController.PlaySoundEffect(Sounds.Coins);

            copperAnimation = new Animation(
                game.Graphics,"coin", copperCoinSheet, 8);
            
            silverAnimation = new Animation(
              game.Graphics, "coin", silverCoinSheet, 8);
            
            goldAnimation = new Animation(
              game.Graphics, "coin", goldCoinSheet, 8);
            
            CreateCoins();
        }
        private void CreateCoins()
        {
            Animation animation;

            Random random = new Random();
            
            for (int i = 0; i < Max_Coins; i ++)
            {
                int color = random.Next(0, 3) +1;
                switch (color)
                {
                    case 1: animation = copperAnimation;
                        break;
                    case 2: animation = silverAnimation;
                        break;
                    case 3: animation = goldAnimation;
                        break;
                        default: animation = goldAnimation;
                        break;
                }
                int x = random.Next(10, 1200);
                int y = random.Next(10, 700);

                AnimatedSprite coin = new AnimatedSprite()
                {
                    Animation = animation,
                    Image = animation.FirstFrame,
                    Scale = 2.0f,
                    Position = new Vector2(x, y),
                    Speed = 0,
                };

                Coins.Add(coin);
            }
        }


        /// <summary>
        /// If the sprite collides with a coin the coin becomes
        /// invisible and inactive.  A sound is played
        /// </summary>
        public void DetectCollision(Sprite sprite)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                if (coin.HasCollided(sprite) && coin.IsAlive)
                {
                    SoundController.PlaySoundEffect(Sounds.Coins);

                    coin.IsActive = false;
                    coin.IsAlive = false;
                    coin.IsVisible = false;
                }
            }           
        }

        public void Update(GameTime gameTime)
        {
            // TODO: create more coins every so often??
            // or recyle collected coins

            foreach(AnimatedSprite coin in Coins)
            {
                coin.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                coin.Draw(spriteBatch, gameTime);
            }
        }
    }
}
