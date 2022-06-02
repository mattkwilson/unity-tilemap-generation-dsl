using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class ParseTreeToAST : TilemapDSLParserBaseVisitor<ASTBase>
    {
        public override ASTBase VisitCall([NotNull] TilemapDSLParser.CallContext context)
        {
            int x;
            int y;
            string functionName = context.TEXT()[0].GetText();
            switch (context.VAR().Length)
            {
                case 2:
                    x = -1;
                    y = -1;
                    break;
                case 1:
                    if (context.VAR()[0].GetText() == "x") {
                        x = -1;
                        y = Int32.Parse(context.INTEGER()[0].GetText());
                    } else {
                        x = Int32.Parse(context.INTEGER()[0].GetText());
                        y = -1;
                    }
                    break;
                case 0:
                    x = Int32.Parse(context.INTEGER()[0].GetText());
                    y = Int32.Parse(context.INTEGER()[1].GetText());
                    break;
                default:
                    throw new Exception("Unexpected error parsing Call");
            }
            List<string> args = new List<string>();
            for(int i = 1; i < context.TEXT().Length; i++) {
                args.Add(context.TEXT()[i].GetText());
            }
            if(args.Count > 0 && context.FUNCTION_PARAM_END() == null) {
                throw new Exception("Missing ending bracket of argument declaration");
            }

            return new Call(functionName, x, y, args);
        }

        public override ASTBase VisitCanvas([NotNull] TilemapDSLParser.CanvasContext context)
        {
            int w = Int32.Parse(context.INTEGER()[0].GetText());
			int h = Int32.Parse(context.INTEGER()[1].GetText());
			return new Canvas(w, h);
        }

        public override ASTBase VisitTexture([NotNull] TilemapDSLParser.TextureContext context)
        {
            string name = context.TEXT().GetText();
            int index = Int32.Parse(context.INTEGER().GetText());
            return new Texture(name, index);
        }

        public override ASTBase VisitColor([NotNull] TilemapDSLParser.ColorContext context)
        {
            string name = context.TEXT().GetText();
            int r = Int32.Parse(context.INTEGER()[0].GetText());
            int g = Int32.Parse(context.INTEGER()[1].GetText());
            int b = Int32.Parse(context.INTEGER()[2].GetText());
            return new Color(name, r, g, b);
        }


        public override ASTBase VisitFill([NotNull] TilemapDSLParser.FillContext context)
        {
            int x;
            int y;
            int width;
            int height;
            if (context.VAR().Length > 0) {
                if (context.VAR().Length == 1) {
                    if (context.VAR()[0].GetText() == "x") {
                        x = -1;
                        y = Int32.Parse(context.INTEGER()[0].GetText());
                        width = Int32.Parse(context.INTEGER()[1].GetText());
                        height = Int32.Parse(context.INTEGER()[2].GetText());
                    } else {
                        x = Int32.Parse(context.INTEGER()[0].GetText());
                        y = -1;
                        width = Int32.Parse(context.INTEGER()[1].GetText());
                        height = Int32.Parse(context.INTEGER()[2].GetText());
                    }
                } else {
                    x = -1;
                    y = -1;
                    width = Int32.Parse(context.INTEGER()[0].GetText());
                    height = Int32.Parse(context.INTEGER()[1].GetText());
                }
            } else {
                x = Int32.Parse(context.INTEGER()[0].GetText());
                y = Int32.Parse(context.INTEGER()[1].GetText());
                width = Int32.Parse(context.INTEGER()[2].GetText());
                height = Int32.Parse(context.INTEGER()[3].GetText());
            }
            string texture = context.TEXT().GetText();
            return new Fill(x, y, width, height, texture);
        }

        public override ASTBase VisitFunction([NotNull] TilemapDSLParser.FunctionContext context)
        {
            string name = context.TEXT()[0].GetText();
            List<Statement> statements = new List<Statement>(); 
            foreach (TilemapDSLParser.StatementContext statement in context.statement()) {
                statements.Add(VisitStatement(statement) as Statement);         
            }  
            List<string> parameters = new List<string>();
            if(context.FUNCTION_PARAM_START() != null) {
                for(int i = 1; i < context.TEXT().Length; i++) {
                    parameters.Add(context.TEXT()[i].GetText());
                }   
                if(context.FUNCTION_PARAM_END() == null) {
                    throw new Exception("Missing end of parameter declaration");
                }
            }
                   
            if(context.FUNCTION_END() == null) {
                throw new Exception("Missing end of function");
            }
            return new Function(name, statements, parameters);
        }

        public override ASTBase VisitIf([NotNull] TilemapDSLParser.IfContext context)
        {
            if (context.IF_END() == null)
            {
                throw new Exception("Missing EndIf");
            }
            string arg = context.TEXT().GetText();
            string condition = context.CONDITION().GetText();
            int number = Int32.Parse(context.INTEGER().GetText());
            List<Statement> statements = new List<Statement>();
            foreach (TilemapDSLParser.StatementContext statement in context.statement()) {
                statements.Add(VisitStatement(statement) as Statement);         
            }
            return new If(arg, condition, number, statements);
        }

        public override ASTBase VisitLoop([NotNull] TilemapDSLParser.LoopContext context)
        {
            if (context.LOOP_END() == null)
            {
                throw new Exception("Missing EndLoop");
            }
            IteratorType iterator = (context.VAR().GetText() == "x")? IteratorType.X : IteratorType.Y;
            int from = Int32.Parse(context.INTEGER()[0].GetText());
            int to   = Int32.Parse(context.INTEGER()[1].GetText());
            int step = Int32.Parse(context.INTEGER()[2].GetText());
            List<Statement> statements = new List<Statement>();
            foreach (TilemapDSLParser.StatementContext statementContext in context.statement())
            {
                statements.Add(VisitStatement(statementContext) as Statement);
            }
            return new Loop(iterator, from, to, step, statements);
        }

        public override ASTBase VisitNoise([NotNull] TilemapDSLParser.NoiseContext context)
        {
            string name = context.TEXT()[0].GetText();
            int x;
            int y;
            if (context.VAR().Length > 0)
            {
                if (context.VAR().Length == 1)
                {
                    if (context.VAR()[0].GetText() == "x")
                    {
                        x = -1;
                        y = Int32.Parse(context.INTEGER()[0].GetText());
                    }
                    else
                    {
                        x = Int32.Parse(context.INTEGER()[0].GetText());
                        y = -1;
                    }
                }
                else
                {
                    x = -1;
                    y = -1;                    
                }
            }
            else
            {
                x = Int32.Parse(context.INTEGER()[0].GetText());
                y = Int32.Parse(context.INTEGER()[1].GetText());                
            }         
            string noiseMapName = context.TEXT()[1].GetText();
            return new Noise(name, x, y, noiseMapName);
        }

        public override ASTBase VisitNoiseMap([NotNull] TilemapDSLParser.NoiseMapContext context)
        {
            string name = context.TEXT().GetText();
            int frequency = Int32.Parse(context.INTEGER()[0].GetText());
            int scale = Int32.Parse(context.INTEGER()[1].GetText());
            return new NoiseMap(name, frequency, scale);
        }

        public override ASTBase VisitProgram([NotNull] TilemapDSLParser.ProgramContext context)
        {
            List<Function> functions = new List<Function>();
			List<Statement> statements = new List<Statement>();
			Canvas canvas;
			foreach (TilemapDSLParser.StatementContext statementContext in context.statement())
            {
                statements.Add(VisitStatement(statementContext) as Statement);
            }
			foreach (TilemapDSLParser.FunctionContext functionContext in context.function())
            {
                functions.Add(VisitFunction(functionContext) as Function);
            }
			canvas = VisitCanvas(context.canvas()) as Canvas;
			return new Program(canvas, statements, functions);
        }

        public override ASTBase VisitStatement([NotNull] TilemapDSLParser.StatementContext context)
        {
            if (context.loop() != null) {
				return VisitLoop(context.loop());
			} else if (context.@if() != null) {
				return VisitIf(context.@if());
			} else if (context.fill()!= null) {
				return VisitFill(context.fill());
			} else if (context.call()!= null) {
				return VisitCall(context.call());
			} else {
				return VisitVariable(context.variable());
			}
        }

        public override ASTBase VisitVariable([NotNull] TilemapDSLParser.VariableContext context)
        {
            if (context.color() != null)
            {
                return VisitColor(context.color());
            }

            if (context.noise() != null)
            {
                return VisitNoise(context.noise());
            }

            if (context.noiseMap() != null)
            {
                return VisitNoiseMap(context.noiseMap());
            }

            if (context.texture() != null)
            {
                return VisitTexture(context.texture());
            }

            if (context.random() != null)
            {
                return VisitRandom(context.random());
            }

            throw new Exception("Unexpected error parsing variable");
        }

        public override ASTBase VisitRandom([NotNull] TilemapDSLParser.RandomContext context)
        {
            string name = context.TEXT().GetText();
            int min = Int32.Parse(context.INTEGER()[0].GetText());
            int max = Int32.Parse(context.INTEGER()[1].GetText());
            return new Random(name, min, max);
        }
    }
}