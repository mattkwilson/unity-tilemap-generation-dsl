using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class Canvas : ASTBase {

        private int width;

        private int height;

        public int getWidth() {
          return width;
        }

		public Canvas(int w, int h) {
			width = w;
			height = h;
		}

        public int getHeight() {
          return height;
        }

        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v) {
          v.visit(tilemapGenerator, this);
        } 
    }
}