using System;
using System.Collections;
using System.Collections.Generic;
using Charlie.Scripts.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Charlie.Scripts.Managers
{
    public class SceneLoader: SingletonMonoBehavior<SceneLoader>
    {
        [SerializeField] private List<Transform> _transitionPanels = new ();
        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionDuration;
        
        private Dictionary<string, Transform> _transitionsList;

        private static readonly int Start = Animator.StringToHash("Start");

        protected override void Awake()
        {
            base.Awake();
            
            _transitionsList = new Dictionary<string, Transform>();
            
            foreach (Transform panel in _transitionPanels)
                _transitionsList.Add(panel.name, panel);
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadSceneCoroutine(sceneIndex));
        }
        
        private IEnumerator LoadSceneCoroutine(int buildIndex)
        {
            _transition.SetTrigger(Start);

            yield return new WaitForSeconds(_transitionDuration);

            SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
        }
    }
}
