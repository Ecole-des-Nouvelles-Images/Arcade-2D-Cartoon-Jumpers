using UnityEngine;
using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "Command", menuName = "New waiting Command")]
    public class WaitCommand : CommandSO
    {
        [SerializeField] private float _waitingTime;

        public override void Setup(EnemyComponent enemyComponent)
        {
            enemyComponent.Memory[(this, "_timeOut")] = Time.time + _waitingTime;
        }

        public override void Execute(EnemyComponent enemyComponent)
        {
            // animation d Idle 
        }

        public override bool IsFinished(EnemyComponent enemyComponent)
        {
            float _timeOut = (float)enemyComponent.Memory[(this, "_timeOut")];
            return Time.time >= _timeOut;
        }

        public override void CleanUp(EnemyComponent enemyComponent)
        {
            enemyComponent.Memory.Remove((this, "_timeOut"));
        }
    }
}
