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
    class Platform : Entity
    {
        //Data Members
        Rectangle screenBounds;

        //Constructor
        public Platform(Texture2D t, Vector2 v)
                :base(t, Plaper.PLAT_SCALE, v) { }

        public Platform(Texture2D texture, int startHeight)
                :base(texture, 1.0, Vector2.Zero) {
            position = new Vector2((Plaper.SCREEN_WIDTH - Width)/2, Plaper.SCREEN_HEIGHT - startHeight);
        }

        public Vector2 Pos { get { return position; } }

        public void SetPlatform(Vector2 p) {
            position = p;
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, position, null, Color.White);
        }
    }
}
