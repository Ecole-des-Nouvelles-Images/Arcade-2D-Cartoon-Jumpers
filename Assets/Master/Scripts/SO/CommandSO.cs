using System;
using UnityEngine;

using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO
{
    [Serializable]
    public abstract class CommandSO : ScriptableObject
    {
        
        public abstract void Setup(EnemyComponent enemyComponent);
        public abstract void Execute(EnemyComponent enemyComponent);
        public abstract bool IsFinished(EnemyComponent enemyComponent);
        public abstract void CleanUp(EnemyComponent enemyComponent);
    }
}
