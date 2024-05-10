using System;
using Engine;
using Engine.Grid;
using GameFigures.Shape;
using Save.Data.SaveDataElements;
using Service;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public class Mover : MonoBehaviour
    {
        [SerializeField][Range((float)0.01, 1)] private float verticalSpeed = 1f;
        [SerializeField][Range((float)0.01, 1)] private float horizontalSpeed = 1f;
        [SerializeField] private Logic logic;
        [SerializeField] private GameGrid gameGrid;
        [SerializeField] private TetrominoOrder tetrominoOrder;

        public float HorizontalSpeed
        {
            get => Save.HorizontalMoveSpeed;
            private set => horizontalSpeed = value;
        }
        public float VerticalSpeed
        {
            get => Save.VerticalMoveSpeed;
            private set => verticalSpeed = value;
        }
        
        private InputController GameInput { get; set; }
        private InputProvider _inputProvider;
        
        private float VerticalSpeedTimer => (float)0.01 / VerticalSpeed;
        private float HorizontalSpeedTimer => (float)0.01 / HorizontalSpeed;
        
        private GameGrid.GridAction _currentAction;
        private Timer _autoMoveTimer, _accelerateTimer;
        private bool _isAccelerating;
        private bool _isBlockedToMove;
        private float _timeToNextStep;

        private Action<InputAction.CallbackContext> _movementStartedHandler, 
            _movementPerformedHandler,
            _movementCanceledHandler,
            _rotationStartedHandler;
        
        private ControlOptions Save => Saver.SaveData.Control;

        private void Awake()
        {
            GameInput = new InputController();
            _inputProvider = new GameplayInputProvider(GameInput);
            _movementStartedHandler = ctx 
                => OnMovementStarted(_inputProvider.GetMovementAction(ctx));
            _rotationStartedHandler = ctx 
                => OnRotationStarted(_inputProvider.GetRotationAction(ctx));
            _movementPerformedHandler = ctx 
                => OnMovementPerformed();
            _movementCanceledHandler = ctx 
                => OnMovementCanceled(_inputProvider.GetMovementAction(ctx));
        }

        private void Start()
        {
            GameInput.Enable();
            LoadSaveData();
            _timeToNextStep = logic.TimeToNextStep;
            _autoMoveTimer = new Timer(_timeToNextStep);
            _accelerateTimer = new Timer(_timeToNextStep);
        }

        private void Update()
        {
            if (Game.IsGameOver) OnDisable();
            
            if (_isBlockedToMove || Time.timeScale == 0) return;
            
            UpdateTimeToNextStep();
            
            _accelerateTimer.Update();
            if (_currentAction != null && _isAccelerating && _accelerateTimer.IsEnding())
            {
                _currentAction();
                _accelerateTimer.Reset();
            }
            
            _autoMoveTimer.Update();
            if (_autoMoveTimer.IsEnding())
            {
                gameGrid.StepDown();
                _autoMoveTimer.Reset();
            }
        }
        
        private void CheckTetrominoStatus(Tetromino tetromino)
        {
            if (tetromino.Status == ObjectStatus.Active)
            {
                _isBlockedToMove = false;
            }
            
            if (tetromino.Status == ObjectStatus.MakeComplete)
            {
                _isBlockedToMove = true;
            }
            
            if (tetromino.Status == ObjectStatus.Completed)
            {
                _currentAction = null;
                _isAccelerating = false;
            }
        }

        private void OnMovementStarted(InputProvider.MovementType type)
        {
            var action = GetMovementActionByType(type);
            var speed = type == InputProvider.MovementType.Down 
                ? VerticalSpeedTimer 
                : HorizontalSpeedTimer;
            
            StartAction(action, speed);
        }
        
        private void OnRotationStarted(InputProvider.RotationType type)
        {
            switch (type)
            {
                case InputProvider.RotationType.Anticlockwise:
                    gameGrid.AnticlockwiseAngleRotation();
                    break;
                case InputProvider.RotationType.Clockwise:
                    gameGrid.ClockwiseAngleRotation();
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
        
        private void OnMovementCanceled(InputProvider.MovementType type)
        {
            if (_currentAction == GetMovementActionByType(type))
            {
                _currentAction = null;
                _isAccelerating = false;
            }
        }
        
        private void UpdateTimeToNextStep()
        {
            if (_timeToNextStep > logic.TimeToNextStep)
            {
                _timeToNextStep = logic.TimeToNextStep;
                _autoMoveTimer = new Timer(_timeToNextStep);
            }
        }
        
        private GameGrid.GridAction GetMovementActionByType(InputProvider.MovementType type)
        {
            return type switch
            {
                InputProvider.MovementType.Left => gameGrid.StepLeft,
                InputProvider.MovementType.Right => gameGrid.StepRight,
                InputProvider.MovementType.Down => gameGrid.StepDown,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void LoadSaveData()
        {
            HorizontalSpeed = Save.HorizontalMoveSpeed;
            VerticalSpeed = Save.VerticalMoveSpeed;
        }
        
        public void ToggleHorizontalMoveSpeed(float value)
        {
            Save.HorizontalMoveSpeed = value;
        }
        
        public void ToggleVerticalMoveSpeed(float value)
        {
            Save.VerticalMoveSpeed = value;
        }
        
        private void OnEnable()
        {
            tetrominoOrder.OnChangeStatus += CheckTetrominoStatus;
            Saver.OnLoadData += LoadSaveData;
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
            tetrominoOrder.OnChangeStatus -= CheckTetrominoStatus;
            Saver.OnLoadData -= LoadSaveData;
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