using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameEngine.Script;

namespace GameEngine
{
    public class BackgroundTask{
        public delegate void BackGroundTask();
        public BackGroundTask backGroundTask;
        public void start(){
            Thread BackGroundTask1 = new Thread(new ThreadStart(backGroundTask));
            BackGroundTask1.Start();
        }
        

    }
}
