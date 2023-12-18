using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyComponent = Master.Scripts.Enemy.Enemy;

namespace Master.Scripts.SO.Commands
{
    [CreateAssetMenu(fileName = "Command", menuName = "New waiting Command")]
    public class WaitCommand : CommandSO
    {
        [SerializeField] private float _waitingTime;

        public override void Setup(EnemyComponent enemy)
        {
            EnemyCtx = enemy;
            EnemyCtx.Memory[(this, "_timeOut")] = Time.time + _waitingTime;
        }

        public override void Execute()
        {
            // animation d Idle 
        }

        public override bool IsFinished()
        {
            float _timeOut = (float)EnemyCtx.Memory[(this, "_timeOut")];
            return Time.time >= _timeOut;
        }

        public override void CleanUp()
        {
            EnemyCtx.Memory.Remove((this, "_timeOut"));
        }
    }
}