using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2
{
    class Death : Entity
    {
        public Death(int id)
            : base(id)
        {
            object_type = 9;
        }
    }
}
