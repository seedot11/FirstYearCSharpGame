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
    class Camera
    {
        private int x_pos, y_pos;
        private int width, height;
        public Camera()
        {
            x_pos = y_pos = 0;
            width = height = 0;
        }
        public void init(int x_, int y_, int w_, int h_)
        {
            x_pos = x_;
            y_pos = y_;
            width = w_;
            height = h_;
        }
        public void init(int w_, int h_)
        {
            width = w_;
            height = h_;
        }
        public int X
        {
            get { return x_pos; }
        }
        public int Y
        {
            get { return y_pos; }
        }
        public int Width
        {
            get { return width; }
        }
        public int Height
        {
            get { return height; }
        }
        public Rectangle Screen
        {
            get { return new Rectangle(x_pos, y_pos, width, height); }
        }
        public void advanceScreen(int direction, int by)
        {
            //Advances the camera in the specified direction by the specified amount.
            //Make sure you don't go past the origin.
            switch (direction % 4)
            {
                case RIGHT:
                    x_pos += by; break;
                case LEFT:
                    if (x_pos - by > 0) x_pos -= by;
                    else x_pos = 0; break;
                case DOWN:
                    y_pos += by; break;
                case UP:
                    if (y_pos - by > 0) y_pos -= by;
                    else y_pos = 0; break;
                default: break;
            }
        }
        public bool ifInRange(Rectangle other)
        {
            if (other.X + other.Width > x_pos && other.X < x_pos + width && other.Y + other.Height > y_pos && other.Y < y_pos + height)
                return true;
            else
                return false;
        }
        public void centreAround(Component other, int speed)
        {
            if (ifInRange(other.Mask) == false)
            {
                x_pos = other.X - width/2;
                y_pos = other.Y - height/2;
            }
            else
            {
                if (other == null) return;
                if (other.X + other.Width / 2 > x_pos + width / 2) advanceScreen(RIGHT, speed);
                if (other.X - other.Width / 2 < x_pos + width / 2) advanceScreen(LEFT, speed);
                if (other.Y + other.Height / 2> y_pos + height /2) advanceScreen(DOWN, (int)(speed * 1.5));
                if (other.Y - other.Height / 2 < y_pos + height /2) advanceScreen(UP, (int)(speed * 1.5));
            }
        }

        public const int LEFT = 1;
        public const int RIGHT = 0;
        public const int UP = 3;
        public const int DOWN = 2;
    }
}
