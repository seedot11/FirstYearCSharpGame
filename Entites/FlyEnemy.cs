using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2
{
    class FlyEnemy : BaseEnemy
    {
        public FlyEnemy(int id)
            : base(id)
        {
            object_type = 5;
        }
        public override void init(float x, float y, int w, int h)
        {
            base.init(x, y, w, h);
            initGravity(0.01f, 0.5f, 2);
            baseHP = 10;
            speed = 1;
        }
        public override void reInit(float x, float y)
        {
            base.reInit(x, y);
        }
        public override void handleAnimation()
        {
            base.handleAnimation();
            if (jump_height == 2)
                current_frame = 1;
            else
                current_frame = 2;
        }
        public override bool update()
        {
            if (base.update() == false) return false;
            simpleMovement();
            updatePosition();
            markerCollision();
            jumpingMovement();
            handleAnimation();
            return true;
        }
        public override void whenDestroyed()
        {
            base.whenDestroyed();
            y_pos -= 64;
        }
    }
}
