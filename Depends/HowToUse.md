# How to use

## Installing Package
    1) Create new .NET core 6.0 Console application
    2) Go to https://www.nuget.org/packages/neon-game-engine/ and install package in the project
## Getting started
    1) In program.cs type:
    using GameEngine;
    
    2) Then in class program, create Engine Object by:
        public static Engine engine = new Engine();
        
    3) Now set resolution and framerate using:
        engine.setResolution(100, 11);
        engine.defaultFramerate = fps;
        engine.setFramerate(fps);
    
    4) Now, to run engine just type
        engine.Init();
    
## Using the debug function
    To get more information about errors etc. just set debug mode to true by
        engine.debug = true;
    
## Creating object
    You can create simple object by:
        engine.CreateGameObject(name, description, X, Y, 'texture');
    
    Or you can create object group by:
        CreateGameObject(name, description, texture, Engine)
    
## Scripting
    Create script by 
        Script script = new Script();
    
    Assign Start and Update function to it by:
        script.Start = new Script.Start1(startFunction);
        script.Update = new Script.Update1(updateFunction);

## Background task
    Run background tasks using
        BackgroundTask backgroundTask = new BackgroundTask();
        backgroundgTask.backGroundTask = backGroundFunction;
        backgroundTask.start();

## Access objects
    You can easily modify object data using
        engine.GameObjects[objectID]

    For example, i can modify position of object using
        engine.GameObjects[Z].X = A;
        engine.GameObjects[Z].Y = B;

