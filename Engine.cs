using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine{
    
    public class Engine{
        private Graphics Graphics = new Graphics();
        public List<GameObject> GameObjects = new List<GameObject>();
        private System.Timers.Timer mainTimer = new System.Timers.Timer();

        //Interval of main timer(readonly, controlled by other funcitions)
        private int interval = 10; 
        //Controls if debug mode is ON or OFF(Showing extra details etc.)
        public bool debug = false;
        //Sets FPS
        private int framerate;
        public int defaultFramerate = 10;
        
        //Intial function
        public void init(){
                Graphics.Init();
                initScripts();
                initTimer();
        }
        //Sets resolution of canvas
        public int setResolution(int X, int Y) {
            Graphics.resolutionX = X;
            Graphics.resolutionY = Y;
            return 0;
        }
        //Updates OOP parent list
        private int UpdateParents(){     
            for (int i = 0; i < GameObjects.Count; i++){
                List<int> tmp = new List<int>();
                if (GameObjects[i].parentOf.Count > 0){
                    for (int j = 0; j < GameObjects[i].parentOf.Count; j++) {
                        tmp.Add(GameObjects[j].id);
                    }
                    for(int i2 = 0; i2 < GameObjects.Count; i2++) {
                        for(int j2 = 0; j2 < tmp.Count; j2++){
                            if (GameObjects[i2].id == tmp[j2]){
                                for(int i3 = 0; i3 < GameObjects[i2].childOf.Count;i3++){
                                    if (GameObjects[i2].childOf[i3].id != tmp[j2]){
                                        GameObjects[i2].childOf.Add(GameObjects[tmp[j2]]);
                                    }
                                }           
                            }
                        }
                    }       
                }
            }
            return 0;
        }
        //Creates new gameobject with name
        public int CreateGameObject(string name){
            GameObjects.Add(new GameObject());
            GameObjects[GameObjects.Count - 1].id = GameObjects.Count - 1;
            GameObjects[GameObjects.Count - 1].X = 0;
            GameObjects[GameObjects.Count - 1].Y = 0;
            GameObjects[GameObjects.Count - 1].name = name;
            GameObjects[GameObjects.Count - 1].runScriptStart();
            UpdateParents();
            return 0;
        }
        //Without name
        public int CreateGameObject(){
            GameObjects.Add(new GameObject());
            GameObjects[GameObjects.Count - 1].id = GameObjects.Count - 1;
            GameObjects[GameObjects.Count - 1].X = 0;
            GameObjects[GameObjects.Count - 1].Y = 0;
            GameObjects[GameObjects.Count - 1].runScriptStart();
            UpdateParents();
            return 0;
        }
        //Update all update scripts of all objects
        public int UpdateObjectScripts(){
            for (int i = 0; i < GameObjects.Count; i++){
                GameObjects[i].runScriptUpdate();
            }
            return 0;
        }
        //Update function
        private void Update(){
            UpdateParents();
            Graphics.gameObjects = GameObjects;
            Graphics.updateScreen();
            UpdateObjectScripts();
        }
        //Starts Main timer
        private void initTimer(){
            mainTimer.Stop();
            setFramerate(defaultFramerate);
            mainTimer.Elapsed += tick;
            mainTimer.Start();
        }
        //Run all starts scripts of all objects
        private void initScripts(){
            for (int i = 0; i < GameObjects.Count; i++){
                GameObjects[i].runScriptStart();
            }
        }
        private void tick(object? sender, System.Timers.ElapsedEventArgs e){
            Update();
        }//Sets framerate (new framerate = how much fps)
        public void setFramerate(int newFramerate){ 
            
            framerate = newFramerate;
            interval = 1000 / newFramerate;
            defaultFramerate = interval;
            mainTimer.Interval = interval;
            mainTimer.Stop();
            mainTimer.Start();
        }
        
        

    }

}
