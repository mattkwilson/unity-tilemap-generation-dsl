using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    public class LoopVariable
    {
        private IteratorType iteratorType;
        private int value;

        public LoopVariable(IteratorType type, int value) {
            this.iteratorType = type;
            this.value = value;
        }

        public IteratorType GetIteratorType() {
            return iteratorType;
        }

        public int GetValue() {
            return value;
        }

        public void SetValue(int value) {
            this.value = value;
        }
    }
}