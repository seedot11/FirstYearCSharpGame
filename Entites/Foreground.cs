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
    class Foreground
    {
        private int x_pos, y_pos;
        private int type;
        private Camera cam;
        private Texture2D sprite;

        public Foreground()
        {
            x_pos = 0;
            y_pos = 0;
            type = 0;
        }
        public void init(int x, int y, int t)
        {
            x_pos = x;
            y_pos = y;
            type = t;
        }
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        public Camera Cam
        {
            get { return cam; }
            set { cam = value; }
        }
        public void draw(SpriteBatch sb)
        {
            if (cam.ifInRange(new Rectangle(x_pos, y_pos, 16, 16)))
                sb.Draw(sprite, new Rectangle(x_pos - cam.X, y_pos - cam.Y, 16, 16), new Rectangle((type % 4) * 16, (type / 4) * 16, 16, 16), Color.White);
        }
    }
}
