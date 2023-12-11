using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

using Master.Scripts.Internal;

namespace Charlie.Scripts.Managers
{
    public class SceneLoader: SingletonMonoBehaviour<SceneLoader>
    {
        [SerializeField] private bool _reloadSceneOnOverflow;
        
        [SerializeField] private Animator _transitionController;
        [SerializeField] private float _additionnalDuration = 0.5f; 

        private AnimationClip[] _runtimeClips;

        private float _transitionDuration;
        private static int _currentSceneBuildIndex;
        private static int _lastSceneBuildIndex;

        private static readonly int Start = Animator.StringToHash("Start");

        protected override void Awake()
        {
            base.Awake();
            _transitionController.gameObject.SetActive(true);
            
            _currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            _lastSceneBuildIndex = SceneManager.sceneCountInBuildSettings - 1;
            _runtimeClips = _transitionController.runtimeAnimatorController.animationClips;
            
            _transitionDuration = GetTransitionEndDuration();
        }
        
        // Scene Switch methods and Coroutine //

        public void LoadPreviousScene()
        {
            StartCoroutine(LoadSceneCoroutine(_currentSceneBuildIndex - 1));
        }
        
        public void LoadNextScene()
        {
            StartCoroutine(LoadSceneCoroutine(_currentSceneBuildIndex + 1));
        }

        public void LoadScene(int buildIndex)
        {
            StartCoroutine(LoadSceneCoroutine(buildIndex));
        }
        
        private IEnumerator LoadSceneCoroutine(int buildIndex)
        {
            if (buildIndex < 0)
            {
                if (_reloadSceneOnOverflow) buildIndex = 0;
                else { StopAllCoroutines(); }
            }
            else if (buildIndex > _lastSceneBuildIndex)
            {
                if (_reloadSceneOnOverflow) buildIndex = _lastSceneBuildIndex;
                else { StopAllCoroutines(); }
            }
            
            _transitionController.SetTrigger(Start);
            
            yield return new WaitForSeconds(_transitionDuration);

            SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
        }
        
        // Utils //
        
        private DropdownList<Animator> GetTransitionTypes()
        {
            DropdownList<Animator> list = new();
            
            foreach (Transform transition in transform) {
                list.Add(transition.name, transition.GetComponent<Animator>());
            }

            return list;
        }
        
        private float GetTransitionEndDuration()
        {
            string expectedClipName = _transitionController.name + "_End";
            
            foreach (AnimationClip clip in _runtimeClips)
            {
                if (clip.name != expectedClipName)
                    continue;
                
                return clip.length;
            }

            throw new Exception("Fuck");
        }
    }
}
