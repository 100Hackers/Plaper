using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper
{
    class Platform
    {
        //Data Members
        Rectangle screenBounds;
        private Texture2D tex;
        private Vector2 pos;
        const double SCALE = 3.0;
        const int HEIGHT = (int)(24);
        const int WIDTH = (int)(101);

        //Bounding Box
        public Rectangle BoundingBox
        {
            get { return new Rectangle((int)pos.X, (int)pos.Y, WIDTH, HEIGHT); }
        }

        //Constructor
        public Platform(Texture2D t, Vector2 v, Rectangle screenBounds) {
            tex = t;
            pos = v;
            this.screenBounds = screenBounds;
        }

        public Platform(Texture2D texture, int startHeight, Rectangle screenBounds) {
            this.tex = texture;
            this.screenBounds = screenBounds;

            this.pos = new Vector2((screenBounds.Width - WIDTH)/2, screenBounds.Height - startHeight);
        }

        //Properties
        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public void SetPlatform(Vector2 position) {
            this.pos = position;
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos);
        }
    }
}
