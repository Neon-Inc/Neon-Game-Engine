using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine{
    public class GameObject{
        public int id;
        public string name = "object";
        public string description = "";
        public char graph = 'O';

        public int X, Y;//Position
        
        private List<Script> scripts = new List<Script>();
        public List<GameObject> parentOf = new List<GameObject>();
        public List<GameObject> childOf = new List<GameObject>();
        
        public void AddScript(Script script){
            scripts.Add(script);
            
            
        }
        public Script GetScript(int id) { 
            return scripts[id];
        }
        public void runScriptStart(){
            for (int i = 0; i < scripts.Count; i++) {
                scripts[i].Start();
            }
        }
        public void runScriptUpdate(){
            for (int i = 0; i < scripts.Count; i++){
                scripts[i].Update();
            }
        }


    }


}
