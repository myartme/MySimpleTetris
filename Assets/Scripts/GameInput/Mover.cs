using System;
using Engine;
using GameFigures.Shape;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float verticalSpeed = 1f;
        [SerializeField] private float horizontalSpeed = 1f;

        public InputController GameInput { get; private set; }
        private InputProvider _inputProvider;
        
        private float VerticalSpeedTimer => 1 / verticalSpeed;
        private float HorizontalSpeedTimer => 1 / horizontalSpeed;

        private Game _game;
        private GameGrid _gameGrid;
        private Logic _logic;
        
        private GameGrid.GridAction _currentAction;
        private Timer _moveTimer, _accelerateTimer;
        private KeyCode _instantlyMovementKeyCode;
        private bool _isAccelerating;
        private float _timeToNextStep;

        private Action<InputAction.CallbackContext> _movementStartedHandler, 
            _movementPerformedHandler,
            _movementCanceledHandler,
            _rotationStartedHandler;

        private void Awake()
        {
            GameInput = new InputController();
            _inputProvider = new GameplayInputProvider(GameInput);
            GameInput.Enable();
            _game = GetComponent<Game>();
            _gameGrid = GetComponent<GameGrid>();
            _logic = GetComponent<Logic>();
            _timeToNextStep = _logic.timeToNextStep;
            _moveTimer = new Timer(_timeToNextStep);
            _accelerateTimer = new Timer(_timeToNextStep);

            _movementStartedHandler = ctx 
                => OnMovementStarted(_inputProvider.GetMovementAction(ctx));
            _rotationStartedHandler = ctx 
                => OnRotationStarted(_inputProvider.GetRotationAction(ctx));
            _movementPerformedHandler = ctx => OnMovementPerformed();
            _movementCanceledHandler = ctx => OnMovementCanceled();
        }

        private void Update()
        {
            if (!_gameGrid.IssetTetromino || Time.timeScale == 0 || Options.IsGameOver || !GameInput.Gameplay.enabled) return;
            
            UpdateTimeToNextStep();
            _moveTimer.Update();
            _accelerateTimer.Update();
            
            if (_currentAction != null && _isAccelerating && _accelerateTimer.IsEnding())
            {
                _currentAction();
                _accelerateTimer.Reset();
            }
            
            if (_moveTimer.IsEnding())
            {
                _gameGrid.StepDown();
                _moveTimer.Reset();
            }
        }
        
        private void CheckTetrominoStatus(Tetromino tetromino)
        {
            if (tetromino.Status == ObjectStatus.Active)
            {
                _currentAction = null;
                _isAccelerating = false;
                GameInput.Enable();
            }
            
            if (tetromino.Status == ObjectStatus.MakeComplete)
            {
                GameInput.Disable();
            }
        }

        private void OnMovementStarted(InputProvider.MovementType type)
        {
            switch (type)
            {
                case InputProvider.MovementType.Left:
                    StartAction(_gameGrid.StepLeft, HorizontalSpeedTimer);
                    break;
                case InputProvider.MovementType.Right:
                    StartAction(_gameGrid.StepRight, HorizontalSpeedTimer);
                    break;
                case InputProvider.MovementType.Down:
                    StartAction(_gameGrid.StepDown, VerticalSpeedTimer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnRotationStarted(InputProvider.RotationType type)
        {
            switch (type)
            {
                case InputProvider.RotationType.Anticlockwise:
                    _gameGrid.AnticlockwiseAngleRotation();
                    break;
                case InputProvider.RotationType.Clockwise:
                    _gameGrid.ClockwiseAngleRotation();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void StartAction(GameGrid.GridAction gridAction, float speedTimer)
        {
            _isAccelerating = false;
            _accelerateTimer.Ending = speedTimer;
            _accelerateTimer.Reset();
            _currentAction = gridAction;
            gridAction();
        }
        
        private void OnMovementPerformed()
        {
            _isAccelerating = true;
        }
        
        private void OnMovementCanceled()
        {
            _isAccelerating = false;
        }

        private void UpdateTimeToNextStep()
        {
            if (_timeToNextStep > _logic.timeToNextStep)
            {
                _timeToNextStep = _logic.timeToNextStep;
                _moveTimer = new Timer(_timeToNextStep);
            }
        }
        
        private void OnEnable()
        {
            _game.spawner.OnCurrentTetromino += CheckTetrominoStatus;
            foreach (var movement in _inputProvider.MovementInputAction)
            {
                movement.started += _movementStartedHandler;
                movement.performed += _movementPerformedHandler;
                movement.canceled += _movementCanceledHandler;
            }
            foreach (var rotation in _inputProvider.RotationInputAction)
            {
                rotation.started += _rotationStartedHandler;
            }
        }


        private void OnDisable()
        {
            _game.spawner.OnCurrentTetromino -= CheckTetrominoStatus;
            foreach (var movement in _inputProvider.MovementInputAction)
            {
                movement.started -= _movementStartedHandler;
                movement.performed -= _movementPerformedHandler;
                movement.canceled -= _movementCanceledHandler;
            }
            foreach (var rotation in _inputProvider.RotationInputAction)
            {
                rotation.started -= _rotationStartedHandler;
            }
        }
    }
}