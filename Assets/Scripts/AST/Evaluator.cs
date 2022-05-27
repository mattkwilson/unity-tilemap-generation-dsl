using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    // Map: name -> [] or name -> Variable implemented by NoiseValue NoiseMapValue ColorValue
    public class Evaluator : ITilemapDSLVisitor
    {
        private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
        private Dictionary<string, Function> functions = new Dictionary<string, Function>();

        public void visit(TilemapGenerator tilemapGenerator, Program p)
        {
            throw new System.NotImplementedException();
        }

        public void visit(TilemapGenerator tilemapGenerator, Call c)
        {
            // Get function from dictionary using it's name
            // Call function Execute method
            if (!functions.ContainsKey(c.getFunctionName())) {
                throw new Exception("Function does not exist");
            }
            Function function = functions[c.getFunctionName()];
            function.Execute(tilemapGenerator, this, c.getX(), c.getY());;
        }

        public void visit(TilemapGenerator tilemapGenerator, Statement c)
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
            Color32 color = new Color32(255, f.getColor().getB(), f.getColor().getG(), f.getColor.getR());
            tilemapGenerator.Fill(f.getX(), f.getY(), f.getWidth(), f.getHeight(), color);
        }

        public void visit(TilemapGenerator tilemapGenerator, Function f)
        {
            functions.Add(f.GetName(), f);
            f.SetScope(functions.Count); // this works as long as we never remove any functions from the dictionary
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