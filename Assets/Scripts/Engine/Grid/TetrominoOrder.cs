using System;
using System.Linq;
using GameFigures.Shape;
using Service;
using UnityEngine;

namespace Engine.Grid
{
    public class TetrominoOrder : MonoBehaviour
    {
        [SerializeField] private Transform previewTransform;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private ColorTheme colorTheme;
        public static event Action OnGetTetromino;
        public event Action<Tetromino> OnChangeStatus;
        public Tetromino CurrentTetromino => _current;

        private TetrominoGenerator _generator;
        private Tetromino _current;
        private Tetromino _previewer;

        private void Awake()
        {
            _generator = new TetrominoGenerator();
        }

        private void Start()
        {
            SetPreview(CreateTetromino());
        }

        private void ChangeStatus(Tetromino tetromino)
        {
            if (_current?.Status == ObjectStatus.Completed)
            {
                _current.OnChangeStatus -= OnChangeStatus;
                _current.OnChangeStatus -= ChangeStatus;
                _current = null;
            }
            
            if (_current is null)
            {
                SetCurrent(_previewer);
                if (_current is not null)
                {
                    SetPreview(CreateTetromino());
                }
            }
        }
        
        private void SetPreview(Tetromino tetromino)
        {
            tetromino.OnChangeStatus += ChangeStatus;
            _previewer = tetromino;
            _previewer.Position = previewTransform.position - tetromino.Bounds.center;
            _previewer.SetAsPreview();
        }
        
        private void SetCurrent(Tetromino tetromino)
        {
            tetromino.Position = spawnTransform.position;
            var spawnIsBlocked = tetromino.Children
                .Select(tetrominoChild => Vector3Int.RoundToInt(tetrominoChild.Position))
                .Any(pos => !GameGrid.IsEmptyGridPosition(pos.x, pos.y));

            if (spawnIsBlocked)
            {
                Game.IsGameOver = true;
                return;
            }
            
            tetromino.OnChangeStatus += OnChangeStatus;
            _current = tetromino;
            _current.SetAsReady();
            OnGetTetromino?.Invoke();
        }
        
        private Tetromino CreateTetromino()
        {
            return new Tetromino(colorTheme.CurrentBlock, _generator.Next());
        }
    }
}