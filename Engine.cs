using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine{
    
    public class Engine{
        public bool catchInputEnabled = false;
        private Graphics Graphics = new Graphics();
        public List<GameObject> GameObjects = new List<GameObject>();
        private System.Timers.Timer mainTimer = new System.Timers.Timer();
        public int currentObjScript = 0;
        //Interval of main timer(readonly, controlled by other funcitions)
        //Controls if debug mode is ON or OFF(Showing extra details etc.)
        public bool debug = false;
        //Sets FPS
        private int framerate;
        public int defaultFramerate = 10;
        public void OnError(string errormsg, string error){
            Stop();
            Console.Clear();
            if (debug) {
                Console.WriteLine(errormsg + "\n" + error);
            }
            else { 
            Console.WriteLine(errormsg);
            }
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(-1);
        }
        //Intial function
        public void init(){
            try{
                Graphics.Init();
                initScripts();
                initTimer();
            }catch(Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }
        //Sets resolution of canvas
        public int setResolution(int X, int Y) {
            try
            {
                Graphics.resolutionX = X;
                Graphics.resolutionY = Y;
                return 0;
            }catch(Exception ex){
                OnError(ex.Message, ex.ToString());
                return -1;
            }
        }
        //Updates OOP parent list
        private int UpdateParents(){
            try
            {
                for (int i = 0; i < GameObjects.Count; i++)
                {
                    List<int> tmp = new List<int>();
                    if (GameObjects[i].parentOf.Count > 0)
                    {
                        for (int j = 0; j < GameObjects[i].parentOf.Count; j++)
                        {
                            tmp.Add(GameObjects[j].id);
                        }
                        for (int i2 = 0; i2 < GameObjects.Count; i2++)
                        {
                            for (int j2 = 0; j2 < tmp.Count; j2++)
                            {
                                if (GameObjects[i2].id == tmp[j2])
                                {
                                    for (int i3 = 0; i3 < GameObjects[i2].childOf.Count; i3++)
                                    {
                                        if (GameObjects[i2].childOf[i3].id != tmp[j2])
                                        {
                                            GameObjects[i2].childOf.Add(GameObjects[tmp[j2]]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return 0;
            }catch(Exception ex){
                OnError(ex.Message, ex.ToString());
                return 0;
            }
        }
        public void changeColor(ConsoleColor foreColor, ConsoleColor backColor){
            try { 
            Graphics.changeColor(foreColor, backColor);
            }
            catch (Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }
        public void changeDefaultcolor(ConsoleColor fore, ConsoleColor back){
            try { 
            Graphics.defaultBack = back;
            Graphics.defaultFore = fore;
            }
            catch (Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }
            //Creates new gameobject with name
            public int CreateGameObject(string name, string description, int X,int Y, char graph){
            try { 
            GameObjects.Add(new GameObject());
            GameObjects[GameObjects.Count - 1].id = GameObjects.Count - 1;
            GameObjects[GameObjects.Count - 1].X = X;
            GameObjects[GameObjects.Count - 1].Y = Y;
            GameObjects[GameObjects.Count - 1].graph = graph;
            GameObjects[GameObjects.Count - 1].name = name;
            GameObjects[GameObjects.Count - 1].description = description;
            GameObjects[GameObjects.Count - 1].runScriptStart();
            UpdateParents();
            return 0;
            }
            catch (Exception ex){
                OnError(ex.Message, ex.ToString());return -1;
            }
        }
        public int CreateGameObject(string name){
            try
            {
                GameObjects.Add(new GameObject());
                GameObjects[GameObjects.Count - 1].id = GameObjects.Count - 1;
                GameObjects[GameObjects.Count - 1].X = 0;
                GameObjects[GameObjects.Count - 1].Y = 0;
                GameObjects[GameObjects.Count - 1].name = name;
                GameObjects[GameObjects.Count - 1].runScriptStart();
                UpdateParents();
                return 0;
            }
            catch(Exception ex){
                OnError(ex.Message, ex.ToString());
                return -1;
            }
        }
        //Without name
        public int CreateGameObject(){
            try{
            GameObjects.Add(new GameObject());
            GameObjects[GameObjects.Count - 1].id = GameObjects.Count - 1;
            GameObjects[GameObjects.Count - 1].X = 0;
            GameObjects[GameObjects.Count - 1].Y = 0;
            GameObjects[GameObjects.Count - 1].runScriptStart();
            UpdateParents();
            return 0;
            }
            catch (Exception ex){
                OnError(ex.Message, ex.ToString());
                return -1;
            }
        }
        //Update all update scripts of all objects
        public int UpdateObjectScripts(){
            for (int i = 0; i < GameObjects.Count; i++){
                currentObjScript = i;
                GameObjects[i].runScriptUpdate();
            }
            return 0;
        }
        //Update function
        private void Update(){
            try
            {
                UpdateParents();
                Graphics.gameObjects = GameObjects;
                Graphics.updateScreen();
                UpdateObjectScripts();
            }catch(Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }
        //Starts Main timer
        private void initTimer(){
            try { 
            mainTimer.Stop();
            setFramerate(defaultFramerate);
            mainTimer.Elapsed += tick;
            mainTimer.Start();
            }
            catch (Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }

    
        //Run all starts scripts of all objects
        private void initScripts(){
            try{
                for (int i = 0; i < GameObjects.Count; i++){
                    GameObjects[i].runScriptStart();
                }
            }
            catch (Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }
        private void tick(object? sender, System.Timers.ElapsedEventArgs e){
            Update();
        }//Sets framerate (new framerate = how much fps)
        public void setFramerate(int newFramerate){
            try { 
            int interval = 10;
            framerate = newFramerate;
            interval = 1000 / newFramerate;
            defaultFramerate = interval;
            mainTimer.Interval = interval;
            mainTimer.Stop();
            mainTimer.Start();
            }
            catch (Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }
        public int[] getResolution(){
            try{
                int[] ints = new int[2];
                ints[0] = Graphics.resolutionX;
                ints[1] = Graphics.resolutionY;
                return ints;
            }catch(Exception ex){
                OnError(ex.Message, ex.ToString());
                return new int[2] {5,5 };
            }
        }
        public void Stop(){
            try{
                mainTimer.Stop();
            }catch(Exception ex){
                OnError(ex.Message, ex.ToString());
            }
        }

    }

}
