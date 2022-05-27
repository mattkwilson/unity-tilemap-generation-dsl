using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class ParseTreeToAST : TilemapDSLParserBaseVisitor<ASTBase>
    {
        public override ASTBase VisitCall([NotNull] TilemapDSLParser.CallContext context)
        {
            int x;
            int y;
            string functionName = context.TEXT().GetText();
            if (context.VAR() != null) {
                if (context.VAR().Length == 1) {
                    if (context.VAR()[0].GetText() == "x") {
                        x = -1;
                        y = Int32.Parse(context.INTEGER()[0].GetText());
                    } else {
                        x = Int32.Parse(context.INTEGER()[0].GetText());
                        y = -1;
                    }
                } else {
                    x = -1;
                    y = -1;
                }
            } else {
                x = Int32.Parse(context.INTEGER()[0].GetText());
                y = Int32.Parse(context.INTEGER()[1].GetText());
            }
            return new Call(functionName, x, y);
        }

        public override ASTBase VisitCanvas([NotNull] TilemapDSLParser.CanvasContext context)
        {
            int w = Int32.Parse(context.INTEGER()[0].GetText());
			int h = Int32.Parse(context.INTEGER()[1].GetText());
			return new Canvas(w, h);
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
            if (context.VAR() != null) {
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
            string color = context.TEXT().GetText();
            return new Fill(x, y, width, height, color);
        }

        public override ASTBase VisitFunction([NotNull] TilemapDSLParser.FunctionContext context)
        {
            string name = context.TEXT().GetText();
            List<Statement> statements = new List<Statement>(); 
            foreach (TilemapDSLParser.StatementContext statement in context.statement()) {
                statements.Add(VisitStatement(statement) as Statement);         
            }            
            return new Function(name, statements);
        }

        public override ASTBase VisitIf([NotNull] TilemapDSLParser.IfContext context)
        {
            string noise = context.TEXT().GetText();
            string condition = context.CONDITION().GetText();
            int number = Int32.Parse(context.INTEGER().GetText());
            List<Statement> statements = new List<Statement>();
            foreach (TilemapDSLParser.StatementContext statement in context.statement()) {
                statements.Add(VisitStatement(statement) as Statement);         
            }
            return new If(noise, condition, number, statements);
        }

        public override ASTBase VisitLoop([NotNull] TilemapDSLParser.LoopContext context)
        {
            Iterator iterator = (context.VAR().GetText() == "x")? Iterator.X : Iterator.Y;
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
            int x = Int32.Parse(context.INTEGER()[0].GetText());
            int y = Int32.Parse(context.INTEGER()[1].GetText());
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
			Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
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
			return new Program(canvas, variables, statements, functions);
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

    }
}