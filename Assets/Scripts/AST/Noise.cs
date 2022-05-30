using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Noise : Variable
    {
        private readonly int _x;
        private readonly int _y;
        private readonly string _noiseMapName;
        private int _noiseValue;
        
        public Noise(string name, int x, int y, string noiseMapName) : base(name)
        {
            _x = x;
            _y = y;
            _noiseMapName = noiseMapName;
        }
        public void CalculateNoise(NoiseMap noiseMap)
        {
            float frequency = noiseMap.GetFrequency();
            int scale = noiseMap.GetScale();
            _noiseValue = Mathf.RoundToInt(Mathf.PerlinNoise(_x / frequency, _y / frequency) * scale);
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }

        public int GetNoise()
        {
            return _noiseValue;
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