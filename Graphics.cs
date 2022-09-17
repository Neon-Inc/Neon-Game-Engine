using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Line{
       public List<char> chars = new List<char>();

    }
    public class Graphics{
        public List<GameObject> gameObjects = new List<GameObject>();   
        public static int resolutionX, resolutionY;
        List<Line> lines = new List<Line>();
        public void Init(){
            for (int i = 0; i < resolutionY; i++){
                lines.Add(new Line());
            }
            for(int i = 0;i< gameObjects.Count; i++){
                lines[gameObjects[i].Y].chars[gameObjects[i].X] = gameObjects[i].graph;
            }
        }
        
        public int updateScreen(){
            try { 
            for (int i = 0; i < lines.Count; i++) {
                for(int j = 0; i < lines[i].chars.Count; j++){
                    Console.Write(lines[i].chars[j]);
                }
                Console.Write("\n");
            }
                return 0;
            }catch(Exception ex){
                return -1;
            }


        }
    }
    
}
