using System;
using GameFigures.Shape;
using Service;
using UnityEngine;

namespace Engine
{
    public class TetrominoOrder : MonoBehaviour
    {
        [SerializeField] private Sprite blockSprite;
        [SerializeField] private Transform previewTransform;
        [SerializeField] private Transform spawnTransform;

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
            
            if (_current == null)
            {
                SetCurrent(_previewer);
                SetPreview(CreateTetromino());
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
            tetromino.OnChangeStatus += OnChangeStatus;
            _current = tetromino;
            _current.Position = spawnTransform.position;
            _current.SetAsReady();
            OnGetTetromino?.Invoke();
        }
        
        private Tetromino CreateTetromino()
        {
            return new Tetromino(blockSprite, _generator.Next());
        }
    }
}