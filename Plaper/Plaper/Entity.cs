using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Entity {
        protected Texture2D Texture { get; }
        protected Vector2   position;

        public Entity (Texture2D texture, Vector2 position) {
            this.Texture  = texture;
            this.position = position;
        }
    }
}
