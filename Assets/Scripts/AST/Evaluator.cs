using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    // Map: name -> [] or name -> Variable implemented by NoiseValue NoiseMapValue ColorValue
    public class Evaluator : ITilemapDSLVisitor
    {
        private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

        public void visit(TilemapGenerator tilemapGenerator, Program p)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, Call c)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, Canvas c)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, Color c)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, Fill f)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, Function f)
        {
            
        }

        public void visit(TilemapGenerator tilemapGenerator, Loop l)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, Noise n)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, NoiseMap n)
        {
            throw new System.NotImplementedException();
        }
    }
}