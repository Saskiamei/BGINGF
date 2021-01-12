using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace BGINGF
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Scrolling scrolling1;
        Scrolling scrolling2;


        Texture2D playerTxr, platformSheetTxr, mushroomTxr, whiteBox;
        
        SpriteFont uiFont, heartFont, bigFont;
        SoundEffect mushroomSound, deadSound;
        Point screenSize = new Point(800, 450);
        int levelNumber = 0;
        PlayerSprite playerSprite;
        MushroomSprite mushroomSprite;

       

        List<List<PlatformSprite>> Levels = new List<List<PlatformSprite>>();
        
        List<Vector2> mushrooms = new List<Vector2>();



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           

            scrolling1 = new Scrolling(Content.Load<Texture2D>("Background 1"), new Rectangle(0, 0, 800, 450));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Background2"), new Rectangle(800, 0, 800, 450));

            playerTxr = Content.Load<Texture2D>("wizardPlayer");
            mushroomTxr = Content.Load<Texture2D>("mushroom");
            platformSheetTxr = Content.Load<Texture2D>("bgingf");
            uiFont = Content.Load<SpriteFont>("UIFont");
            bigFont = Content.Load<SpriteFont>("BigFont");
            heartFont = Content.Load<SpriteFont>("HeartFont");
            mushroomSound = Content.Load<SoundEffect>("Capture");
            deadSound = Content.Load<SoundEffect>("Died");

            

            whiteBox = new Texture2D(GraphicsDevice, 1, 1);
            whiteBox.SetData(new[] { Color.White });

            playerSprite = new PlayerSprite(playerTxr, whiteBox, new Vector2(100, 50), deadSound);
            mushroomSprite = new MushroomSprite(mushroomTxr, whiteBox, new Vector2(550, 200));
            BuildLevels(); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
          

            //scrolling background
            if (scrolling1.rectangle.X + scrolling1.texture.Width <= 0)
                scrolling1.rectangle.X = scrolling2.rectangle.X + scrolling2.texture.Width;
            if (scrolling2.rectangle.X + scrolling2.texture.Width <= 0)
                scrolling2.rectangle.X = scrolling1.rectangle.X + scrolling1.texture.Width;

            scrolling1.Update();
            scrolling2.Update();

            
            //player sprite, levels and cllision checks

            playerSprite.Update(gameTime, Levels[levelNumber]);
            if (playerSprite.spritePos.Y > screenSize.Y + 50)
            {
                playerSprite.lives--;
                if (playerSprite.lives <= 0)
                {
                    playerSprite.lives = 3;
                    levelNumber = 0 ;
                }
                playerSprite.ResetPlayer(new Vector2(50, 50));
            }

            if (playerSprite.checkCollision(mushroomSprite))
            {
                levelNumber++;
                if (levelNumber >= Levels.Count) levelNumber = 0;
                mushroomSprite.spritePos = mushrooms[levelNumber];
                playerSprite.ResetPlayer(new Vector2(100, 50));
                mushroomSound.Play();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            scrolling1.Draw(_spriteBatch);
            scrolling2.Draw(_spriteBatch);
            playerSprite.Draw(_spriteBatch, gameTime);
            mushroomSprite.Draw(_spriteBatch, gameTime);
            foreach (PlatformSprite platform in Levels[levelNumber]) platform.Draw(_spriteBatch, gameTime);

            string livesString = "";
            if (playerSprite.lives == 3) livesString += "bim";
            else if (playerSprite.lives == 2) livesString += "im";
            else if (playerSprite.lives == 1) livesString += "m";
            else livesString = "";

            _spriteBatch.DrawString(heartFont, livesString, new Vector2(15, 10), Color.White);


            _spriteBatch.DrawString(
                uiFont, "Level " + (levelNumber + 1),
                new Vector2(screenSize.X - 15 - uiFont.MeasureString("level" + levelNumber + 1).X, 5),
                Color.White);

            if (playerSprite.lives <= 0)
            {
                Vector2 textSize = bigFont.MeasureString("GAME OVER");

                _spriteBatch.DrawString(
                    bigFont,
                    "GAME OVER",
                    new Vector2((screenSize.X / 2) - (textSize.X / 2) + 8, (screenSize.Y / 2) - (textSize.Y / 2) + 8),
                    Color.Black);

                _spriteBatch.DrawString(
                    bigFont,
                    "GAME OVER",
                    new Vector2((screenSize.X / 2) - (textSize.X / 2), (screenSize.Y / 2) - (textSize.Y / 2)),
                    Color.White);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void BuildLevels()
        {
            Levels.Add(new List<PlatformSprite>());
            // Level 1
            Levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 300)));
            Levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 300)));
            Levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(375, 250)));
            Levels[0].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(500, 200)));
            mushrooms.Add(new Vector2(550, 200));

            //Level 2
            Levels.Add(new List<PlatformSprite>());
            Levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 200)));
            Levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 350)));
            Levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(400, 350)));
            Levels[1].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(550, 350)));
            mushrooms.Add(new Vector2(600, 350));

            //Lvel 3
            Levels.Add(new List<PlatformSprite>());
            Levels[2].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(150, 200)));
            Levels[2].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(400, 250)));
            Levels[2].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(300, 225)));
            mushrooms.Add(new Vector2(300, 150));

            //Level 4
            Levels.Add(new List<PlatformSprite>());
            Levels[3].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(500, 200)));
            Levels[3].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(375, 250)));
            Levels[3].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 300)));
            Levels[3].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 400)));
            mushrooms.Add(new Vector2(550, 200));

            //Level 5
            Levels.Add(new List<PlatformSprite>());
            Levels[4].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(200, 150)));
            Levels[4].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(350, 100)));
            Levels[4].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 100)));
            Levels[4].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 150)));
            mushrooms.Add(new Vector2(550, 150));

            //Level 6
            Levels.Add(new List<PlatformSprite>());
            Levels[5].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(200, 150)));
            Levels[5].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(300, 100)));
            Levels[5].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(250, 100)));
            Levels[5].Add(new PlatformSprite(platformSheetTxr, whiteBox, new Vector2(100, 150)));
            mushrooms.Add(new Vector2(550, 150));

        }
    }
}
