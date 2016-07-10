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

        const int GRAVITY = 348;
        const int JUMP_SPEED = 152;

        const double SCALE = 3.0;
        const int HEIGHT = (int) (17 * SCALE);
        const int WIDTH  = (int) (14 * SCALE);

        Vector2 position;
        Vector2 velocity;

        KeyboardState keyboardState;

        Texture2D texture;
        Rectangle screenBounds;

        double arrowAngle;
        bool   arrowGoingLeft;
        Texture2D arrowTexture;
        const int ARROW_HEIGHT = 64;
        const int ARROW_WIDTH  = 32;
        const int ARROW_PADING = 75;
        const int ARROW_SPEED  = 3;
        const double ARROW_BOUND_UPPER = 0;
        const double ARROW_BOUND_LOWER = -Math.PI;

        public Player(Texture2D texture, Texture2D arrowTexture, Rectangle screenBounds) {
            this.texture = texture;
            this.arrowTexture = arrowTexture;
            this.screenBounds = screenBounds;

            state = States.Standing;
            arrowAngle = 0.0;
            arrowGoingLeft = false;

            SetInStartPosition();
        }

        public void Update(GameTime gameTime) {
            double elapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;

            switch (state) {
                case States.Standing:

                    keyboardState = Keyboard.GetState();

                    // rotate arrow
                    double arrowDelta = elapsedSeconds * ARROW_SPEED;
                    arrowAngle += arrowGoingLeft ? -arrowDelta : arrowDelta;

                    // check bounds
                    if (arrowAngle < ARROW_BOUND_LOWER || ARROW_BOUND_UPPER < arrowAngle) {
                        // limit to bounds
                        arrowAngle = arrowGoingLeft ? ARROW_BOUND_LOWER : ARROW_BOUND_UPPER;
                        // reverse direction
                        arrowGoingLeft = !arrowGoingLeft;
                    }

                    // jump if space is hit
                    if (keyboardState.IsKeyDown(Keys.Space)) {
                        velocity.Y = JUMP_SPEED;
                        state = States.Jumping;
                    }

                    break;

                case States.Jumping:
                    velocity.Y -= (float) (elapsedSeconds * GRAVITY);
                    position.Y -= (float) (elapsedSeconds * velocity.Y);

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
            // draw character
            Rectangle posRect = new Rectangle((int) position.X, (int) position.Y, WIDTH, HEIGHT);
            spriteBatch.Draw(texture, posRect, Color.White);

            //draw arrow
            var arrowRect = new Rectangle();
            arrowRect.X = (int) (ARROW_PADING * Math.Cos(arrowAngle) + position.X);
            arrowRect.Y = (int) (ARROW_PADING * Math.Sin(arrowAngle) + position.Y);
            arrowRect.Height = ARROW_HEIGHT;
            arrowRect.Width = ARROW_WIDTH;

            spriteBatch.Draw(arrowTexture, arrowRect, Color.White);

        }
    }
}
