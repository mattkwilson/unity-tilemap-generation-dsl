using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public abstract class Statement : Element
    {
        protected Statement parent;
        public Statement Parent { get {return parent;}}
        protected Vector2Int position;

        public void SetParent(Statement parent) {
            this.parent = parent;
        }
        
        public void SetPosition(int x, int y) {
            position = new Vector2Int(x, y);
        }

        public Vector2Int GetPosition() {
            return position;
        }
        
    }
}