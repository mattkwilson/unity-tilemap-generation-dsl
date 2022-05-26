using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public interface ITilemapDSLVisitor
    {
        void visit(TilemapGenerator tilemapGenerator, Program p);
        void visit(TilemapGenerator tilemapGenerator, Call c);
        void visit(TilemapGenerator tilemapGenerator, Canvas c);
        void visit(TilemapGenerator tilemapGenerator, Color c);
        void visit(TilemapGenerator tilemapGenerator, Fill f);
        void visit(TilemapGenerator tilemapGenerator, Function f);
        void visit(TilemapGenerator tilemapGenerator, Loop l);
        void visit(TilemapGenerator tilemapGenerator, Noise n);
        void visit(TilemapGenerator tilemapGenerator, NoiseMap n);
        void visit(TilemapGenerator tilemapGenerator, Statement n);
    }
}