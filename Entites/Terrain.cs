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
    class Terrain
    {
        private int x_pos, y_pos;
        private int type;
        private Camera cam;
        private Texture2D sprite;

        public Terrain()
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
            if (type < 32 && cam.ifInRange(new Rectangle(x_pos, y_pos, 16, 16)))
                sb.Draw(sprite, new Rectangle(x_pos - cam.X, y_pos - cam.Y, 16, 16), new Rectangle((type % 4) * 16, (type / 4) * 16, 16, 16), Color.White);
            if (type >= 32 && cam.ifInRange(new Rectangle(x_pos, y_pos, 32, 32)))
                sb.Draw(sprite, new Rectangle((int)(x_pos - cam.X + ((type - 32) % 4) * 16), (int)(y_pos - cam.Y + ((type - 32) / 4) * 16 - 64), 32, 32), new Rectangle((type % 4) * 16, (type / 4) * 16, 16, 16), Color.White);
        }
    }
}
