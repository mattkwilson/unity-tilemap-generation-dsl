using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Function : ASTBase
    {
        private string name;
        private List<Statement> statements;

        public Function(string name, List<Statement> statements) {
            this.name = name;
            this.statements = statements;
        }

        public override void accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
    }
}