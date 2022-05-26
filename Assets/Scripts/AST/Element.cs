using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public abstract class Element : ASTBase
    {   
        // this is only for function scope (i.e. not loop or if)
        public int Scope { get { return scope; } }
        protected int scope;

        public void SetScope(int scope)
        {
            this.scope = scope;
        }
    }
}