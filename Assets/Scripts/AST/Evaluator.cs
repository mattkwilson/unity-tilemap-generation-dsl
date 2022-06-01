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
            List<string> args = c.GetArgs();
            List<string> parameters = function.GetParameters();
            foreach(string parameter in parameters) {
                if(args.Count == 0) {
                    throw new Exception("Call to function missing arguments");
                }
                string currArg = args[0];
                Variable argVar;
                if(!variables.TryGetValue(currArg, out argVar)) {
                    throw new Exception("Argument does not reference valid variable");
                }
                variables.Add(parameter, argVar);
                args.RemoveAt(0);
            }
    

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

        public void visit(TilemapGenerator tilemapGenerator, Texture t)
        {
            variables.Add(t.GetName(), t);
        }

        public void visit(TilemapGenerator tilemapGenerator, Fill f)
        {
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
            Variable variable;
            if(variables.TryGetValue(f.GetTexture(), out variable)) {
                if(variable is Texture) {
                    Texture texture = variable as Texture;
                    
                    if (x != null && y != null) {
                        tilemapGenerator.Fill(x.GetValue()+f.GetPosition().x, y.GetValue()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), texture);  
                    } else if (x != null) {
                        tilemapGenerator.Fill(x.GetValue()+f.GetPosition().x, f.GetY()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), texture); 
                    } else if (y != null) {
                        tilemapGenerator.Fill(f.GetX()+f.GetPosition().x, y.GetValue()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), texture); 
                    } else {
                        tilemapGenerator.Fill(f.GetX()+f.GetPosition().x, f.GetY()+f.GetPosition().y, f.GetWidth(), f.GetHeight(), texture);
                    }
                } else {
                    // is color
                    Color color = variable as Color;
                    Byte b = Convert.ToByte(color.GetB());
                    Byte g = Convert.ToByte(color.GetG());
                    Byte r = Convert.ToByte(color.GetR());
                    Color32 color32 = new Color32(r, g, b, 255);

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
            } else {
                throw new Exception("Variable referenced within Fill does not exist");
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
                if(nestedLoop.TryGetNestedLoop() != null) {
                    throw new Exception("Can not have more than 2 nested loops");
                }
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
            string noiseMapName = n.GetNoiseMapName();
            Variable noiseMap = null;
            if(variables.TryGetValue(noiseMapName, out noiseMap) && noiseMap is NoiseMap) {
                LoopVariable x = null, y = null;
                if(n.GetX() == -1) {
                    x = findMatchingLoopVar(n, IteratorType.X);
                    if(x == null) {
                        throw new Exception("No loop variable named: x");
                    }
                }
                if(n.GetY() == -1) {
                    y = findMatchingLoopVar(n, IteratorType.Y);
                    if(y == null) {
                        throw new Exception("No loop variable named: y");
                    }
                }
                if (n.GetX() == -1 && n.GetY() == -1) {
                    n.CalculateNoise(n.GetPosition().x + x.GetValue(), n.GetPosition().y + y.GetValue(), noiseMap as NoiseMap);
                } else if (n.GetX() == -1) {
                    n.CalculateNoise(n.GetPosition().x + x.GetValue(), n.GetPosition().y + n.GetY(), noiseMap as NoiseMap);
                } else if (n.GetY() == -1) {
                    n.CalculateNoise(n.GetPosition().x + n.GetX(), n.GetPosition().y + y.GetValue(), noiseMap as NoiseMap);
                } else {
                    n.CalculateNoise(n.GetPosition().x + n.GetX(), n.GetPosition().y + n.GetY(), noiseMap as NoiseMap);
                }
            } else {
                throw new Exception("Invalid NoiseMap reference in Noise variable");
            }

            if (!variables.TryAdd(n.GetName(), n))
            {
                variables[n.GetName()] = n;
            }
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