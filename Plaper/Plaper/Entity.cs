using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Entity {
        //Had to change some stuff in this because I don't have visual studio 2015. Once I do, it can be changed back
        protected Texture2D Texture { get; set; }
        protected Vector2 position;

        protected int Height { get; set; }
        protected int Width  { get; set; }

        protected static bool isScrolling = false;
        protected static bool IsScrolling { get { return isScrolling; } set {isScrolling = value; } }
        static float shiftDelta;

        private static ArrayList entities = new ArrayList();

        public Entity(Texture2D texture, double scale, Vector2 position, bool scrollable = true) {
            this.Texture  = texture;
            this.position = position;
            this.Height   = (int) (scale * texture.Height);
            this.Width    = (int) (scale * texture.Width);
            
            if (scrollable) entities.Add(this);
        }

        public Entity(Texture2D texture, int height, int width, Vector2 position, bool scrollable = true) {
            this.Texture  = texture;
            this.position = position;
            this.Height   = height;
            this.Width    = width;

            if (scrollable) entities.Add(this);
        }

        public Rectangle Hitbox() {
            return new Rectangle((int)position.X, (int)position.Y, Width, Height);
        }

        public static void ScrollInit(float shiftDelta) {
            IsScrolling = true;
            Entity.shiftDelta = shiftDelta;
        }


        public static bool ScrollDown() {

            float delta = (float) (Plaper.SHIFT_SPEED * Plaper.gameTime.ElapsedGameTime.TotalSeconds);

            foreach (Entity entity in entities) {
                entity.position.Y += delta;
            }

            shiftDelta -= delta;
            IsScrolling = shiftDelta > 0;

            return IsScrolling;
        }
    }
}
