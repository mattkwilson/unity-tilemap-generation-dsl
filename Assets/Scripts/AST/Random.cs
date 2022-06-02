using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Random : Variable
    {
        private int min;
        private int max;
        private int value;
        
        public Random(string name, int min, int max) : base(name)
        {
            this.min = min;
            this.max = max;
        }

        public void GenerateRandom(System.Random r) {
            this.value = r.Next(min, max + 1);
        }
 
        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
        
        public int GetValue()
        {
            return value;
        }

    }
}