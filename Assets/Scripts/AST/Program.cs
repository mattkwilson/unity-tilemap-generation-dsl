using System.Collections.Generic;
using System;

namespace Assets.Scripts.AST
{
    public class Program : ASTBase
    {
        private Canvas canvas;
        private Dictionary<string, Variable> variables;
        private List<Statement> statements;
        private List<Function> functions;

        public List<Statement> getStatements()
        {
            return statements;
        }

        public List<Function> getFunctions()
        {
            return functions;
        }

        public Canvas getCanvas()
        {
            return canvas;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="functions"></param>
        public Program(Canvas canvas, Dictionary<string, Variable> variables, List<Statement> statements, List<Function> functions)
        {
            // TODO: implement constructor
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this)
        }
    }
}