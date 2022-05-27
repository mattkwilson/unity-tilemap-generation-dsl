using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public enum Iterator
    {
        X,
        Y
    }
    
    public class Loop : Statement
    {
        private readonly Iterator _iterator;
        private readonly int _from;
        private readonly int _to;
        private readonly int _step;
        private readonly List<Statement> _statements;

        public Loop(Iterator iterator, int from, int to, int step, List<Statement> statements)
        {
            _iterator = iterator;
            _from = from;
            _to = to;
            _step = step;
            _statements = statements;
        }

        public Iterator GetIterator()
        {
            return _iterator;
        }

        public int GetFrom()
        {
            return _from;
        }

        public int GetTo()
        {
            return _to;
        }

        public int GetStep()
        {
            return _step;
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