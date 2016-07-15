﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    abstract class State {

        static State currentState = null;

        public static void setState(State state) {
            currentState = state;
        }

        public static State getState() {
            return currentState;
        }

        public virtual void updateGameTime(GameTime gameTime) {}

        public virtual void tick() {}

        public virtual void render() {}

    }
}
