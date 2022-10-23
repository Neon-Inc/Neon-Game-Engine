using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine{
    public class GameObject{
        public bool partOfGroup = false;
        //ID of object, by default its automaticly index in list
        public int id;
        public string name = "";
        public string description = "";
        //Sets how graphics of object will look
        public char graph = ' ';
        //Position
        public int X, Y;
        //List of all scripts
        private List<Script> scripts = new List<Script>();
        //List of who is parent of who
        public List<GameObject> parentOf = new List<GameObject>();
        public List<GameObject> childOf = new List<GameObject>();
        public List<int> data = new List<int>();
        //Adds script to object
        public void AddScript(Script script){
            scripts.Add(script);
        }
        //Get the script by id
        public Script GetScript(int id) { 
            return scripts[id];
        }
        //Runs startup script
        public void runScriptStart(){
            for (int i = 0; i < scripts.Count; i++) {
                scripts[i].Start();
            }
        }
        //Runs update script
        public void runScriptUpdate(){
            for (int i = 0; i < scripts.Count; i++){
                scripts[i].Update();
            }
        }
        

    }


}
