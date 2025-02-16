using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [Serializable]
    public class AudioPlayer
    {
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private AudioSource _audioSource;
        
        [Space]
        [SerializeField] private float _volume = 1;
        
        [Tooltip("x - min pitch; y = max pitch")]
        [SerializeField] private float _minPitch = 0.95f;
        [SerializeField] private float _maxPitch = 1.05f;

        public void PlayShot()
        {
            if (!_audioSource || _audioClips.Length == 0) return;
            
            SetupAudioSource();
            _audioSource.PlayOneShot(GetRandomClip());
        }

        private void SetupAudioSource()
        {
            var pitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.pitch = pitch;
            
            _audioSource.volume = _volume;
        }

        private AudioClip GetRandomClip()
        {
            return _audioClips[Random.Range(0, _audioClips.Length)];
        }
    }
}