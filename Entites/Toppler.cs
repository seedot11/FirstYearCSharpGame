using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2
{
    class Toppler : BaseEnemy
    {
        public Toppler(int id)
            : base(id)
        {
            object_type = 6;
        }
        public override void init(float x, float y, int w, int h)
        {
            base.init(x, y, w, h);
            initGravity(0.01f, 0.5f, 0);
            speed = 2;
            baseHP = 10;
            Restitution = 1;
        }
        public override void reInit(float x, float y)
        {
            base.reInit(x, y);
        }
        public override void handleAnimation()
        {
            base.handleAnimation();
            if (Direction == 0)
                current_frame = 1;
            if (Direction == 180)
                current_frame = 2;
        }
        public override bool update()
        {
            if (base.update() == false) return false;
            updatePosition();
            turtling();
            handleAnimation();
            return true;
        }
        public override void whenDestroyed()
        {
            base.whenDestroyed();
        }
    }
}
