using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public abstract class ASTBase
    {
        public abstract void accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v);
    }
}