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

        private enum States { Standing, Power, Jumping, Dead }; // Possible states Player can be in
        private States state; // Current state

        const double PI = Math.PI;  // Too lazy to keep typing Math.PI
        const int GRAVITY = 400;    // Subtracted from velocity.X
        const int JUMP_SPEED = 11;  // Initial upwards velocity.Y

        const double SCALE = 3.0;   // How much to scale player sprite by
        const int HEIGHT = (int) (17 * SCALE);
        const int WIDTH  = (int) (14 * SCALE);

        Vector2 position;   // Position and velocity of player
        Vector2 velocity;

        KeyboardState keyboardState; // Current state of keyboard, and state from last update
        KeyboardState lastState;

        Texture2D sprite; // Player texture

        Rectangle screenBounds; 

        double arrowAngle;      // Used for current angle of the arrow
        bool   arrowGoingLeft;  // Controls direction of arrow
        Texture2D arrowTexture; // Outline of arrow
        Texture2D arrowFill;    // Fills in arrow
        double arrowPower;      // Used for current arrow power
        bool   powerInc;        // Controls whether power increasing or decreasing

        const int ARROW_HEIGHT = 64;    // Arrow constants
        public Rectangle posRect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, WIDTH, HEIGHT);
            }
        }
        const int ARROW_WIDTH  = 32;
        const int ARROW_PADING = 90;    // Distance from player's head
        const int ARROW_SPEED  = 2;
        const double ARROW_BOUND_UPPER = (PI / 3);
        const double ARROW_BOUND_LOWER = (-PI / 3);

        public Player(Texture2D texture, Texture2D arrowTexture, Texture2D arrowFill, int startHeight, Rectangle screenBounds) {
            this.sprite = texture;
            this.arrowFill = arrowFill;
            this.arrowTexture = arrowTexture;
            this.screenBounds = screenBounds;

            this.position = new Vector2((screenBounds.Width - WIDTH) / 2, screenBounds.Height - startHeight - HEIGHT);

            // Initial conditions of the game
            state = States.Standing;
            arrowAngle = 0.0;
            arrowPower = ARROW_HEIGHT;
            arrowGoingLeft = false;

            lastState = Keyboard.GetState();
        }

        public bool Update(GameTime gameTime, Platform[] platforms, int curPlatform) {
            // time since Update was last called
            double elapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;

            bool onNextPlat = false;

            // See which state we are in
            switch (state) {
                case States.Standing:

                    keyboardState = Keyboard.GetState();

                    // Rotate arrow
                    double arrowDelta = elapsedSeconds * ARROW_SPEED;
                    arrowAngle += arrowGoingLeft ? -arrowDelta : arrowDelta;

                    // Check arrow bounds
                    if (arrowAngle < ARROW_BOUND_LOWER || ARROW_BOUND_UPPER < arrowAngle) {
                        // limit to bounds
                        arrowAngle = arrowGoingLeft ? ARROW_BOUND_LOWER : ARROW_BOUND_UPPER;
                        // reverse direction
                        arrowGoingLeft = !arrowGoingLeft;
                    }

                    // Move to Power state to get jump power if space is pressed
                    bool spacePressed  = keyboardState.IsKeyDown(Keys.Space);
                    if (spacePressed) {
                        powerInc = true;
                        state = States.Power;
                    }

                    lastState = keyboardState;
                    break;

                case States.Power:
                    bool spaceReleased = !Keyboard.GetState().IsKeyDown(Keys.Space);
                    
                    // Move power up and down
                    arrowPower = arrowPower + elapsedSeconds * (powerInc ? -100 : 100);

                    // Check power bounds (full and empty)
                    if (arrowPower < 0 || ARROW_HEIGHT < arrowPower) {
                        arrowPower = powerInc ? 0 : ARROW_HEIGHT;
                        powerInc = !powerInc;
                    }

                    // Jump once space is released and move to Jumping state
                    if (spaceReleased) {
                        velocity.Y = (float) (Math.Cos(arrowAngle)  * (ARROW_HEIGHT - arrowPower) * JUMP_SPEED);
                        velocity.X = (float) (Math.Sin(-arrowAngle) * (ARROW_HEIGHT - arrowPower) * JUMP_SPEED);
                        arrowPower = ARROW_HEIGHT;
                        state = States.Jumping;
                    }
                    break;

                case States.Jumping:
                    // Subtract Gravity from vertical velocity and add velocity to position
                    velocity.Y -= (float) (elapsedSeconds * GRAVITY);
                    position.Y -= (float) (elapsedSeconds * velocity.Y);

                    // Check for wall colissions for add horizontal velocity to position
                    if (position.X < 0 || screenBounds.Width < position.X + WIDTH) {
                        velocity.X = -velocity.X;
                        position.X = position.X < 0 ? 0f : screenBounds.Width - WIDTH;
                    }
                    position.X -= (float) (elapsedSeconds * velocity.X);

                    // Check for player hitting the bottom of the screen
                    // If so, go back to Standing state and reset arrow
                    if (position.Y + HEIGHT > screenBounds.Height) {
                        position.Y = screenBounds.Height - HEIGHT;
                        velocity = Vector2.Zero;

                        arrowAngle = 0;
                        arrowGoingLeft = false;

                        state = States.Dead;
                    }
                    break;

                case States.Dead:
                    position = Vector2.Zero;
                    break;
            }

            //Platform collision detection
            for (int i = 0; i < platforms.Length; i++) {

                //makes platform smaller so the player has to be more centered to land on platform.
                if(!Rectangle.Intersect(new Rectangle(posRect.X + 10, posRect.Y, posRect.Width - 20, posRect.Height), platforms[i].BoundingBox).IsEmpty) {
                    //old version on next line
                    //if (posRect.Intersects(platforms[i].BoundingBox) && state != States.Standing) {

                    //If character is above platform and falling when he collides, sets conditions for landing on platform
                    if(position.Y < platforms[i].Pos.Y) {
                        if (velocity.Y < 0) {
                            velocity = Vector2.Zero;
                            position.Y = platforms[i].Pos.Y - HEIGHT;
                            state = States.Standing;
                            if (i == ((curPlatform + 1) % 3))
                                onNextPlat = true;
                        }
                    }
                    //If character is below platform and rising when he collides, sets conditions for falling back down and bounding off of platform
                    else if (position.Y + HEIGHT > platforms[i].Pos.Y + platforms[i].Tex.Height) {
                        if (velocity.Y > 0) {
                            velocity.Y *= -1;
                        }
                    }

                    //If character hits side of platform, reverses horizontal travel direction
                    if (position.X < platforms[i].Pos.X || position.X + WIDTH > platforms[i].Pos.X + platforms[i].Tex.Width) {
                        velocity.X *= -1;
                    }
                }
            }

            return onNextPlat;
        }

        // Put player around middle of the screen
        public void SetPlayer(Vector2 position) {
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch) {
            
            //draw character
            Rectangle spriteRect = new Rectangle(14, 0, 14, 17);

            // Player direction
            if (velocity.X > 0.0) {
                spriteRect.X = 0;
            } else if (velocity.X < 0.0) {
                spriteRect.X = 28;
            } else {
                spriteRect.X = 14;
            }

            spriteBatch.Draw(sprite, posRect, spriteRect, Color.White);

            // Draw arrow if the player is still on the ground
            if (state == States.Standing || state == States.Power) {

                // Arrow Fill
                // Gross math to get the position of where the arrow should be
                var arrowRect = new Rectangle();
                arrowRect.X = (int)((ARROW_PADING - arrowPower) * Math.Cos(arrowAngle - PI / 2) + position.X + WIDTH / 2 
                                        - ARROW_WIDTH / 2 * Math.Cos(arrowAngle));

                arrowRect.Y = (int)((ARROW_PADING - arrowPower) * Math.Sin(arrowAngle - PI / 2) + position.Y + 5*SCALE
                                        - ARROW_WIDTH / 2 * Math.Sin(arrowAngle));

                arrowRect.Height = ARROW_HEIGHT - (int) arrowPower;
                arrowRect.Width = ARROW_WIDTH;

                spriteBatch.Draw(arrowFill, arrowRect, new Rectangle(0, (int) arrowPower, ARROW_WIDTH, ARROW_HEIGHT-(int)arrowPower),
                                    Color.White, (float)arrowAngle, Vector2.Zero, SpriteEffects.None, 0);


                // Arrow Outline
                // More gross math, yet somehow beautiful at the same time. I </3 trig.
                arrowRect.X = (int)(ARROW_PADING * Math.Cos(arrowAngle - PI / 2) + position.X + WIDTH / 2 
                                    - ARROW_WIDTH / 2 * Math.Cos(arrowAngle));

                arrowRect.Y = (int)(ARROW_PADING * Math.Sin(arrowAngle - PI / 2) + position.Y + 5*SCALE 
                                    - ARROW_WIDTH / 2 * Math.Sin(arrowAngle));

                arrowRect.Height = ARROW_HEIGHT;
                spriteBatch.Draw(arrowTexture, arrowRect, null, Color.White, (float)arrowAngle, Vector2.Zero, SpriteEffects.None, 0);
            }
        }
    }
}
