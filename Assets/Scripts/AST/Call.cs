using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Call : Statement
    {
        private string functionName;

        public Call(string functionName, int x, int y) {
            this.functionName = functionName;
            SetPositionOffset(x, y);
        }

        public string getFunctionName() {
            return functionName;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v) {
            v.visit(tilemapGenerator, this);
        }
    }
}