using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {

    //abstract base class for game states
    abstract class State {

        protected GraphicsDeviceManager graphics;
        protected Game1 game;
        protected Texture2D buttonTexture;

        protected int buttonSpacing;

        protected int nButtons = 0;

        protected const float TEXT_SCALE = 2.5f;
        protected static Color HOVER_COLOR = Color.White;
        protected static Color TEXT_COLOR = Color.Black;

        static State currentState = null;

        public State(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;

            buttonTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            buttonTexture.SetData(new Color[] { Color.Chocolate });
        }

        public static void setState(State state) {
            currentState = state;
        }

        public static State getState() {
            return currentState;
        }

        public virtual void Update(GameTime gameTime, Game1 game) {}

        public virtual void Draw(SpriteBatch spriteBatch) {}

    }
}
