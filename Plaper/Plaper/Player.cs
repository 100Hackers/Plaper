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

        const double PI = Math.PI;
        const int GRAVITY = 400;
        const int JUMP_SPEED = 300;

        const double SCALE = 3.0;
        const int HEIGHT = (int) (17 * SCALE);
        const int WIDTH  = (int) (14 * SCALE);

        Vector2 position;
        Vector2 velocity;

        KeyboardState keyboardState;
        KeyboardState lastState;

        Texture2D texture;
        Rectangle screenBounds;

        double arrowAngle;
        bool   arrowGoingLeft;
        Texture2D arrowTexture;
        const int ARROW_HEIGHT = 64;
        const int ARROW_WIDTH  = 32;
        const int ARROW_PADING = 90;
        const int ARROW_SPEED  = 3;
        const double ARROW_BOUND_UPPER = PI / 2;
        const double ARROW_BOUND_LOWER = -PI / 2;

        public Player(Texture2D texture, Texture2D arrowTexture, Rectangle screenBounds) {
            this.texture = texture;
            this.arrowTexture = arrowTexture;
            this.screenBounds = screenBounds;

            state = States.Standing;
            arrowAngle = 0.0;
            arrowGoingLeft = false;

            lastState = Keyboard.GetState();

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

                    // jump if space is hit; make sure it has been released since last jump
                    bool spacePressed  = keyboardState.IsKeyDown(Keys.Space);
                    bool spaceReleased = !lastState.IsKeyDown(Keys.Space);

                    if (spacePressed && spaceReleased) {
                        velocity.Y = (float) (Math.Cos(arrowAngle) * JUMP_SPEED);
                        velocity.X = (float) (Math.Sin(-arrowAngle) * JUMP_SPEED);
                        state = States.Jumping;
                    }

                    lastState = keyboardState;
                    break;

                case States.Jumping:
                    velocity.Y -= (float) (elapsedSeconds * GRAVITY);
                    position.Y -= (float) (elapsedSeconds * velocity.Y);

                    if (position.X < 0 || screenBounds.Width < position.X + WIDTH) {
                        velocity.X = -velocity.X;
                        position.X = position.X < 0 ? 0f : screenBounds.Width - WIDTH;
                    }
                    position.X -= (float) (elapsedSeconds * velocity.X);

                    if (position.Y + HEIGHT > screenBounds.Height) {
                        position.Y = screenBounds.Height - HEIGHT;
                        velocity = Vector2.Zero;

                        arrowAngle = 0;
                        arrowGoingLeft = false;

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
            if (state == States.Standing) {
                var arrowRect = new Rectangle();
                arrowRect.X = (int)(ARROW_PADING * Math.Cos(arrowAngle - PI / 2) + position.X + WIDTH / 2 - ARROW_WIDTH / 2 * Math.Cos(arrowAngle));
                arrowRect.Y = (int)(ARROW_PADING * Math.Sin(arrowAngle - PI / 2) + position.Y + 5*SCALE - ARROW_WIDTH / 2 * Math.Sin(arrowAngle));
                arrowRect.Height = ARROW_HEIGHT;
                arrowRect.Width = ARROW_WIDTH;

                spriteBatch.Draw(arrowTexture, arrowRect, null, Color.White, (float)arrowAngle, Vector2.Zero, SpriteEffects.None, 0);
            }
        }
    }
}
