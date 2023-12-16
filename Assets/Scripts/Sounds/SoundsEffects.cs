using UnityEngine;

namespace Sounds
{
    public class SoundsEffects : MonoBehaviour
    {
        [SerializeField] public AudioClip completeTetromino;
        [SerializeField] public AudioClip deleteLine;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayComplete()
        {
            Play(completeTetromino);
        }
        
        public void PlayDeleteLine()
        {
            Play(deleteLine);
        }

        private void Play(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}