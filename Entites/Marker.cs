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
    class Marker : Entity
    {
        public Marker(int id)
            : base(id)
        {
            object_type = 3;
        }
        public void init(float x, float y)
        {
            base.init(x, y, 16, 16);
        }
    }
}
