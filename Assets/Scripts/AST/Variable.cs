using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AST
{
    enum VariableType {
        Color,
        Noise,
        NoiseMap
    }

    public abstract class Variable : Statement
    {
        protected readonly string _name;

        public Variable(string name) {
            _name = name;
        }
    }
}