using UnityEngine;

namespace Service
{
    public class Timer
    {
        public float CurrentTimer { get; private set; }
        public float Ending { get; set; }

        public Timer(float ending)
        {
            Ending = ending;
        }

        public bool IsEnding() 
            => CurrentTimer >= Ending;

        public void Update() 
            => CurrentTimer += Time.deltaTime;

        public void Reset() 
            => CurrentTimer = 0;
    }
}