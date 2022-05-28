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
        private int _loopX = -1;
        private int _loopY = -1;

        public void visit(TilemapGenerator tilemapGenerator, Program p)
        {
            Canvas canvas = p.getCanvas();
            canvas.Accept(tilemapGenerator, this);
            foreach (Function function in p.getFunctions())
            {
                function.Accept(tilemapGenerator, this);
            }

            foreach (Statement statement in p.getStatements())
            {
                statement.Accept(tilemapGenerator, this);
            }
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
            tilemapGenerator.Canvas(c.getWidth(), c.getHeight());
        }

        public void visit(TilemapGenerator tilemapGenerator, Color c)
        {
            variables.Add(c.GetName(), c);
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
            ref int i = ref _loopX;
            if (l.GetIterator() == Iterator.Y)
            {
                i = ref _loopY;
            }
            if (i != -1)
            {
                throw new Exception("Loops cannot nest a loop with the same iterator");
            }
            for (i = l.GetFrom(); i <= l.GetTo(); i += l.GetStep())
            {
                foreach (Statement statement in l.GetStatements())
                {
                    if (_loopX != -1)
                    {
                        statement.SetX(i);
                    }
                    if (_loopY != -1)
                    {
                        statement.SetY(i);
                    }
                    statement.Accept(tilemapGenerator, this);
                }
            }
            i = -1;
        }

        public void visit(TilemapGenerator tilemapGenerator, If i)
        {
            if (variables[i.GetNoiseVariable()] is not Noise noise)
            {
                throw new Exception("If variable should be of Noise type");
            }
            i.SetNoiseValue(noise.GetNoise());
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
            if (variables[n.GetNoiseMapName()] is NoiseMap)
            {
                n.PutNoiseMapInfo(variables[n.GetNoiseMapName()] as NoiseMap);
            }
            variables.Add(n.GetName(), n);
        }

        public void visit(TilemapGenerator tilemapGenerator, NoiseMap n)
        {
            variables.Add(n.GetName(), n);
        }
    }
}