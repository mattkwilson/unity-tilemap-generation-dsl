using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Function : Element
    {
        private string name;
        private List<Statement> statements;

        private List<string> parameters; 

        public string GetName() {
            return name;
        }

        public List<string> GetParameters() {
            return parameters;
        }

        public void Execute(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v, int x, int y) {
            foreach (Statement statement in statements) {
                statement.SetScope(this.scope);
                statement.SetPosition(x, y);
                statement.SetParent(null);
                statement.Accept(tilemapGenerator, v);
            }                
        }

        public Function(string name, List<Statement> statements, List<string> parameters) {
            this.name = name;
            this.statements = statements;
            this.parameters = parameters;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
    }
}