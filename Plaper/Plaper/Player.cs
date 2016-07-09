using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Player {

        private enum States { Standing, Jumping};

        private States state;

        const float GRAVITY = 20f;
        const float JUMP_SPEED = 50f;

        const double SCALE = 3.0;
        const int HEIGHT = (int) (17 * SCALE);
        const int WIDTH  = (int) (14 * SCALE);

        Vector2 position;
        Vector2 velocity;

        KeyboardState keyboardState;

        Texture2D texture;
        Rectangle screenBounds;

        public Player(Texture2D texture, Rectangle screenBounds) {
            this.texture = texture;
            this.screenBounds = screenBounds;

            state = States.Standing;

            SetInStartPosition();
        }

        public void Update(GameTime gTime) {
            switch (state) {
                case States.Standing:

                    keyboardState = Keyboard.GetState();

                    if (keyboardState.IsKeyDown(Keys.Space)) {
                        velocity.Y = JUMP_SPEED;
                        state = States.Jumping;
                    }

                    break;

                case States.Jumping:
                    velocity.Y -= (float) gTime.ElapsedGameTime.TotalSeconds * GRAVITY;
                    position.Y -= (float) gTime.ElapsedGameTime.TotalSeconds * velocity.Y;

                    if (position.Y + HEIGHT > screenBounds.Height) {
                        position.Y = screenBounds.Height - HEIGHT;
                        velocity = Vector2.Zero;

                        state = States.Standing;
                    }

                    break;
            }
        }

        public void SetInStartPosition() {
            position.X = screenBounds.Width / 2;
            position.Y = screenBounds.Height - HEIGHT;
        }

        public void Draw(SpriteBatch spriteBatch) {
            Rectangle posRect = new Rectangle((int) position.X, (int) position.Y, WIDTH, HEIGHT);
            spriteBatch.Draw(texture, posRect, Color.White);
        }
    }
}
