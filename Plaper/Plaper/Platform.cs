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

        private static Color[] colors = { Color.White, Color.Red, Color.Green };
        private static Random rand = new Random();

        private Color color;

        //Constructor
        public Platform(Texture2D t, Vector2 v)
                :base(t, Plaper.PLAT_SCALE, v) {
            color = colors[rand.Next(0, colors.Length)];
        }

        public Platform(Texture2D texture, int startHeight)
                :base(texture, 1.0, Vector2.Zero) {
            position = new Vector2((Plaper.SCREEN_WIDTH - Width)/2, Plaper.SCREEN_HEIGHT - startHeight);
            color = colors[rand.Next(0, colors.Length)];
        }

        public Vector2 Pos { get { return position; } }

        public void SetPlatform(Vector2 p) {
            color = colors[rand.Next(0, colors.Length)];
            position = p;
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, position, null, color);
        }
    }
}
