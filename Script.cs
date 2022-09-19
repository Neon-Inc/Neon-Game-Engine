using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine{
    public class Script{
        public string name = "", description = "";
        public delegate void Start1();
        public delegate void Update1();
        public Start1 Start;
        public Update1 Update;
    }
}
