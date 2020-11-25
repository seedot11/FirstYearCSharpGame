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
    class Entity
    {
        protected float x_pos, y_pos;
        protected int width, height;
        protected int max_frames, current_frame;
        protected int id, object_type;
        protected bool isSolid, isDestroyed, doneDestroyed;
        protected Texture2D sprite;
        protected Rectangle[] clip;
        protected Component comp;
        protected Camera cam;

        public Entity(int id)
        {
            this.id = id;
            x_pos = y_pos = 0;
            width = height = 0;
            max_frames = current_frame = 0;
            sprite = null;
            object_type = 0;
            isSolid = isDestroyed = doneDestroyed = false;
            comp = new Component();
            Level = 0;
        }
        public Texture2D Sprite
        {
            set { sprite = value; }
        }
        public Component Comp
        {
            get { return comp; }
            set { comp = value; }
        }
        public Camera Cam
        {
            set { cam = value; }
        }
        public float Restitution
        {
            get { return comp.Restitution; }
            set { comp.Restitution = value; }
        }
        public int Level
        {
            get { return comp.Level; }
            set { comp.Level = value; }
        }
        public void setClips(int x, int y, int max_frames)
        {
            this.max_frames = max_frames;
            clip = new Rectangle[max_frames];
            for (int i = 0; i < max_frames; i++)
                clip[i] = new Rectangle(x + i * width, y, width, height);
        }
        public void updateMask()
        {
            Comp.X = (int)x_pos;
            Comp.Y = (int)y_pos;
            Comp.Width = width;
            Comp.Height = height;
        }
        virtual public void init(float x, float y, int w, int h)
        {
            x_pos = x;
            y_pos = y;
            width = w;
            height = h;
            //Initialise the component
            comp.init(new Rectangle((int)x, (int)y, w, h), id, object_type);
        }
        virtual public void reInit(float x, float y)
        {
            x_pos = x;
            y_pos = y;
            comp.init(new Rectangle((int)x_pos, (int)y_pos, width, height), id, object_type);
            isDestroyed = comp.Destroyed = false;
        }
        virtual public void handleInput(KeyboardState keyboard)
        {
        }
        virtual public bool update()
        {
            //Do something when destroyed
            if ((isDestroyed == true || comp.Destroyed == true) && doneDestroyed == false)
            {
                doneDestroyed = false;
                isDestroyed = true;
                whenDestroyed();
            }
            //Can't do anything else when destroyed
            if (isDestroyed == true)
                return false;
            //Update mask
            updateMask();
            return true;
        }
        virtual public void handleAnimation()
        {
        }
        virtual public void draw(SpriteBatch sb)
        {
            //Camera must be initiated.
            if (cam != null)
            {
                //Now check if it's in the camera's range and it's not destroyed
                if (cam.ifInRange(comp.Mask) && isDestroyed == false && current_frame < max_frames)
                    //Draw the position relative to the camera
                    sb.Draw(sprite, new Rectangle((int)x_pos - cam.X, (int)y_pos - cam.Y, width, height), clip[current_frame], Color.White);
            }
            else
                sb.Draw(sprite, new Rectangle((int)x_pos, (int)y_pos, width, height), clip[current_frame], Color.White);
        }
        virtual public void whenDestroyed()
        {
        }

        public const int RIGHT = 0;
        public const int LEFT = 1;
        public const int DOWN = 2;
        public const int UP = 3;
    }
}
