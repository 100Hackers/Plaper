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
        protected Vector2 position;

        protected int Height { get; }
        protected int Width  { get; }


        public Entity(Texture2D texture, double scale, Vector2 position) {
            this.Texture  = texture;
            this.position = position;
            this.Height   = (int) (scale * texture.Height);
            this.Width    = (int) (scale * texture.Width);
        }

        public Rectangle Hitbox() {
            return new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        //public void ScrollDown
    }
}
