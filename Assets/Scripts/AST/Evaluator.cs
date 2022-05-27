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
            if (!functions.ContainsKey(c.GetFunctionName())) {
                throw new Exception("Function does not exist");
            }
            Function function = functions[c.GetFunctionName()];
            if (c.GetX() == -1 && c.GetY() == -1) {
                function.Execute(tilemapGenerator, this, c.GetLoopX(), c.GetLoopY());  
            } else if (c.GetX() == -1) {
                function.Execute(tilemapGenerator, this, c.GetLoopX(), c.GetY());  
            } else if (c.GetY() == -1) {
                function.Execute(tilemapGenerator, this, c.GetX(), c.GetLoopY());  
            } else {
                function.Execute(tilemapGenerator, this, c.GetX(), c.GetY());
            }
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
            Color color = (Color)variables[f.GetColor()];
            Byte b = Convert.ToByte(color.GetB());
            Byte g = Convert.ToByte(color.GetG());
            Byte r = Convert.ToByte(color.GetR());
            Color32 color32 = new Color32(255, b, g, r);
            
            if (f.GetX() == -1 && f.GetY() == -1) {
                tilemapGenerator.Fill(f.GetLoopX()+f.GetPositionOffset().x, f.GetLoopY()+f.GetPositionOffset().y, f.GetWidth(), f.GetHeight(), color32);  
            } else if (f.GetX() == -1) {
                tilemapGenerator.Fill(f.GetLoopX()+f.GetPositionOffset().x, f.GetY()+f.GetPositionOffset().y, f.GetWidth(), f.GetHeight(), color32); 
            } else if (f.GetY() == -1) {
                tilemapGenerator.Fill(f.GetX()+f.GetPositionOffset().x, f.GetLoopY()+f.GetPositionOffset().y, f.GetWidth(), f.GetHeight(), color32); 
            } else {
                tilemapGenerator.Fill(f.GetX()+f.GetPositionOffset().x, f.GetY()+f.GetPositionOffset().y, f.GetWidth(), f.GetHeight(), color32);
            }
        }

        public void visit(TilemapGenerator tilemapGenerator, Function f)
        {
            functions.Add(f.GetName(), f);
            f.SetScope(functions.Count); // this works as long as we never remove any functions from the dictionary
        }

        public void visit(TilemapGenerator tilemapGenerator, Loop l)
        {
            // Problem: this simple implementation allows two or more loops of either X or Y to be nested
            // which does not make sense. We need to restrict nesting to two loops only one over x and the other over y.
            for (int i = l.GetFrom(); i <= l.GetTo(); i += l.GetStep())
            {
                foreach (Statement statement in l.GetStatements())
                {
                    if (l.GetIterator() == Iterator.X)
                    {
                        statement.SetLoopX(i);
                    }
                    else
                    {
                        statement.SetLoopY(i);
                    }
                    statement.Accept(tilemapGenerator, this);
                }
            }
        }

        public void visit(TilemapGenerator tilemapGenerator, If i)
        {
            Variable variable = variables[i.GetNoiseVariable()];
            if (!(variable is Noise))
            {
                throw new Exception("If variable should be of Noise type");
            }

            Noise noise = variable as Noise;
            // Implement GetInt in Noise
            // i.SetNoiseValue(noise.GetInt());
            if (i.EvaluateCondition())
            {
                foreach (Statement statement in i.GetStatements())
                {
                    statement.Accept(tilemapGenerator, this);
                }
            }
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