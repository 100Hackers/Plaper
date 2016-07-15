using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {

    class GameState : State {

        public GameState(Player player, GameTime gameTime, SpriteBatch spriteBatch) {
            this.player = player;
            this.gameTime = gameTime;
            this.spriteBatch = spriteBatch;
        }

        public GameState(Player player, SpriteBatch spriteBatch) {
            this.player = player;
            this.spriteBatch = spriteBatch;
        }

        Player player;
        GameTime gameTime;
        SpriteBatch spriteBatch;

        public override void updateGameTime(GameTime gameTime) {
            this.gameTime = gameTime;
        }

        public override void tick() {

        }

        public override void render() {

            player.Update(gameTime);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            player.Draw(spriteBatch);

            spriteBatch.End();

        }

    }

}