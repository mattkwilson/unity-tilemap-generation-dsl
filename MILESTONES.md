# Milestone 1

## Summary

Our DSL will simplify the process of procedurally generating graphical terrain for use in video games and other applications. This is useful for game developers, artists and anybody who wants to create a 2d graphical terrain map. This could be used to create tile maps for 2d video games, or topographical maps for geological engineers. It will be much easier for an artist or non-software engineer to make use of this DSL as opposed to using a programming language as it greatly reduces the complexity of creating these graphical terrain maps.
<br /><br /><br />
## Features

- Functions

- Can define functions for generating specific terrain in multiple places

- Conditionals

- Can set blocks based on surrounding terrain

- Loops

- Can loop through map coordinates to set blocks/terrain

- Recursion (maybe)

- Can recursively set blocks of terrain for running algorithms (flood fill, etc)

## Division of Responsibilities

- DSL

  - Lexar (Parse string into tokens) - everyone

  - Parser (Parse tokens into AST) - everyone

  - Evaluation (Convert AST) - Jeffrey

  - Internal data structure -Tarek

- Procedural Generation Framework - Matt

- Graphics - Edvin, Mert

  - 2d graph library - Edvin

  - Image generation - Mert

- User Study 1 - Edvin, Mert

- User Study 2 - Jeffrey, Tarek, Matt

- Creating the video - everyone

## Timeline

Milestone 1 - Fri May 20th

First Draft of DSL - Sat May 21st

Prototype User Study - Sun May 22nd

Finalize Design of DSL: implementation plan - Mon May 23rd

Major Language Features Implemented - Thurs May 26th

Milestone 2 - Fri May 27th

Finalize Language - Sat May 28th

Task-Driven User Study - Sun May 29th

5 Minute Video Completed - Thurs June 2nd

Project Due - Thurs June 2nd

## Meeting Notes

- Ensure we have enough features to make our DSL viable

- Add more complexity to the program, so that it isn’t just a simplified paint editor, but provides a more useful product

- Focus on the complex terrain generation

- Add textures

# Rough Ideas and Notes

## App structure

- Could use Java to generate simple 2d graphics

- Terrain is made up of 1x1 blocks (or pixels)

- Terrain generated using layers of noise

- Using random noise generators (Perlin Noise)



## DSL

1) who you think would want to use your DSL, 

- Game developers making 2d games that require procedural terrain

2) what they would use it for, and 

- Use it for randomly generating tilemaps

3) why it would make sense for them to use it instead of, for example, a general purpose programming language

- Makes it easy to quickly define new randomly generated terrain maps instead of having to code them directly



## DSL Grammar Example

Generate Terrain : TerrainLayer+

TerrainLayer : Noise* Color?

Color : Byte Byte Byte Byte 


Byte : [0-255]

Noise :  X Y Scale Magnitude

X : \d

Y: \d

Scale: \d

Magnitude: \d





## Potential Features (to add complexity)

- Could add textures

- Ability to set specific blocks to a certain color/texture

- Can add loops for looping over x/y coordinates to set blocks

- Create conditions for setting blocks

- bit masking

- Define function for creating graphical objects


  - Ex. setup function to build a house using floor, wall, and roof textures


  - In a loop Call the build house function in various different locations based on random values

- Expand to create levels for a game





## Potential Issues

- Just need to  ensure if we can make it rich/powerful enough to be a strong dsl



## Example Code (Setting and modifying blocks/tiles)



Canvas 100 100 




Color blockColor r5 g255 b200



Fill x5 y6 w10 h15 blockColor



Fill x0 y0 w100 h1 blockColor // first row of pixel is filled

Fill color // fill the whole canvas 



function building: // drawing relative to where this function was called

Fill x0 y0 w10 h10 white

Fill x5 y5 w1 h1 black



For i 0 to 10 step 5

For j 0 to 10 step 5

Fill xi yj w2 h2 black



Loop start0 step2 end5 i

Fill x5+i y10 w1 h1 blockColor



Loop j step

Fill x5 y10+step i h1 blockColor

# Milestone 2

## Progress Summary

Up to this point we have almost completed the implementation of our DSL. We have finished implementing our lexer and parser rules, and are well on our way to completing the implementation of the visitor pattern for converting the parse tree into our AST and completing our evaluator.

- Finished lexer and parser grammar rules.
- Planned the structure of our AST.
- Used visitor pattern to convert parse tree into AST. 
- Planned to use visitor pattern for the evaluator that executes the compiled program.

We have also performed our first prototype user study, and are preparing to perform our second user study this weekend.

### Division of Responsibilities (updated)

- DSL

  - Lexar (Parse string into tokens) - everyone

  - Parser (Parse tokens into AST) - everyone

  - Evaluation (Convert AST) - everyone
    - Function - Matt
    - Loop - Tarek
    - If - Tarek
    - Canvas - Mert
    - Program - Mert
    - Statement - Mert
    - Fill - Jeffrey
    - Call - Jeffrey
    - Noise - Edvin
    - Noisemap - Edvin
    - Color - Edvin

  - Internal data structure - everyone

- Unity - Matt

- User Study 1 - Edvin, Mert

- User Study 2 - Jeffrey, Tarek, Matt

- Creating the video - everyone

### Timeline

~~Milestone 1 - Fri May 20th~~

~~First Draft of DSL - Sat May 21st~~

~~Prototype User Study - Sun May 22nd~~

~~Finalize Design of DSL: implementation plan - Mon May 23rd~~

~~Major Language Features Implemented - Thurs May 26th~~

~~Milestone 2 - Fri May 27th~~

Finalize Language - Sat May 28th

Task-Driven User Study - Sun May 29th

5 Minute Video Completed - Thurs June 2nd

Project Due - Thurs June 2nd

### Prototype User Study

#### User Description

For our first user study we had 2 users. Both are students who have experience coding but have never worked on DSL before. 

#### Example Programs and Provided Information

The user studies were done separately to prevent one learning from the other. We first provided the users information about our Language Design. The info provided included  our DSL’s syntax list, presented in 3 categories: names of the parameters, types of the parameters, and a short description of each syntax. Example code and sample output map/image were also provided. The investigator only answered questions prior to the user starting tasks as means of clarification on Language Design and did not help the user with the tasks. 


Example of using noise

Canvas 100 100
Color Red 255 0 0
NoiseMap NoiseMap1 10 20
Noise Noise1 10 15 NoiseMap1
If Noise1 < 5
	Fill 10 10 2 2 Red


#### Tasks

The users were each provided with three to four tasks. First two tasks focused more on basic functionality such as creating a canvas, and filling some parts of the canvas with color. One user forgot to create a Color variable but neither had major issues using basic 
functionality. Both users were prompted using noisemaps generate the following:  place 
green terrain on pixels where noise > 3’ as the third task. The user figured he had to  create 
a noisemap and then loop through all of the canvas, find the noise for each coordinate and 
fill said coordinate if the condition was met. User #1 was able to figure out how to use our 
syntax correctly on his first try while User #2 tried to get one noise value for the 
coordinate (0,0) then fill the whole canvas with that one value/color. After being reminded 
to check the Language Design he achieved the correct result in his second try. User #2 had 
a fourth task which involved using multiple noisemaps to have more than one coloring, he 
got it right in his first try.

Create 10x10 blue square in somewhere on canvas

Create 100x100 canvas, fill all in red

Use noise to generate blue terrain on pixels where noise > 5, for all of map. For all noisemaps set frequency and scale to 3

Using separate noisemaps generate the following:  green terrain on pixels where noise > 3, blue terrain on pixels where noise > 10, for all of map. For all noisemaps set frequency and scale to 3


#### Task Performance Analysis and Feedback

The users were both mostly satisfied with the grammar of our Language Design. They 
found it easy and clear. But they both asked about how Noise worked prior to starting their tasks as they didn’t fully understand what it did/how it worked. Also, both users weren’t sure about white spaces and code format.  As a result of our user study we added keywords that define end of functions, loops, and if statements as well as colons to define declarations of functions, calls, noise and noisemaps to make our DSL easier to read and better defined. We will also strengthen syntax description for our next user study.

Our users found our DSL design to be approachable and only had issues with indentation and syntax as we didn’t have anything to specify declarations and if/loop/function body endings. 

#### Conclusions

We decided to addq colons to declare variables and functions. We also added 3 keywords EndLoop, EndFunction, and EndIf. 


## Language Design

### Program Features

Creating a tile map
Setting tiles particular colors
Using noisemaps to generate pseudo-random terrain
### Language Features (Current Project Scope)

Change Note:  We  decided not to do recursion in order to reduce the complexity of the underlying implementation of the project, as we felt there was enough functionality that could be accomplished through loops.

- Functions

  - Can define functions for generating specific terrain in multiple places

- Conditionals

  - Can set blocks based on surrounding terrain

- Loops

  - Can loop through map coordinates to set blocks/terrain

### Grammar 

Program : Canvas (Statement | Function)*;

Statement: (Loop | If | Variables | Call | Fill) ;

Variables : (Color | NoiseMap | Noise);

Canvas : ‘Canvas’ Integer Integer;

Color : ‘Color’ Name Integer Integer Integer;

NoiseMap: ‘NoiseMap:’ Name Integer Integer;

Noise: ‘Noise:’ Name Integer Integer NoiseMap;

Fill: ‘Fill’ Integer Integer Integer Integer Color;

Loop: ‘Loop’ LoopVar ‘from’ Integer ‘to’ Integer ‘step’ Integer ‘EndLoop’;

If: ‘If’ Number Condition Number ‘EndIf’;

Function: ‘Function:’ Name (Variables | Call | Statement | Fill)* ‘EndFunction’;

Call: ‘Call:’ Name Integer Integer;

### Code Samples

```
Function: CreateTerrain
	Color: Blue 0 0 255
	Color: Green 0 255 0

	NoiseMap: NoiseMap1 3 3
	NoiseMap: NoiseMap2 4 5

	Loop (x from 0 to 100 step 1)
		Loop (y from 0 to 100 step 1)
			Noise: Noise1 x y NoiseMap1
			Noise: Noise2 x y NoiseMap2
			If (Noise1 >  3)
				Fill x y 1 1 Blue
			EndIf
			If (Noise2 > 4) 
				Fill x y 1 1 Green
			EndIf
		EndLoop
	EndLoop
EndFunction


Canvas 100 100
Call CreateTerrain 0 0
```
Output: Using noise to generate blue terrain


### Current Limitations
- Functions have no ability to define parameters (only the predefined and required  X and Y)
- Functions perform statements relative to input X and Y position
- Loops are limited to two nested loops, one iterating over X and the other over Y
- If statement are limited to evaluating a condition of this format (NOISE_VAR COND INT)
- The variables we have are not mutable

### Potential Features (to add complexity)

- Add textures
- Reduce limitations of current language features

## Implementation Plan

### Summary

Following our original plan we first completed the implementation of the Lexer.g4 and Parser.g4 grammar files all together, and then designed and implemented our AST. Then following the visitor pattern we set up a framework to use to parse the AST and evaluate the tree. We then divided the implementation for those based on the individual elements of our language (as outlined in our division of responsibilities). 

At this point we are finishing up work on the implementation of our evaluator and will be merging all our work this weekend, fixing bugs and testing. After everything is working we will perform our last user study and based on the feedback we receive from our TA we will consider expanding our language by reducing limitations and potentially adding new features (if time allows). 

Once all of that is complete we will finalize the project and create our final video beginning early next week.

### Which language are you using for your implementation?

C#

### Which libraries/frameworks/tools are you  using for your language frontend (i.e. parsing your input)?

We are creating a Unity editor extension for the Unity game engine.

### Which libraries/frameworks/tools are you using for your language backend?

We are using the UnityEngine framework, as well as general .NET core libraries.

## Meeting Notes
Improve richness of language by adding function parameters on top of current defined X, Y variables (maintaining relative positioning)
Adding textures would greatly improve end product
Otherwise features are looking good and language is powerful enough considering we could define shapes and objects with functions


### Project Scope and Remaining Timeline (any changes?)

We will be increasing our project scope by adding function parameters, and will consider adding textures. Our current timeline will remain the same (refer to timeline above). 
