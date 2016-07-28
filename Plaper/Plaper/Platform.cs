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
        //Data Members;

        private static Color[] colors = { Color.Red, Color.Green, Color.LightSalmon, Color.LawnGreen, Color.Orange };
        private static Random rand = new Random();

        private Color color;

        //Constructor
        public Platform(Texture2D t, Vector2 v)
                :base(t, new Rectangle((int)v.X, (int)v.Y, (int)(Plaper.playHeight * Plaper.PLAT_SCALE), (int)(Plaper.playHeight * Plaper.PLAT_SCALE * Plaper.PLAT_RATIO))) {
            color = Color.White;
        }

        public Platform(Texture2D texture, int startHeight)
                :base(texture, new Rectangle(0, 0, (int)(Plaper.playHeight * Plaper.PLAT_SCALE), (int)(Plaper.playHeight * Plaper.PLAT_SCALE * Plaper.PLAT_RATIO))) {
            position = new Vector2((Plaper.playWidth - Width)/2, (float) (Plaper.playHeight * (1 - Plaper.START_HEIGHT)));
            color = Color.White;
        }

        public Vector2 Pos { get { return position; } }

        public void SetPlatform(Vector2 p) {
            color = colors[rand.Next(0, colors.Length)];
            position = p;
        }

        //Draw
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(Texture, Hitbox(), null, Color.White);
            spriteBatch.End();
        }
    }
}
