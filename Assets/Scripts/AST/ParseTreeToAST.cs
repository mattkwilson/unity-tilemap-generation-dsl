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
            return base.VisitCall(context);
        }

        public override ASTBase VisitCanvas([NotNull] TilemapDSLParser.CanvasContext context)
        {
            return base.VisitCanvas(context);
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
            return base.VisitFill(context);
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
            return base.VisitProgram(context);
        }

        public override ASTBase VisitStatement([NotNull] TilemapDSLParser.StatementContext context)
        {
            return base.VisitStatement(context);
        }

    }
}