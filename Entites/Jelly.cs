using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2
{
    class Jelly : BaseEnemy
    {
            public Jelly(int id)
        : base(id)
        {
            object_type = 7;
        }
        public override void init(float x, float y, int w, int h)
        {
            base.init(x, y, w, h);
            initGravity(0.01f, 0.5f, 0);
            baseHP = 100;
            speed = 1.5f;
            Restitution = 3.15f;
        }
        public override void reInit(float x, float y)
        {
            base.reInit(x, y);
        }
        public override void handleAnimation()
        {
            base.handleAnimation();
            if (InvinsTimer <= 0)
                current_frame = 0;
            else
            {
                if (InvinsTimer > 0 && InvinsTimer <= 20)
                    current_frame = 1;
                if (InvinsTimer > 20 && InvinsTimer <= 40)
                    current_frame = 2;
                if (InvinsTimer > 40 && InvinsTimer <= 60)
                    current_frame = 3;
                if (InvinsTimer > 60)
                    current_frame = 4;
            }
        }
        public override bool update()
        {
            if (base.update() == false) return false;
            updatePosition();
            turtling();
            markerCollision();
            handleAnimation();
            return true;
        }
        public override void whenDestroyed()
        {
            base.whenDestroyed();
        }
    }
}
