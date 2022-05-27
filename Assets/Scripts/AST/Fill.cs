using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Fill : Statement
    {
        private int width, height;
        private Color color;

        public Fill(int x, int y, int width, int height, Color color) {
            this.width = width;
            this.height = height;
            this.color = color;
            SetPositionOffset(x, y);
        }

        public int getWidth() {
            return width;
        }

        public int getHeight() {
            return height;
        }

        public Color getColor() {
            return color;
        }


        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
    }
}