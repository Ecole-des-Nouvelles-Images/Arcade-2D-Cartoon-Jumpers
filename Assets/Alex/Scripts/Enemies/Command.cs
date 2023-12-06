using System;
using UnityEngine;

namespace Alex.Scripts.Enemies
{
    [Serializable]
    public abstract class Command : ScriptableObject
    {
        public abstract void Execute(Enemy enemy);
        public abstract bool IsFinished(Enemy enemy);
    }
}