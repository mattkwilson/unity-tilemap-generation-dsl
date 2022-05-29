using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Function : Element
    {
        private string name;
        private List<Statement> statements;

        public string GetName() {
            return name;
        }

        public void Execute(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v, int x, int y) {
            foreach (Statement statement in statements) {
                statement.SetScope(this.scope);
                statement.SetPosition(x, y);
                statement.SetParent(null);
                v.visit(tilemapGenerator, statement);
            }                
        }

        public Function(string name, List<Statement> statements) {
            this.name = name;
            this.statements = statements;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
    }
}