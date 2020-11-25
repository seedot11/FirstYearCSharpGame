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
    class Crawler : BaseEnemy
    {
        public bool Athlete
        {
            get { return Comp.Athlete; }
            set { Comp.Athlete = value; }
        }
        public Crawler(int id)
            : base(id)
        {
            object_type = 4;
            Athlete = false;
        }
        public override void init(float x, float y, int w, int h)
        {
            base.init(x, y, w, h);
            baseHP = 10;
            initGravity(0.01f, 0.5f, 0);
            speed = 2;
        }
        public override void reInit(float x, float y)
        {
            base.reInit(x, y);
        }
        public override void handleAnimation()
        {
            base.handleAnimation();
            if (Direction == 0)
                current_frame = 0;
            if (Direction == 180)
                current_frame = 3;
        }
        public override bool update()
        {
            if (base.update() == false) return false;
            updatePosition();
            turtling();
            redTurtling();
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
