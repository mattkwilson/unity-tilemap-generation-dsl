using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        private static bool _loopingX = false;
        private static bool _loopingY = false;
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

        public static void LockIterator(Iterator i)
        {
            bool loopingI = (i == Iterator.X) ? _loopingX : _loopingY;
            if (loopingI)
            {
                throw new Exception("Illegal Loop Nesting");
            }

            if (i == Iterator.X)
            {
                _loopingX = true;
            }
            else
            {
                _loopingY = true;
            }
        }
        
        public static void FreeIterator(Iterator i)
        {
            if (i == Iterator.X)
            {
                _loopingX = false;
            }
            else
            {
                _loopingY = false;
            }
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