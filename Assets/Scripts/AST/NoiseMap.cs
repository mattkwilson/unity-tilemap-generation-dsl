using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class NoiseMap : Variable
    {
        private readonly string _name;
        private readonly int _frequency;
        private readonly int _scale;
        public NoiseMap(string name, int frequency, int scale)
        {
            _name = name;
            _frequency = frequency;
            _scale = scale;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
        
        public string GetName() {
            return _name;
        }
        
        public int GetFrequency() {
            return _frequency;
        }
        
        public int GetScale() {
            return _scale;
        }
    }
}