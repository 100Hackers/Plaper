using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {

    class GameState : State {

        public GameState(Player player, SpriteBatch spriteBatch) {
            this.player = player;
            this.spriteBatch = spriteBatch;
        }

        Player player;
        SpriteBatch spriteBatch;

        public override void Update(GameTime gameTime) {

            player.Update(gameTime);

        }

        public override void Draw() {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            player.Draw(spriteBatch);

            spriteBatch.End();

        }

    }

}