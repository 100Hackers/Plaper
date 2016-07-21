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

        GraphicsDeviceManager graphics;
        Game1 game;

        Random rand = new Random();

        Player player;
        Platform[] platforms;
        int platformCounter;
        const int START_HEIGHT = 30;
        const int MAX_HEIGHT_DIFF = 400;
        const int MIN_HEIGHT_DIFF = 300;

        bool isShifting;
        int shiftDelta;
        float preShift;
        const int SHIFT_SPEED = 300;

        Rectangle screenRectangle;

        //ctor
        public GameState(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;
            screenRectangle = new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT);
            player = new Player(game.Sprite, game.Arrow, game.ArrowFill, START_HEIGHT, screenRectangle);

            platformCounter = 0;
            platforms = new Platform[3];

            isShifting = false;

            platforms[0] = new Platform(game.PlatformTex, START_HEIGHT, screenRectangle);
            platforms[1] = new Platform(game.PlatformTex, new Vector2(rand.Next(screenRectangle.Width - 101), rand.Next(100, 300)), screenRectangle);
            platforms[2] = new Platform(game.PlatformTex, new Vector2(rand.Next(screenRectangle.Width - 101), rand.Next(MAX_HEIGHT_DIFF - MIN_HEIGHT_DIFF) + MIN_HEIGHT_DIFF), screenRectangle);
            generateNewPlatform();
        }

        //update for game logic
        public override void Update(GameTime gameTime) {

            if (!isShifting) {
                if (player.Update(gameTime, platforms, platformCounter)) {
                    generateNewPlatform();
                    ++platformCounter;
                    platformCounter %= 3;
                    isShifting = true;
                    shiftDelta = (int) (600 - platforms[platformCounter].Pos.Y) - START_HEIGHT;
                    preShift = platforms[platformCounter].Pos.Y;
                }
            } else {
                if (shiftPlatforms(gameTime)) {
                    isShifting = false;
                }
                
            }

            // Quit to make menu is esc is pressed
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            player.Draw(spriteBatch);

            foreach (var plat in platforms) {
                plat.Draw(spriteBatch);
            }

            spriteBatch.End();

        }

        private bool shiftPlatforms(GameTime gameTime) {
            float delta = (int) (SHIFT_SPEED * gameTime.ElapsedGameTime.TotalSeconds);

            foreach (Platform p in platforms) {
                p.SetPlatform(new Vector2(p.Pos.X, p.Pos.Y + delta));
            }

            player.SetPlayer(new Vector2(player.posRect.X, player.posRect.Y + delta));

            preShift += delta;
            return preShift > screenRectangle.Height - START_HEIGHT;
        }

        private void generateNewPlatform() {
            Platform prevPlatform = platforms[(platformCounter + 1) % 3];
            Platform curPlatform   = platforms[(platformCounter + 2) % 3];
            

            curPlatform.SetPlatform(new Vector2(rand.Next(0, screenRectangle.Width - 101), rand.Next((int)prevPlatform.Pos.Y - MAX_HEIGHT_DIFF, (int)prevPlatform.Pos.Y - MIN_HEIGHT_DIFF) ));
        }

    }

}