using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Call : Statement
    {
        private int x, y;
        private string functionName;
        private List<string> args;

        public Call(string functionName, int x, int y, List<string> args) {
            this.functionName = functionName;
            this.x = x;
            this.y = y;
            this.args = args;
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

        public List<string> GetArgs() {
            return args;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v) {
            v.visit(tilemapGenerator, this);
        }
    }
}