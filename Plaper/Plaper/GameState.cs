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

        Player player;
        GraphicsDeviceManager graphics;

        //ctor
        public GameState(Player player, GraphicsDeviceManager graphics) {
            this.player = player;
            this.graphics = graphics;
        }

        //update for game logic
        public override void Update(GameTime gameTime) {

            player.Update(gameTime);

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(player, Game1.font, graphics));
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