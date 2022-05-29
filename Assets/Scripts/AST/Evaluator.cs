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
            LoopVariable x = null, y = null;
            if(c.GetX() == -1) {
                x = findMatchingLoopVar(c, IteratorType.X);
                if(x == null) {
                    throw new Exception("No loop variable named: x");
                }
            }
            if(c.GetY() == -1) {
                y = findMatchingLoopVar(c, IteratorType.Y);
                if(y == null) {
                    throw new Exception("No loop variable named: y");
                }
            }

            Function function = functions[c.GetFunctionName()];
            if (c.GetX() == -1 && c.GetY() == -1) {
                function.Execute(tilemapGenerator, this, c.GetPosition().x + x.GetValue(), c.GetPosition().y + y.GetValue());  
            } else if (c.GetX() == -1) {
                function.Execute(tilemapGenerator, this, c.GetPosition().x + x.GetValue(), c.GetPosition().y + c.GetY());  
            } else if (c.GetY() == -1) {
                function.Execute(tilemapGenerator, this, c.GetPosition().x + c.GetX(), c.GetPosition().y + y.GetValue());  
            } else {
                function.Execute(tilemapGenerator, this, c.GetPosition().x + c.GetX(), c.GetPosition().y + c.GetY());
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
            LoopVariable x = null, y = null;
            if(f.GetX() == -1) {
                x = findMatchingLoopVar(f, IteratorType.X);
                if(x == null) {
                    throw new Exception("No loop variable named: x");
                }
            }
            if(f.GetY() == -1) {
                y = findMatchingLoopVar(f, IteratorType.Y);
                if(y == null) {
                    throw new Exception("No loop variable named: y");
                }
            }
            
            if (x != null && y != null) {
                tilemapGenerator.Fill(x.GetValue()+f.GetPosition().x, y.GetValue()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), color32);  
            } else if (x != null) {
                tilemapGenerator.Fill(x.GetValue()+f.GetPosition().x, f.GetY()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), color32); 
            } else if (y != null) {
                tilemapGenerator.Fill(f.GetX()+f.GetPosition().x, y.GetValue()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), color32); 
            } else {
                tilemapGenerator.Fill(f.GetX()+f.GetPosition().x, f.GetY()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), color32);
            }
        }

        public void visit(TilemapGenerator tilemapGenerator, Function f)
        {
            functions.Add(f.GetName(), f);
            f.SetScope(functions.Count); // this works as long as we never remove any functions from the dictionary
        }

        public void visit(TilemapGenerator tilemapGenerator, Loop loop)
        {
            LoopVariable loopVar = loop.GetLoopVariable();
            Loop nestedLoop = loop.TryGetNestedLoop();
            if (nestedLoop != null)
            {
                if(nestedLoop.GetLoopVariable().GetIteratorType() == loopVar.GetIteratorType()) {
                    throw new Exception("Loops can not nest a loop with the same iterator type");
                }
                
                // uncomment this in future if we add more potential loop variables
                // if(nestedLoop.TryGetNestedLoop() != null) {
                //     throw new Exception("Can not have more than 2 nested loops");
                // }
            }
            for (int i = loop.GetFrom(); i <= loop.GetTo(); i += loop.GetStep())
            {
                loopVar.SetValue(i);
                foreach (Statement statement in loop.GetStatements())
                {
                    statement.Accept(tilemapGenerator, this);
                }
            }
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

        private LoopVariable findMatchingLoopVar(Statement statement, IteratorType iteratorType) {
            while(statement != null) {
                if(statement is Loop) {
                    Loop tempLoop = statement as Loop;
                    LoopVariable loopVariable = tempLoop.GetLoopVariable();
                    if(loopVariable.GetIteratorType() == iteratorType) {
                        return loopVariable;
                    }
                }
                statement = statement.Parent;
            }
            return null;
        }
    }
}