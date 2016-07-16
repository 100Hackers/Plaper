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
    class MenuState : State {

        Player player;
        Rectangle settingsButtonPosition;
        Texture2D menuButtonTexture;
        Rectangle startButtonPosition;
        GraphicsDeviceManager graphics;

        //ctor
        public MenuState(Player player, GraphicsDeviceManager graphics) {
            this.player = player;
            this.graphics = graphics;
            menuButtonTexture = new Texture2D(graphics.GraphicsDevice, 20, 10);
            Color[] startTextureData = new Color[20*10];
            for (int i = 0; i < startTextureData.Length; i++) {
                startTextureData[i] = Color.Chocolate;
            }
            menuButtonTexture.SetData(startTextureData);
            settingsButtonPosition = new Rectangle(0, Game1.SCREEN_HEIGHT-(Game1.SCREEN_HEIGHT/10)-Game1.SCREEN_HEIGHT/5, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT/5);
            startButtonPosition = new Rectangle(0, settingsButtonPosition.Y-settingsButtonPosition.Height-(Game1.SCREEN_HEIGHT/10), Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT/5);
        }

        //update for game logic
        public override void Update(GameTime gameTime) {

            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            if(mouseState.LeftButton == ButtonState.Pressed) {
                if(startButtonPosition.Contains(mousePosition)) {
                    State.setState(new GameState(player, graphics));
                }
            }
        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            spriteBatch.Draw(menuButtonTexture, startButtonPosition, Color.White);

            spriteBatch.Draw(menuButtonTexture, settingsButtonPosition, Color.White);

            spriteBatch.End();

        }
    }
}