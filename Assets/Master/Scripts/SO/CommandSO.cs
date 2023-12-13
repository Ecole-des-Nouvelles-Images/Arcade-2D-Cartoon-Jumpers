using System;
using UnityEngine;

using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO
{
    [Serializable]
    public abstract class CommandSO : ScriptableObject
    {
        protected EnemyComponent EnemyCtx { get; set; }

        public abstract void Setup(EnemyComponent enemy);
        public abstract void Execute();
        public abstract bool IsFinished();
        public abstract void CleanUp();
    }
}


