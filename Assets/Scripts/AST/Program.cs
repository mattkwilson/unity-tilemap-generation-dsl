﻿using System.Collections.Generic;
using System;

namespace Assets.Scripts.AST
{
    public class Program : ASTBase
    {
        private Canvas canvas;
        private Dictionary<string, Variable> variables;
        private List<Statement> statements;
        private List<Function> functions;
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="functions"></param>
        public Program(Canvas canvas, Dictionary<string, Variable> variables, List<Statement> statements, List<Function> functions)
        {
            // TODO: implement constructor
        }

        public void accept(ITilemapDSLVisitor<TilemapGenerator> v)
        {
            v.visit(this);
        }
    }
}