using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Random : Variable
    {
        private int min;
        private int max;
        
        public Random(string name, int min, int max) : base(name)
        {
            this.min = min;
            this.max = max;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
        
        public int GetValue(System.Random r) {
            return r.Next(min, max);
        }

    }
}