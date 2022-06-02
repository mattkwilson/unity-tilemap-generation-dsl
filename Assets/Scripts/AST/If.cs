using System;
using System.Collections.Generic;

namespace Assets.Scripts.AST
{
    public class If : Statement
    {
        private readonly string argumentVariable;
        private int argumentValue;
        private readonly string _condition;
        private readonly int _number;
        private readonly List<Statement> _statements;

        public If(string arg, string condition, int number, List<Statement> statements)
        {
            argumentVariable = arg;
            _condition = condition;
            _number = number;
            _statements = statements;

            foreach(Statement statement in statements) {
                statement.SetParent(this);
            }
        }

        public void SetArgValue(int value)
        {
            argumentValue = value;
        }

        public bool EvaluateCondition()
        {
            switch (_condition)
            {
                case ">":
                    return argumentValue > _number;
                case ">=":
                    return argumentValue >= _number;
                case "<":
                    return argumentValue < _number;
                case "<=":
                    return argumentValue <= _number;
                case "==":
                    return argumentValue == _number;
                case "!=":
                    return argumentValue != _number;
                default:
                    throw new Exception("Unexpected comparator in If");
            }
        }

        public string GetArgument()
        {
            return argumentVariable;
        }

        public List<Statement> GetStatements()
        {
            return _statements;
        }

        public override void SetPosition(int x, int y) {
            foreach (Statement statement in _statements)
            {
                statement.SetPosition(x,y);
            }
        }
        
        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v)
        {
            v.visit(tilemapGenerator, this);
        }
    }
}