using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Noise : Variable
    {
        private readonly string _name;
        private readonly int _x;
        private readonly int _y;
        private readonly string _noiseMapName;
        public Noise(string name, int x, int y, string noiseMapName)
        {
            _name = name;
            _x = x;
            _y = y;
            _noiseMapName = noiseMapName;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
        
        public string GetName() {
            return _name;
        }
        
        public int GetX() {
            return _x;
        }
        
        public int GetY() {
            return _y;
        }
        
        public string GetNoiseMapName() {
            return _noiseMapName;
        }
    }
}