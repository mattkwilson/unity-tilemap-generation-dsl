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

		public int getHeight() {
			return height;
		}
		
        public override void Accept(TilemapGenerator tilemapGenerator, ITilemapDSLVisitor v) {
			v.visit(this, tilemapGenerator);
        }
    }
}