using UnityEngine;

namespace Sounds
{
    public class SoundsEffects : MonoBehaviour
    {
        [SerializeField] public AudioClip CompleteTetromino;
        [SerializeField] public AudioClip DeleteLine;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayComplete()
        {
            Play(CompleteTetromino);
        }
        
        public void PlayDeleteLine()
        {
            Play(DeleteLine);
        }

        private void Play(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}