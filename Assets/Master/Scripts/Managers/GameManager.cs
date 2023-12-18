using System;
using UnityEngine;

using Master.Scripts.Internal;

namespace Master.Scripts.Managers
{
    public class GameManager: SingletonMonoBehaviour<GameManager>
    {
        public static Action<bool> OnPause;
        
        public static bool IsPaused { get; set; }
        
        public void SetPause()
        {
            IsPaused = IsPaused == false;
            Time.timeScale = IsPaused ? 0 : 1;
            OnPause.Invoke(IsPaused);
        }
    }
}
