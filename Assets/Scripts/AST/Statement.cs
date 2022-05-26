using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public abstract class Statement : ASTBase
    {
        abstract public void accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor<TilemapGenerator> v);
    }
}