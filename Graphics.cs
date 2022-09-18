using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class Line{
       public List<char> chars = new List<char>();

    }
    public class Graphics{
        //printing each character instead of full string at once
        public bool charRendering = false;
        public List<int> customColorID = new List<int>();
        public List<ConsoleColor> customColors = new List<ConsoleColor>();//requie charRendering to work
        public List<ConsoleColor> customColorsBack = new List<ConsoleColor>();//requie charRendering to work

        public ConsoleColor defaultFore, defaultBack;
        private ConsoleColor ForeColor;
        public bool debug = false;
        private ConsoleColor BackColor;
        private List<char[]> buffer = new List<char[]>();
        //Duplicate of gameObjects in engine(no memory efficient, fix later)
        public List<GameObject> gameObjects = new List<GameObject>();   
        public static int resolutionX, resolutionY = 10;
        //List of lines
        List<Line> lines = new List<Line>();
        //Intial function
        public void OnError(string sender,string errormsg, string error){            
            Console.Clear();
            if (debug){
                Console.WriteLine("From:" + sender + "\n" + errormsg + "\n" + error);
            }
            else{
                Console.WriteLine(errormsg);
            }            
            Console.Write("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(-1);
        }
        public void Init(){
            try{
                for (int i = 0; i < resolutionY; i++)
                {
                    lines.Add(new Line());
                }
                for (int i = 0; i < resolutionY; i++)
                {
                    buffer.Add(new char[resolutionX]);
                }
            }catch(Exception ex){
                OnError("graphics:init", ex.Message, ex.ToString());
    }

}

        public void changeColor(ConsoleColor foreColor, ConsoleColor backColor){
            try { 
            BackColor = backColor;
            ForeColor = foreColor;
            }
            catch (Exception ex){
                OnError("graphics:change_color",ex.Message, ex.ToString());
            }
        }
        //Update function for screen
        public void updateScreen(){
            try { 
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
            for(int i = 0; i < resolutionX; i++){
                toOutput += "_";
            }
            Console.ForegroundColor =ForeColor;
            Console.BackgroundColor =BackColor;
                //Printing every character each(slower, supports multiple colors)
                if (charRendering){
                    for (int i = 0; i < toOutput.Length; i++){
                        for(int j = 0; j < customColorID.Count; j++){
                            if (customColorID[j] == i){
                                Console.BackgroundColor = customColorsBack[j];
                                Console.ForegroundColor = customColors[j];
                            }
                        }
                        Console.Write(toOutput[i]);
                        Console.ForegroundColor = ForeColor;
                        Console.BackgroundColor = BackColor;
                    }
                    Console.Write("\n");
                }
                else{
                //Prints full sting at once
                    Console.WriteLine(toOutput);
                }
                
            //Console.WriteLine(toOutput);
            Console.ForegroundColor = defaultFore;
            Console.BackgroundColor = defaultBack;
            }
            catch (Exception ex){
                OnError("graphics:screen_update",ex.Message, ex.ToString());
            }
        }
        //Fills all empty things by spaces
        private void FillWithSpaces(){
            try { 
            for (int i = 0; i < buffer.Count; i++)
            {
                for (int j = 0; j < buffer[i].Length; j++)
                {
                    buffer[i][j] = ' ';
                }
            }
            }
            catch (Exception ex){
                OnError("graphics:fill_text_with_spaces",ex.Message, ex.ToString());
            }
        }
        public List<char[]> calculateGraphics()
        {
            try
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    buffer[gameObjects[i].Y][gameObjects[i].X] = gameObjects[i].graph;
                }
            }catch(Exception ex){
                OnError("graphics:calculate",ex.Message, ex.ToString());
            }
            return buffer;
        }
            
    }
}
    

