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
