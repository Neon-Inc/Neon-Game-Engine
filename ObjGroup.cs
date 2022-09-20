using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
     public class ObjGroup{
        public Engine engine;
        public List<int> objIDs = new List<int>();
        public List<Script> scripts = new List<Script>();
        public List<GameObject> parentOf = new List<GameObject>();
        public List<GameObject> childOf = new List<GameObject>();
        public string name, description;



        public void Init() {
            for (int i = 0; i < engine.GameObjects.Count; i++){
                for(int j = 0; j < objIDs.Count; j++){
                    if (objIDs[j] == i){
                        engine.GameObjects[i].partOfGroup = true;
                    }
                }
            }
        }
        public void setTexture(char[] texture){
            for (int i = 0; i < engine.GameObjects.Count; i++)
            {
                for (int j = 0; j < objIDs.Count; j++)
                {
                    if (objIDs[j] == i)
                    {
                        engine.GameObjects[i].graph = texture[j];
                    }
                }
            }
        }
        public void Move(int X, int Y){
            for (int i = 0; i < engine.GameObjects.Count; i++){
                for (int j = 0; j < objIDs.Count; j++){
                    if (objIDs[j] == i){
                        engine.GameObjects[i].X += X;
                        engine.GameObjects[i].Y += Y;
                    }
                }
            }
        }
        public void AddScript(Script script){
            scripts.Add(script);
        }
        //Get the script by id
        public Script GetScript(int id){
            return scripts[id];
        }
        //Runs startup script
        public void runScriptStart(){
            for (int i = 0; i < scripts.Count; i++){
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
