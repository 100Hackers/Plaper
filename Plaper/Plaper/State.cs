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

        static State currentState = null;

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
