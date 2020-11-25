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
    class BaseEnemy : Being
    {
        protected int baseHP;
        public BaseEnemy(int id)
            : base(id)
        {
        }
        public void simpleMovement()
        {
            if (Direction == 0) x_vel = speed;
            if (Direction == 180) x_vel = -speed;
        }
        public void redTurtling()
        {
            //The behaviour of a red turtle in the Mario series, in that it never moves off of the platform.
            //TODO: Think of a better name.
            if (Direction == 0 && isColliding(x_pos + width, y_pos, DOWN, 1) == -1) { Direction = 180; }
            if (Direction == 180 && isColliding(x_pos - width, y_pos, DOWN, 1) == -1) { Direction = 0; }
        }
        public void turtling()
        {
            //The behaviour of a turtle in the Mario series, in that it doesn't move horizontally in the air.
            if (y_vel > 0 && isColliding(DOWN, 1) > -1)
                y_vel = 0;
            if (y_vel != 0)
                x_vel = 0;
            else
                simpleMovement();
            //Gravity stuff
            handleGravity();
        }
        public void jumpingMovement()
        {
            handleGravity();
            if (isColliding(DOWN, 1) > -1)
            {
                y_pos -= speed * 2;
                y_vel = -jump_height;
            }
        }
        public void markerCollision()
        {
            if (isColliding(RIGHT, 3) > -1) { Direction = 180; x_pos -= speed * 2; }
            if (isColliding(LEFT, 3) > -1) { Direction = 0; x_pos += speed * 2; }
        }
        public bool HeadWeak
        {
            get { return Comp.HeadWeak; }
            set { Comp.HeadWeak = value;}
        }
        public new void updatePosition()
        {
            x_pos += x_vel;
            y_pos += y_vel;
            if (isColliding(RIGHT, 1) > -1) { Direction = 180; x_pos -= speed * 2; }
            if (isColliding(LEFT, 1) > -1) { Direction = 0; x_pos += speed * 2; }
        }
        public override void init(float x, float y, int w, int h)
        {
            base.init(x, y, w, h);
            HP = baseHP;
        }
        public override void reInit(float x, float y)
        {
            base.reInit(x, y);
            InvinsTimer = 0;
            HP = baseHP;
        }
        public override void handleAnimation()
        {
            base.handleAnimation();
        }
        public override bool update()
        {
            if (base.update() == false) return false;
            if (cam.ifInRange(comp.Mask) == false && object_type != 5) return false;
            handleDeath();
            return true;
        }
        public override void whenDestroyed()
        {
            base.whenDestroyed();
            reInit(x_or, y_or);
        }
    }
}
