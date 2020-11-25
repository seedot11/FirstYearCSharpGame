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
    class Being : Entity
    {
        protected float x_vel, y_vel; //The current velocities
        protected float speed; //The max speed the Being can go horizontally.
        protected bool isJumping; 
        protected float gravity, gravity_rate, gravity_max; //Variables that handle gravity aspects.
        protected float x_or, y_or; //The origins
        protected int jump_height; //The max height of which the being can jump.
        protected bool isFrozen; //Determines whether it will act as it should.
        protected Component[] cols; //Remember other objects

        public Being(int id)
            : base(id)
        {
            x_vel = y_vel = 0.0f;
            speed = 0;
            gravity = gravity_rate = gravity_max = 0;
            jump_height = 0;
            Direction = 0;
            cols = null;
            isFrozen = false;
            Invinsible = false;
            InvinsTimer = 0;
        }
        public float Speed
        {
            set { speed = value; }
            get { return speed; }
        }
        public bool Frozen
        {
            get { return isFrozen; }
            set { isFrozen = value; }
        }
        public bool Invinsible
        {
            get { return Comp.Invinsible; }
            set { Comp.Invinsible = value; }
        }
        public int InvinsTimer
        {
            get { return Comp.InvinsTimer; }
            set { Comp.InvinsTimer = value; }
        }
        public int HP
        {
            set { Comp.HP = value; }
            get { return Comp.HP; }
        }
        public int Direction
        {
            get { return Comp.Direction; }
            set { Comp.Direction = value; }
        }
        public void initGravity(float rate, float max, int jump)
        {
            gravity_rate = rate;
            gravity_max = max;
            jump_height = jump;
        }
        public void initWorld(Component[] other)
        {
            cols = other;
        }
        public int isColliding(int direction, int objectType)
        {
            int x = (int)x_pos;
            int y = (int)y_pos;
            int s_x = (int)(Math.Abs(x_vel)) + 1;
            int s_y = (int)(Math.Abs(y_vel));
            foreach (Component other in cols)
            {
                if (other != null && other.ObjType == objectType)
                    switch (direction % 4)
                    {
                        case RIGHT: if (y + height > other.Y && y < other.Y + other.Height && x + width <= other.X && x + width + s_x >= other.X)
                            { x_pos = other.X - width - 1; return other.ID; } break;
                        case LEFT: if (y + height > other.Y && y < other.Y + other.Height && x >= other.X + other.Width && x - s_x <= other.X + other.Width)
                            { x_pos = other.X + other.Width + 1; return other.ID; } break;
                        case DOWN: if (x + width > other.X && x < other.X + other.Width && y + height <= other.Y && y + height + s_y >= other.Y)
                            { y_pos = other.Y - height - 1; return other.ID; } break;
                        case UP: if (x + width > other.X && x < other.X + other.Width && y >= other.Y + other.Height && y - s_y <= other.Y + other.Height)
                            { y_pos = other.Y + other.Height + 1; return other.ID; } break;
                        default: break;
                    }
            }
            return -1;
        }
        public int isColliding(float x_, float y_, int direction, int objectType)
        {
            //Returns the object id of which it is colliding with, otherwise returns -1.
            int x = (int)x_;
            int y = (int)y_;
            int s_x = (int)(Math.Abs(x_vel + 1));
            int s_y = (int)(Math.Abs(y_vel + 1));
            foreach (Component other in cols)
            {
                if (other != null && other.ObjType == objectType)
                    switch (direction % 4)
                    {
                        case RIGHT: if (y + height > other.Y && y < other.Y + other.Height && x + width <= other.X && x + width + s_x >= other.X)
                            {  return other.ID; } break;
                        case LEFT: if (y + height > other.Y && y < other.Y + other.Height && x >= other.X + other.Width && x - s_x <= other.X + other.Width)
                            {  return other.ID; } break;
                        case DOWN: if (x + width > other.X && x < other.X + other.Width && y + height <= other.Y && y + height + s_y >= other.Y)
                            {  return other.ID; } break;
                        case UP: if (x + width > other.X && x < other.X + other.Width && y >= other.Y + other.Height && y - s_y <= other.Y + other.Height)
                            {  return other.ID; } break;
                        default: break;
                    }
            }
            return -1;
        }
        public void handleGravity()
        {
            if (isColliding(DOWN, 1) > -1)
                gravity = 0;
            gravity += gravity_rate;
            if (gravity > gravity_max) gravity = gravity_max;
            y_vel += gravity;
        }
        public void updatePosition()
        {
            //Bang head
            if (isColliding(UP, 1) > -1 && y_vel < 0)
            {
                y_pos += Math.Abs(y_vel);
                y_vel = 0;
            }
            //Update x and y position according to velocitys
            if ((x_vel > 0 && isColliding(RIGHT, 1) == -1) || (x_vel < 0 && isColliding(LEFT, 1) == -1) && x_pos > 0)
                x_pos += x_vel;
            if ((y_vel > 0 && isColliding(DOWN, 1) == -1) || (y_vel < 0 && isColliding(UP, 1) == -1) && y_pos > 0)
                y_pos += y_vel;
        }
        public void updateWorld(Component[] other)
        {
            cols = other;
        }
        public void freezeAll(bool freezeSelf)
        {
            foreach (Component c in cols)
            {
                if (freezeSelf == true || c.ID != id) c.Frozen = true;
            }
        }
        public void unFreezeAll()
        {
            foreach (Component c in cols)
             c.Frozen = false;
        }
        public void InvinsibilityTimer()
        {
            InvinsTimer--;
            if (InvinsTimer > 0) Invinsible = true;
            else Invinsible = false;
        }
        public void handleDeath()
        {
            if (HP <= 0)
                isDestroyed = true;
        }
        public void gettingHit(int HPloss, int invisAmount)
        { 
            HP -= HPloss;
            InvinsTimer = invisAmount; 
        }
        public void gettingHit(Component other, int HPloss, int invisAmount)
        {
            other.HP -= HPloss;
            other.InvinsTimer = invisAmount;
        }
        public override void init(float x, float y, int w, int h)
        {
            base.init(x, y, w, h);
            x_or = x;
            y_or = y;
        }
        public override void reInit(float x, float y)
        {
            base.reInit(x, y);
            x_or = x;
            y_or = y;
            x_vel = y_vel = 0;
            Direction = 0;
            gravity = 0;
        }
        public override bool update()
        {
            if (base.update() == false) return false;
            isFrozen = comp.Frozen;
            if (isFrozen == true) return false;
            InvinsibilityTimer();
            return true;
        }
        public override void handleAnimation()
        {
            base.handleAnimation();
        }
        public override void handleInput(KeyboardState keyboard)
        {
            base.handleInput(keyboard);
        }
        public override void whenDestroyed()
        {
            base.whenDestroyed();
        }
    }
}
