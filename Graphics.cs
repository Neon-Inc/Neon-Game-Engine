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
        private List<char[]> buffer = new List<char[]>();
        //Duplicate of gameObjects in engine(no memory efficient, fix later)
        public List<GameObject> gameObjects = new List<GameObject>();   
        public static int resolutionX, resolutionY = 10;
        //List of lines
        List<Line> lines = new List<Line>();
        //Intial function
        public void Init(){

            for (int i = 0; i < resolutionY; i++){
                lines.Add(new Line());
            }
            for (int i = 0; i < resolutionY; i++)
            {
                buffer.Add(new char[resolutionX]);
            }

        }
        
        //Update function for screen
        public void updateScreen(){
            Console.Clear();
            FillWithSpaces();
            List<char[]> characters = calculateGraphics();
            

            string toOutput = "";
            for (int i = 0; i < characters.Count; i++) { 
                for(int j = 0; j< characters[i].Length; j++){
                    toOutput += characters[i][j].ToString();
                }
                toOutput += "|\n";
                
            }

            Console.WriteLine(toOutput);


        }
        //Fills all empty things by spaces
        private void FillWithSpaces(){
            for (int i = 0; i < buffer.Count; i++)
            {
                for (int j = 0; j < buffer[i].Length; j++)
                {
                    buffer[i][j] = ' ';
                }
            }
        }

        public List<char[]> calculateGraphics(){    
            for (int i = 0;i < gameObjects.Count;i ++){
                buffer[gameObjects[i].Y][gameObjects[i].X] = gameObjects[i].graph;
            }
            
            return buffer;
        }
            
    }
}
    

