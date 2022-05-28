using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public abstract class Statement : Element
    {
        protected Vector2Int positionOffset;
        
        // set to true in concrete class constructor if x or y are passed as loop variables
        protected bool IsLoopX; 
        protected bool IsLoopY;
        
        // initialize in concrete class constructor
        protected int X;
        protected int Y;
        
        public void SetPositionOffset(int x, int y) {
            positionOffset = new Vector2Int(x, y);
        }

        public Vector2Int GetPositionOffset() {
            return positionOffset;
        }

        public void SetX(int x) 
        {
            if (IsLoopX)
            {
                X = x;
            }
        }
        
        public void SetY(int y) 
        {
            if (IsLoopY)
            {
                Y = y;
            }
        }
        
        public int GetX() {
            return X;
        }
        
        public int GetY() {
            return Y;
        }
    }
}