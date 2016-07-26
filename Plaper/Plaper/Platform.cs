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

        private static Color[] colors = { Color.Red, Color.Green, Color.LightSalmon, Color.LawnGreen, Color.Orange };
        private static Random rand = new Random();

        private Color color;

        //Constructor
        public Platform(Texture2D t, Vector2 v)
                :base(t, new Rectangle((int)v.X, (int)v.Y, (int)(Plaper.height * Plaper.PLAT_SCALE * 100), (int)(Plaper.height * Plaper.PLAT_SCALE * 22))) {
            color = colors[rand.Next(0, colors.Length)];
        }

        public Platform(Texture2D texture, int startHeight)
                :base(texture, new Rectangle(0, 0, (int)(Plaper.height * Plaper.PLAT_SCALE * 100), (int)(Plaper.height * Plaper.PLAT_SCALE * 22))) {
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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(Texture, position, null, color);
            spriteBatch.End();
        }
    }
}
