using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    class BasePlayer : Being
    {
        public bool isPosh;
        public int timer;
        public BasePlayer(int id)
            : base(id)
        {
            object_type = 2;
            isPosh = false;
            timer = 0;
        }
        public void interactingWithEnemys(int object_id)
        {
            if ((Invinsible == false && ((isColliding(DOWN, object_id) > -1) || (isColliding(LEFT, object_id) > -1) || (isColliding(RIGHT, object_id) > -1))) || (isColliding(UP, object_id) > -1))
            {
                foreach (Component other in cols)
                {
                    if (other != null)
                    {
                        if (other.ID == isColliding(DOWN, object_id) && other.Destroyed == false && other.Invinsible == false && other.HeadWeak == true)
                        {
                            gettingHit(other, 10, (int)(50 * other.Restitution));
                            gravity = 0;
                            if (isJumping) y_vel = (-jump_height) * other.Restitution;
                            else y_vel = (-jump_height / 3) * other.Restitution;
                        }
                        else if (Comp.Invinsible == false)
                        {
                            if (other.ID == isColliding(LEFT, object_id)) gettingHit(10, 50);
                            else if (other.ID == isColliding(RIGHT, object_id)) gettingHit(10, 50);
                            else if (other.ID == isColliding(UP, object_id)) gettingHit(10, 50);
                            else if (other.ID == isColliding(DOWN, object_id) && other.HeadWeak == false) gettingHit(10, 50);
                        }
                    }
                }
            }
        }
        public override void init(float x, float y, int w, int h)
        {
            base.init(x, y, w, h);
            initGravity(0.01f, 1.5f, 4);
            speed = 3;
            HP = 20;
        }
        public override void reInit(float x, float y)
        {
            base.reInit(x, y);
            x_vel = 0;
            InvinsTimer = 0;
            HP = 20;
            timer = 28;
        }
        public override void handleInput(KeyboardState keyboard)
        {
            x_vel = 0;
            if (isFrozen == true) return;
            //Movements
            if (keyboard.IsKeyDown(Keys.Left)) { Direction = 180; x_vel = -speed; }
            else if (keyboard.IsKeyDown(Keys.Right)) { Direction = 0; x_vel = speed; }
            if (keyboard.IsKeyDown(Keys.Up) && isColliding(DOWN, 1) > -1) { gravity = 0; y_vel = -jump_height; }
            if (keyboard.IsKeyUp(Keys.Up) && y_vel < -jump_height  && isJumping == true ) y_vel = 0;
            if (keyboard.IsKeyDown(Keys.P)) isPosh = true;
            if (keyboard.IsKeyDown(Keys.Up)) isJumping = true;
            else isJumping = false;
        }
        public override void handleAnimation()
        {
            base.handleAnimation();
            if (Invinsible == true)
            {
                if (isPosh == false)
                {
                    if (InvinsTimer % 4 == 1)
                    {
                        if (HP > 10) current_frame = 0;
                        else current_frame = 1;
                    }
                    else current_frame = 6;
                }
                else
                {
                    if (InvinsTimer % 4 == 1)
                    {
                        if (Direction == 0)
                        {
                            if (HP > 10) current_frame = 4;
                            else current_frame = 5;
                        }
                        if (Direction == 180)
                        {
                            if (HP > 10) current_frame = 2;
                            else current_frame = 3;
                        }
                    }
                    else current_frame = 6;
                }

            }
            else
            {
                if (isPosh == false)
                {
                    if (HP > 10) current_frame = 0;
                    else current_frame = 1;
                }
                else
                {
                    if (Direction == 0)
                    {
                        if (HP > 10) current_frame = 4;
                        else current_frame = 5;
                    }
                    if (Direction == 180)
                    {
                        if (HP > 10) current_frame = 2;
                        else current_frame = 3;
                    }
                }
            }
        }
        public override bool update()
        {
            //Beginning
            timer--;
            if (timer > 0)
                freezeAll(true);
            else
                unFreezeAll();
            if (base.update() == false) return false;
            //Bad guy interaction
            interactingWithEnemys(4);
            interactingWithEnemys(5);
            interactingWithEnemys(6);
            interactingWithEnemys(7);
            //Movement stuff
            handleGravity();
            updatePosition();
            handleAnimation();
            //Handle death
            if (isColliding(RIGHT, 9) > -1 || isColliding(LEFT, 9) > -1 || isColliding(UP, 9) > -1 || isColliding(DOWN, 9) > -1)
                HP = 0;
            handleDeath();
            //Next level
            if (isColliding(RIGHT, 8) > -1 || isColliding(LEFT, 8) > -1 || isColliding(UP, 8) > -1 || isColliding(DOWN, 8) > -1)
                Level++;
            return true;
        }
        public override void whenDestroyed()
        {
            base.whenDestroyed();
            reInit(x_or, y_or);
        }
    }
}
