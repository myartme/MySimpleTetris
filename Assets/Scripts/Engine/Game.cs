using System;
using Spawn;
using UnityEngine;
using UnityEngine.Serialization;

namespace Engine
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Spawner spawner;
        private GameGrid _gameGrid;
        private float mTimer = 0f;
        public float timeToStep = 2f;
        
        private void Awake()
        {
            _gameGrid = new GameGrid(spawner);
        }

        private void Update()
        {
            if (!_gameGrid.IssetTetromino) return;
            
            /*mTimer += Time.deltaTime;
            if(mTimer > timeToStep)
            {
                mTimer = 0;
                _gameGrid.Step();
            }*/
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _gameGrid.AnticlockwiseAngleRotation();
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                _gameGrid.ClockwiseAngleRotation();
            }
            
			if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _gameGrid.StepLeft();
            }
            
			if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _gameGrid.StepRight();
            }
            
			if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _gameGrid.StepDown();
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _gameGrid.StepUp();
            }
        }

        public void OnDeleteLines(Action<int> action)
        {
            _gameGrid.OnDeleteLines += action;
        }
    }
}