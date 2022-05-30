using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Fill : Statement
    {
        private int x, y, width, height;
        private string texture;

        public Fill(int x, int y, int width, int height, string texture) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.texture = texture;
        }

        public int GetX() {
            return x;
        }

        public int GetY() {
            return y;
        }

        public int GetWidth() {
            return width;
        }

        public int GetHeight() {
            return height;
        }

        public string GetTexture() {
            return texture;
        }


        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v){
            v.visit(tilemapGenerator, this);
        }
    }
}