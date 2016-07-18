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

        Player player;

        Rectangle screenRectangle;

        //ctor
        public GameState(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;
            screenRectangle = new Rectangle(0, 0, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT);
            player = new Player(game.Sprite, game.Arrow, game.ArrowFill, screenRectangle);
        }

        //update for game logic
        public override void Update(GameTime gameTime) {

            player.Update(gameTime);

            // Quit to make menu is esc is pressed
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            player.Draw(spriteBatch);

            spriteBatch.End();

        }

    }

}