using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Noise : Variable
    {
        private readonly int _x;
        private readonly int _y;
        private readonly string _noiseMapName;
        private int _noise;
        private int _frequency;
        private int _scale;
        
        public Noise(string name, int x, int y, string noiseMapName) : base(name)
        {
            _x = x;
            _y = y;
            _noiseMapName = noiseMapName;
            CalculateNoise();
        }

        public void PutNoiseMapInfo(NoiseMap noiseMap)
        {
            _frequency = noiseMap.GetFrequency();
            _scale = noiseMap.GetScale();
        }

        private void CalculateNoise()
        {
            
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }

        public int GetNoise()
        {
            return _noise;
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