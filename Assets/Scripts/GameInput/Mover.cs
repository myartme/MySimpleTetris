﻿using System;
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

        private InputController GameInput { get; set; }
        private InputProvider _inputProvider;
        
        private float VerticalSpeedTimer => 1 / verticalSpeed;
        private float HorizontalSpeedTimer => 1 / horizontalSpeed;

        private TetrominoOrder _tetrominoOrder;
        private GameGrid _gameGrid;
        private Logic _logic;
        
        private GameGrid.GridAction _currentAction;
        private Timer _autoMoveTimer, _accelerateTimer;
        private bool _isAccelerating;
        private bool _isBlockedToMove;
        private float _timeToNextStep;

        private Action<InputAction.CallbackContext> _movementStartedHandler, 
            _movementPerformedHandler,
            _movementCanceledHandler,
            _rotationStartedHandler;

        private void Awake()
        {
            GameInput = new InputController();
            _inputProvider = new GameplayInputProvider(GameInput);
            _tetrominoOrder = GetComponent<TetrominoOrder>();
            _gameGrid = GetComponent<GameGrid>();
            _logic = GetComponent<Logic>();
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
            _timeToNextStep = _logic.timeToNextStep;
            _autoMoveTimer = new Timer(_timeToNextStep);
            _accelerateTimer = new Timer(_timeToNextStep);
        }

        private void Update()
        {
            if (_isBlockedToMove || Time.timeScale == 0 || Options.IsGameOver) return;

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
                _gameGrid.StepDown();
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
            if (_timeToNextStep > _logic.timeToNextStep)
            {
                _timeToNextStep = _logic.timeToNextStep;
                _autoMoveTimer = new Timer(_timeToNextStep);
            }
        }
        
        private GameGrid.GridAction GetMovementActionByType(InputProvider.MovementType type)
        {
            return type switch
            {
                InputProvider.MovementType.Left => _gameGrid.StepLeft,
                InputProvider.MovementType.Right => _gameGrid.StepRight,
                InputProvider.MovementType.Down => _gameGrid.StepDown,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private void OnEnable()
        {
            _tetrominoOrder.OnChangeStatus += CheckTetrominoStatus;
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
            _tetrominoOrder.OnChangeStatus -= CheckTetrominoStatus;
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