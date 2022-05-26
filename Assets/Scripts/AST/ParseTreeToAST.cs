using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System.Collections;
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
            return base.VisitColor(context);
        }

        public override ASTBase VisitFill([NotNull] TilemapDSLParser.FillContext context)
        {
            return base.VisitFill(context);
        }

        public override ASTBase VisitFunction([NotNull] TilemapDSLParser.FunctionContext context)
        {
            return base.VisitFunction(context);
        }

        public override ASTBase VisitIf([NotNull] TilemapDSLParser.IfContext context)
        {
            return base.VisitIf(context);
        }

        public override ASTBase VisitLoop([NotNull] TilemapDSLParser.LoopContext context)
        {
            return base.VisitLoop(context);
        }

        public override ASTBase VisitNoise([NotNull] TilemapDSLParser.NoiseContext context)
        {
            return base.VisitNoise(context);
        }

        public override ASTBase VisitNoiseMap([NotNull] TilemapDSLParser.NoiseMapContext context)
        {
            return base.VisitNoiseMap(context);
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