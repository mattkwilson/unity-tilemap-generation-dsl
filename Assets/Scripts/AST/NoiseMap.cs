using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class NoiseMap : Variable
    {
        private readonly int _frequency;
        private readonly int _scale;
        public NoiseMap(string name, int frequency, int scale) : base(name)
        {
            _frequency = frequency;
            _scale = scale;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
        
        public int GetFrequency() {
            return _frequency;
        }
        
        public int GetScale() {
            return _scale;
        }
    }
}