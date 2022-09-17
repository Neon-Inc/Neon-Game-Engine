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

        private int interval; 
        public bool debug = false;
        private int framerate;
        public int defaultFramerate = 30;
        
        public void init(){
            Graphics.Init();
            initScripts();
            initTimer();
        }
        
        private void UpdateParents(){     
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
        }
        private void CreateGameObject(string name){
            GameObjects.Add(new GameObject());
            GameObjects[GameObjects.Count - 1].id = GameObjects.Count - 1;
            GameObjects[GameObjects.Count - 1].X = 0;
            GameObjects[GameObjects.Count - 1].Y = 0;
            GameObjects[GameObjects.Count - 1].name = name;
            GameObjects[GameObjects.Count - 1].runScriptStart();
            UpdateParents();
        }
        private void CreateGameObject(){
            GameObjects.Add(new GameObject());
            GameObjects[GameObjects.Count - 1].id = GameObjects.Count - 1;
            GameObjects[GameObjects.Count - 1].X = 0;
            GameObjects[GameObjects.Count - 1].Y = 0;
            GameObjects[GameObjects.Count - 1].runScriptStart();
            UpdateParents();
        }
        public void UpdateObjectScripts(){
            for (int i = 0; i < GameObjects.Count; i++){
                GameObjects[i].runScriptUpdate();
            }
        }
        private void Update(){

            UpdateParents();
            Graphics.gameObjects = GameObjects;
            Graphics.updateScreen();
            UpdateObjectScripts();
        }
        private void initTimer(){
            mainTimer.Stop();
            setFramerate(defaultFramerate);
            mainTimer.Elapsed += tick;
            mainTimer.Start();
        }
        private void initScripts(){
            for (int i = 0; i < GameObjects.Count; i++){
                GameObjects[i].runScriptStart();
            }
        }
        private void tick(object? sender, System.Timers.ElapsedEventArgs e){
            Update();
        }
        private void setFramerate(int newFramerate){ 
            framerate = newFramerate;
            interval = 1000 / newFramerate;
            mainTimer.Interval = interval;
        }
        
        

    }

}
