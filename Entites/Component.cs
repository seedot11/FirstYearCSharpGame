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
    class Component
    {
        private Rectangle mask;
        private int id, object_type;
        private bool isSolid, isDestroyed, isFrozen, isInvinsible;
        private int health_points;
        private int invins_timer;
        private float restitution;
        private int level;
        private int direction;
        private bool headWeak;
        private bool athlete;

        public Component()
        {
            id = object_type = -1;
            isSolid = isDestroyed = isFrozen = false;
            health_points = 0;
            Restitution = 1;
            headWeak = true;
        }
        public void init(Rectangle mask, int id, int object_type)
        {
            this.mask = mask;
            this.id = id;
            this.object_type = object_type;
        }
        public int ID
        {
            get { return id; }
        }
        public int ObjType
        {
            get { return object_type; }
        }
        public int X
        {
            get { return mask.X; }
            set { mask.X = value; }
        }
        public int Y
        {
            get { return mask.Y; }
            set { mask.Y = value; }
        }
        public int Width
        {
            get { return mask.Width; }
            set { mask.Width = value; }
        }
        public int Height
        {
            get { return mask.Height; }
            set { mask.Height = value; }
        }
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public bool HeadWeak
        {
            get { return headWeak; }
            set { headWeak = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public bool Athlete
        {
            get { return athlete; }
            set { athlete = value; }
        }
        public Rectangle Mask
        {
            set { mask = value; }
            get { return mask; }
        }
        public bool Destroyed
        {
            get { return isDestroyed; }
            set { isDestroyed = value; }
        }
        public bool Solid
        {
            get { return isSolid; }
            set { isSolid = value; }
        }
        public bool Frozen
        {
            get { return isFrozen; }
            set { isFrozen = value; }
        }
        public bool Invinsible
        {
            get { return isInvinsible; }
            set { isInvinsible = value; }
        }
        public int InvinsTimer
        {
            get { return invins_timer; }
            set { invins_timer = value; }
        }
        public int HP
        {
            set { health_points = value; }
            get { return health_points; }
        }
        public float Restitution
        {
            set { restitution = value; }
            get { return restitution; }
        }
    }
}
