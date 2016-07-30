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
        protected Texture2D Texture { get; }
        protected Vector2 position;

        protected int Height { get; }
        protected int Width  { get; }

        protected static bool IsScrolling { get; private set; } = false;
        static float shiftDelta;

        private static ArrayList entities = new ArrayList();

        public Entity(Texture2D texture, Rectangle rectangle, bool scrollable = true) {
            this.Texture  = texture;

            this.position = new Vector2(rectangle.X, rectangle.Y);
            this.Height = rectangle.Height;
            this.Width  = rectangle.Width;

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

            float delta = (float) (Plaper.SCROLL_SPEED * Plaper.playHeight * Plaper.gameTime.ElapsedGameTime.TotalSeconds);

            foreach (Entity entity in entities) {
                entity.position.Y += delta;
            }

            shiftDelta -= delta;
            IsScrolling = shiftDelta > 0;

            return IsScrolling;
        }
    }
}
