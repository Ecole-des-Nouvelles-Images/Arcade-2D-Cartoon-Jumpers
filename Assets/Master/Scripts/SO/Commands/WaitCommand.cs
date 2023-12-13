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
            enemy.Memory[(this, "_timeOut")] = Time.time + _waitingTime;
        }

        public override void Execute(EnemyComponent enemy)
        {
            Debug.Log($"{enemy.name} is waiting");
        }

        public override bool IsFinished(EnemyComponent enemy)
        {
            float _timeOut = (float)enemy.Memory[(this, "_timeOut")];
            return Time.time >= _timeOut;
        }

        public override void CleanUp(EnemyComponent enemy)
        {
            enemy.Memory.Remove((this, "_timeOut"));
        }
    }
}