using System.Collections.Generic;
using System;

namespace Assets.Scripts.AST
{
    public class Program : ASTBase
    {
        private Canvas _canvas;
        private List<Statement> _statements;
        private List<Function> _functions;

        public Program(Canvas canvas, List<Statement> statements, List<Function> functions)
        {
            _canvas = canvas;
            _statements = statements;
            _functions = functions;
        }

		    public List<Statement> getStatements()
        {
            return _statements;
        }

        public List<Function> getFunctions()
        {
            return _functions;
        }

        public Canvas getCanvas()
        {
            return _canvas;
        }
    
    
        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
    }
}