using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Player : Entity {

        private enum States { Standing, Power, Jumping, Dead }; // Possible states Player can be in
        private States state; // Current state

        const double PI = Math.PI;  // Too lazy to keep typing Math.PI
        const int JUMP_SPEED = 11;  // Initial upwards velocity.Y

        Vector2 velocity;

        double arrowAngle;      // Used for current angle of the arrow
        bool   arrowGoingLeft;  // Controls direction of arrow
        Texture2D arrowTexture; // Outline of arrow
        Texture2D arrowFill;    // Fills in arrow
        double arrowPower;      // Used for current arrow power
        bool   powerInc;        // Controls whether power increasing or decreasing

        bool isDead = false;
        public bool IsDead {
            get { return isDead; }
        }

        int ArrowHeight  { get; } = (int) (Plaper.height * 0.1);    // Arrow constants
        int ArrowWidth   { get; } = (int) (Plaper.height * 0.05);
        int ArrowPadding { get; } = (int) (Plaper.height * 0.15);    // Distance from player's head
        const int ARROW_SPEED  = 2;
        const double ARROW_BOUND_UPPER = (PI / 3);
        const double ARROW_BOUND_LOWER = (-PI / 3);

        public Player(Texture2D texture, Texture2D arrowTexture, Texture2D arrowFill, int startHeight, Rectangle screenBounds) 
            : base(texture, (int)(Plaper.PLAYER_SCALE*Plaper.height*17), (int)(Plaper.PLAYER_SCALE*Plaper.height*14), new Vector2(0f, 0f)) {
            this.arrowFill = arrowFill;
            this.arrowTexture = arrowTexture;

            // Initial conditions of the game
            state = States.Standing;
            arrowAngle = 0.0;
            arrowPower = ArrowHeight;
            arrowGoingLeft = false;

            position = new Vector2((screenBounds.Width - Width) / 2, screenBounds.Height - startHeight - Height);
        }

        public bool Update(GameTime gameTime, Platform[] platforms, int curPlatform) {
            // time since Update was last called
            double elapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;

            bool onNextPlat = false;

            // See which state we are in
            switch(state) {
                case States.Standing:

                    // Rotate arrow
                    double arrowDelta = elapsedSeconds * ARROW_SPEED;
                    arrowAngle += arrowGoingLeft ? -arrowDelta : arrowDelta;

                    // Check arrow bounds
                    if(arrowAngle < ARROW_BOUND_LOWER || ARROW_BOUND_UPPER < arrowAngle) {
                        // limit to bounds
                        arrowAngle = arrowGoingLeft ? ARROW_BOUND_LOWER : ARROW_BOUND_UPPER;
                        // reverse direction
                        arrowGoingLeft = !arrowGoingLeft;
                    }

                    // Move to Power state to get jump power if space is pressed
                    bool spacePressed = Plaper.keyboardState.IsKeyDown(Keys.Space);
                    if(spacePressed) {
                        powerInc = true;
                        state = States.Power;
                    }
                    if(this.Hitbox().X + this.Hitbox().Width / 2 < platforms[curPlatform].Hitbox().X) {
                        this.position.X++;
                    } else if(this.Hitbox().X + this.Hitbox().Width / 2 > platforms[curPlatform].Hitbox().X + platforms[curPlatform].Hitbox().Width) {
                        this.position.X--;
                    }
                    break;

                case States.Power:
                    bool spaceReleased = !Plaper.keyboardState.IsKeyDown(Keys.Space);

                    // Move power up and down
                    arrowPower = arrowPower + elapsedSeconds * (powerInc ? -100 : 100);

                    // Check power bounds (full and empty)
                    if(arrowPower < 0 || ArrowHeight < arrowPower) {
                        arrowPower = powerInc ? 0 : ArrowHeight;
                        powerInc = !powerInc;
                    }

                    // Jump once space is released and move to Jumping state
                    if(spaceReleased) {
                        velocity.Y = (float)(Math.Cos(arrowAngle) * (ArrowHeight - arrowPower) * JUMP_SPEED);
                        velocity.X = (float)(Math.Sin(-arrowAngle) * (ArrowHeight - arrowPower) * JUMP_SPEED);
                        arrowPower = ArrowHeight;
                        state = States.Jumping;
                    }
                    break;

                case States.Jumping:
                    // Subtract Gravity from vertical velocity and add velocity to position
                    velocity.Y -= (float)(elapsedSeconds * Plaper.GRAVITY);
                    position.Y -= (float)(elapsedSeconds * velocity.Y);

                    // Check for wall colissions for add horizontal velocity to position
                    if(position.X < 0 || Plaper.SCREEN_WIDTH < position.X + Width) {
                        velocity.X = -velocity.X;
                        position.X = position.X < 0 ? 0f : Plaper.SCREEN_WIDTH - Width;
                    }
                    position.X -= (float)(elapsedSeconds * velocity.X);

                    // Check for player hitting the bottom of the screen
                    // If so, go back to Standing state and reset arrow
                    if(position.Y + Height > Plaper.SCREEN_HEIGHT) {
                        position.Y = Plaper.SCREEN_HEIGHT - Height;
                        velocity = Vector2.Zero;

                        arrowAngle = 0;
                        arrowGoingLeft = false;

                        state = States.Dead;
                    }
                    break;

                case States.Dead:
                    position = Vector2.Zero;
                    isDead = true;
                    break;              
                
            }

            //Platform collision detection
            for (int i = 0; i < platforms.Length; i++) {

                //makes platform smaller so the player has to be more centered to land on platform.
                if(!Rectangle.Intersect(new Rectangle(Hitbox().X + Plaper.SCREEN_WIDTH / 40, Hitbox().Y, Hitbox().Width - Plaper.SCREEN_WIDTH / 20, Hitbox().Height), platforms[i].Hitbox()).IsEmpty) {
                    //old version on next line
                    //if (posRect.Intersects(platforms[i].BoundingBox) && state != States.Standing) {

                    //If character is above platform and falling when he collides, sets conditions for landing on platform
                    if(position.Y < platforms[i].Pos.Y) {
                        if (velocity.Y < 0) {
                            velocity = Vector2.Zero;
                            position.Y = platforms[i].Pos.Y - Height;
                            state = States.Standing;
                            if (i == ((curPlatform + 1) % 3))
                                onNextPlat = true;
                        }
                    }
                    //If character is below platform and rising when he collides, sets conditions for falling back down and bounding off of platform
                    else if (position.Y + Height > platforms[i].Pos.Y + platforms[i].Hitbox().Height) {
                        if (velocity.Y > 0) {
                            velocity.Y *= -1;
                        }
                    }

                    //If character hits side of platform, reverses horizontal travel direction
                    if (position.X < platforms[i].Pos.X || position.X + Width > platforms[i].Pos.X + platforms[i].Hitbox().Width) {
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

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(Texture, Hitbox(), spriteRect, Color.White);
            spriteBatch.End();

            // Draw arrow if the player is still on the ground
            if ((state == States.Standing || state == States.Power) && !Entity.IsScrolling) {
                spriteBatch.Begin();

                // Arrow Fill
                // Gross math to get the position of where the arrow should be
                var arrowRect = new Rectangle();
                arrowRect.X = (int)((ArrowPadding - arrowPower) * Math.Cos(arrowAngle - PI / 2) + position.X + Width / 2 
                                        - ArrowWidth / 2 * Math.Cos(arrowAngle));

                arrowRect.Y = (int)((ArrowPadding - arrowPower) * Math.Sin(arrowAngle - PI / 2) + position.Y + Height / 3
                                        - ArrowWidth / 2 * Math.Sin(arrowAngle));

                arrowRect.Height = ArrowHeight - (int) arrowPower;
                arrowRect.Width = ArrowWidth;

                spriteBatch.Draw(arrowFill, arrowRect, new Rectangle(0, (int) arrowPower, arrowFill.Width, arrowFill.Height-(int)arrowPower),
                                    Color.White, (float)arrowAngle, Vector2.Zero, SpriteEffects.None, 0);


                // Arrow Outline
                // More gross math, yet somehow beautiful at the same time. I </3 trig.
                arrowRect.X = (int)(ArrowPadding * Math.Cos(arrowAngle - PI / 2) + position.X + Width / 2 
                                    - ArrowWidth / 2 * Math.Cos(arrowAngle));

                arrowRect.Y = (int)(ArrowPadding * Math.Sin(arrowAngle - PI / 2) + position.Y + Height / 3
                                    - ArrowWidth / 2 * Math.Sin(arrowAngle));

                arrowRect.Height = ArrowHeight;
                spriteBatch.Draw(arrowTexture, arrowRect, null, Color.White, (float)arrowAngle, Vector2.Zero, SpriteEffects.None, 0);
                spriteBatch.End();
            }
        }
    }
}
