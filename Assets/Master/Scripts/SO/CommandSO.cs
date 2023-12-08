using System;
using UnityEngine;

namespace Master.Scripts.SO
{
    [Serializable]
    public abstract class CommandSO : ScriptableObject
    {
        public abstract void Setup(Enemy.Enemy enemy);
        public abstract void Execute(Enemy.Enemy enemy);
        public abstract bool IsFinished(Enemy.Enemy enemy);
        public abstract void CleanUp(Enemy.Enemy enemy);
    }
}
