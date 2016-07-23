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

        static State currentState = null;

        public State(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;
        }

        public static void setState(State state) {
            currentState = state;
        }

        public static State getState() {
            return currentState;
        }

        public virtual void Update(GameTime gameTime) {}

        public virtual void Draw(SpriteBatch spriteBatch) {}

    }
}
