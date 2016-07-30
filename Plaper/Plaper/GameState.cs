using Microsoft.Xna.Framework;
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
        Texture2D pixel;

        bool isShifting;
        public bool IsShifting { get { return isShifting; } }
        int shiftDelta;
        float preShift;
        const int SHIFT_SPEED = 300;

		//used for scoring
		Scoring Score;
		//Vector2 scorePos;
		///*static*/ int scoreCnt = 0;

        Rectangle screenRectangle;

        //ctor
        public GameState(GraphicsDeviceManager graphics, Game1 game) : base(graphics, game) {

            screenRectangle = new Rectangle(0, 0, Plaper.playWidth, Plaper.playHeight);
            player = new Player(game.Sprite, game.Arrow, game.ArrowFill, START_HEIGHT * Plaper.playHeight, screenRectangle);

			Score = new Scoring(game);

			//scorePos = new Vector2(5.0f, 5.0f);
			//scoreCnt = 0;

            platformCounter = 0;
            platforms = new Platform[3];

            isShifting = false;

            pixel = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            platforms[0] = new Platform(game.PlatformTex, START_HEIGHT * Plaper.playHeight);
            platforms[1] = new Platform(game.PlatformTex, new Vector2(rand.Next(screenRectangle.Width - 101), rand.Next(100, 300)));
            platforms[2] = new Platform(game.PlatformTex, new Vector2(rand.Next(screenRectangle.Width - 101), rand.Next(MAX_HEIGHT_DIFF - MIN_HEIGHT_DIFF) + MIN_HEIGHT_DIFF));
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
					Score.Update();
                    platformCounter %= 3;
                    isShifting = true;
                    Entity.ScrollInit((int) ((Plaper.playHeight - platforms[platformCounter].Pos.Y) - (Plaper.playHeight * Plaper.START_HEIGHT)));
                }
            } else {
                if (!Entity.ScrollDown()) {
                    isShifting = false;
                }
            }

            // Quit to make menu is esc is pressed
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

            if(player.IsDead) {
                State.setState(new EndgameState(graphics, game, Score.GetScore()));
            }
        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            player.Draw(spriteBatch);

            foreach (var plat in platforms) {
                plat.Draw(spriteBatch);
            }

            if(Plaper.debugMode) {
                foreach(var plat in platforms) {
                    DrawBorder(plat.Hitbox(), 2, spriteBatch, Color.Red);
                }
                DrawBorder(player.Hitbox(), 2, spriteBatch, Color.Red);
            }

            Score.Draw(spriteBatch);

   //         spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
			////spriteBatch.DrawString(game.font10, "Score: " + scoreCnt.ToString(), scorePos, Color.Black);
   //         spriteBatch.End();
        }

        private void generateNewPlatform() {
            Platform prevPlatform = platforms[(platformCounter + 1) % 3];
            Platform curPlatform   = platforms[(platformCounter + 2) % 3];
            

            curPlatform.SetPlatform(new Vector2(rand.Next(0, screenRectangle.Width - platforms[0].Hitbox().Width), rand.Next((int)prevPlatform.Pos.Y - MAX_HEIGHT_DIFF, (int)prevPlatform.Pos.Y - MIN_HEIGHT_DIFF) ));
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, SpriteBatch spriteBatch, Color borderColor) {

            spriteBatch.Begin();
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);

            spriteBatch.End();
        }

    }

}