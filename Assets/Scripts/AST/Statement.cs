using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public abstract class Statement : Element
    {
        protected Vector2Int positionOffset;

        public void SetPositionOffset(int x, int y) {
            positionOffset = new Vector2Int(x, y);
        }

        public int getX() {
            return positionOffset.x;
        }

        public int getY() {
            return positionOffset.y;
        }
    }
}