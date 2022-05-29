# CPSC 410 - Project 1
Procedural Terrain Generation DSL in Unity

## Antlr
Generate C# source files for grammar file using: `antlr4 -Dlanguage=CSharp -visitor TilemapDSLLexer.g4 TilemapDSLParser.g4 -o ./gen`

It will generate source files for both the lexer and parser in the Assets/Antlr/gen folder.

