using System;
using System.Collections.Generic;

namespace Assets.Scripts.AST
{
    public class If : Statement
    {
        private readonly string _noiseVariable;
        private int _noiseValue;
        private readonly string _condition;
        private readonly int _number;
        private readonly List<Statement> _statements;

        public If(string noise, string condition, int number, List<Statement> statements)
        {
            _noiseVariable = noise;
            _condition = condition;
            _number = number;
            _statements = statements;
        }

        public void SetNoiseValue(int value)
        {
            _noiseValue = value;
        }

        public bool EvaluateCondition()
        {
            switch (_condition)
            {
                case ">":
                    return _noiseValue > _number;
                case ">=":
                    return _noiseValue >= _number;
                case "<":
                    return _noiseValue < _number;
                case "<=":
                    return _noiseValue <= _number;
                case "==":
                    return _noiseValue == _number;
                case "!=":
                    return _noiseValue != _number;
                default:
                    throw new Exception("Unexpected comparator in If");
            }
        }

        public string GetNoiseVariable()
        {
            return _noiseVariable;
        }

        public List<Statement> GetStatements()
        {
            return _statements;
        }
        
        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v)
        {
            v.visit(tilemapGenerator, this);
        }
    }
}