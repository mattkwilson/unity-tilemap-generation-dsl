using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Call : Statement
    {
        private int x, y;
        private string functionName;

        public Call(string functionName, int x, int y) {
            this.functionName = functionName;
            this.x = x;
            this.y = y;
        }

        public int GetX() {
            return x;
        }

        public int GetY() {
            return y;
        }

        public string GetFunctionName() {
            return functionName;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v) {
            v.visit(tilemapGenerator, this);
        }
    }
}