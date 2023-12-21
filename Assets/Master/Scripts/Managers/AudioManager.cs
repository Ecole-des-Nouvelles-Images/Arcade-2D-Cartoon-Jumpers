using System.Collections;
using Master.Scripts.Internal;
using UnityEngine;

namespace Master.Scripts.Managers
{
    public class AudioManager: SingletonMonoBehaviour<AudioManager>
    {
        [Header("References")]
        [SerializeField] private AudioSource _musicSource;
        
        [Header("Settings")]
        [SerializeField] [Range(0, 1)] private float _maximumVolume;

        private AudioClip _pendingAudioClip;

        protected override void Awake()
        {
            base.Awake();
            _musicSource.volume = _maximumVolume;
        }

        public void SetAudioSourceVolume(float userMultiplier)
        {
            userMultiplier /= 100;
            _musicSource.volume = _maximumVolume * userMultiplier;
        }

        public void PlayClip(AudioClip newClip)
        {
            _musicSource.clip = newClip;
            _musicSource.Play();
        }

        public void PauseAudio()
        {
            _musicSource.Pause();
        }

        public void ResumeAudio()
        {
            _musicSource.Play();
        }
        
        // Coroutines Launchers //

        public void ChangeAudioClip(AudioClip newClip)
        {
            if (newClip == null)
                return;

            _pendingAudioClip = newClip;
            StartCoroutine(SwitchClipCrossfadeCoroutine());
        }

        public void ChangeAudioClip(string pathToAudioClip)
        {
            _pendingAudioClip = Resources.Load<AudioClip>(pathToAudioClip);
            StartCoroutine(SwitchClipCrossfadeCoroutine());
        }
        
        // Coroutines //

        public IEnumerator ClipCrossfadeCoroutine(float targetVolume)
        {
            const float fadeTime = 1f;
            float t = 0f;
            float initialVolume = _musicSource.volume;

            while (t < 1)
            {
                t += Time.deltaTime/fadeTime;
                _musicSource.volume = Mathf.Lerp(initialVolume, targetVolume, t);
                yield return null;
            }
        }

        private IEnumerator SwitchClipCrossfadeCoroutine()
        {
            const float fadeTime = 1f;
            float t = 0f;
            float initialVolume = _musicSource.volume;

            while (t < 1)
            {
                t += Time.unscaledDeltaTime / fadeTime;
                _musicSource.volume = Mathf.Lerp(initialVolume, 0.00f, t);
                yield return null;
            }

            _musicSource.Stop();
            _musicSource.clip = _pendingAudioClip;
            _musicSource.Play();
            t = 0f;

            while (t < 1)
            {
                t += Time.unscaledDeltaTime / fadeTime;
                _musicSource.volume = Mathf.Lerp(0.00f, initialVolume, t);
                yield return null;
            }
            
            _pendingAudioClip = null;
        }
    }
}
