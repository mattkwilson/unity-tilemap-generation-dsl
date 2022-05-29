using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public enum IteratorType
    {
        X,
        Y
    }
    
    public class Loop : Statement
    {
        private LoopVariable loopVariable;
        private readonly int _from;
        private readonly int _to;
        private readonly int _step;
        private readonly List<Statement> _statements;

        public Loop(IteratorType iterator, int from, int to, int step, List<Statement> statements)
        {
            loopVariable = new LoopVariable(iterator, from);
            _from = from;
            _to = to;
            _step = step;
            _statements = statements;
            
            foreach(Statement statement in statements) {
                statement.SetParent(this);
            }
        }

        public Loop TryGetNestedLoop() {
            Statement tempParent = parent;
            while(tempParent != null) {
                if(tempParent is Loop) {
                    return tempParent as Loop;
                }
                tempParent = tempParent.Parent;
            }
            return null;
        }

        public LoopVariable GetLoopVariable()
        {
            return loopVariable;
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