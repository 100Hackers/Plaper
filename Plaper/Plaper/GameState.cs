using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {

    //game state class
    class GameState : State {

        Random rand = new Random();

        Player player;
        Platform[] platforms;
        int platformCounter;
        const int START_HEIGHT = 30;
        const int MAX_HEIGHT_DIFF = 400;
        const int MIN_HEIGHT_DIFF = 300;
        Texture2D sprite, arrow, arrowFill, platform;
        SpriteFont scoreFont;

        bool isShifting;
        public bool IsShifting { get { return isShifting; } }
        const int SHIFT_SPEED = 300;

		//used for scoring
		Vector2 scorePos;
		/*static*/ int scoreCnt = 0;

        Rectangle screenRectangle;

        //ctor
        public GameState(GraphicsDeviceManager graphics, ContentManager content) : base(graphics, content) {
            scoreFont = content.Load<SpriteFont>("joystik24");

            screenRectangle = new Rectangle(0, 0, Plaper.SCREEN_WIDTH, Plaper.SCREEN_HEIGHT);

            sprite = content.Load<Texture2D>("bouncer_all");
            arrow = content.Load<Texture2D>("arrow");
            arrowFill = content.Load<Texture2D>("arrow_fill");
            platform = content.Load<Texture2D>("platform");


            player = new Player(sprite, arrow, arrowFill, START_HEIGHT, screenRectangle);

			scorePos = new Vector2(5.0f, 5.0f);
			scoreCnt = 0;

            platformCounter = 0;
            platforms = new Platform[3];

            isShifting = false;

            platforms[0] = new Platform(platform, START_HEIGHT);
            platforms[1] = new Platform(platform, new Vector2(rand.Next(screenRectangle.Width - 101), rand.Next(100, 300)));
            platforms[2] = new Platform(platform, new Vector2(rand.Next(screenRectangle.Width - 101), rand.Next(MAX_HEIGHT_DIFF - MIN_HEIGHT_DIFF) + MIN_HEIGHT_DIFF));
            generateNewPlatform();
        }

		public int GetScore() {
			return platformCounter;
		}

        //update for game logic
        public override void Update(GameTime gameTime, Game1 game) {


            if (!isShifting) {
                if (player.Update(gameTime, platforms, platformCounter, game)) {
                    generateNewPlatform();
                    ++platformCounter;
					++scoreCnt;
                    platformCounter %= 3;
                    isShifting = true;
                    Entity.ScrollInit((600 - platforms[platformCounter].Pos.Y) - START_HEIGHT);
                }
            } else {
                if (!Entity.ScrollDown()) {
                    isShifting = false;
                }
            }

            // Quit to make menu is esc is pressed
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, content));
            }

            if(player.IsDead) {
                State.setState(new EndgameState(graphics, content, scoreCnt));
            }
        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            player.Draw(spriteBatch);

            foreach (var plat in platforms) {
                plat.Draw(spriteBatch);
            }

			spriteBatch.DrawString(scoreFont, "Score: " + scoreCnt.ToString(), scorePos, Color.Black);

            spriteBatch.End();

        }

        private void generateNewPlatform() {
            Platform prevPlatform = platforms[(platformCounter + 1) % 3];
            Platform curPlatform   = platforms[(platformCounter + 2) % 3];
            

            curPlatform.SetPlatform(new Vector2(rand.Next(0, screenRectangle.Width - 101), rand.Next((int)prevPlatform.Pos.Y - MAX_HEIGHT_DIFF, (int)prevPlatform.Pos.Y - MIN_HEIGHT_DIFF) ));
        }

    }

}