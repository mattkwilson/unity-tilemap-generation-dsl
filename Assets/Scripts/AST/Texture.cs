using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Texture : Variable
    {
        private readonly int _index;
        public Texture(string name, int index) : base(name)
        {
            _index = index;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
        
        public int GetIndex() {
            return _index;
        }
        
    }
}