using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Color : Variable
    {
        private readonly int _r;
        private readonly int _g;
        private readonly int _b;
        public Color(string name, int r, int g, int b) : base(name)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
        
        public int GetR() {
            return _r;
        }
        
        public int GetG() {
            return _g;
        }
        
        public int GetB() {
            return _b;
        }
    }
}