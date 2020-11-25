using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2
{
    class Block : Entity
    {
        public Block(int id)
            : base(id)
        {
            object_type = 1;
            isSolid = true;
        }
    }
}
