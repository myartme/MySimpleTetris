using System.Collections.Generic;
using Engine;
using UnityEngine;

namespace View.GUI.Grid
{
    public class TotalTableGrid : MonoBehaviour
    {
        [SerializeField] private Logic _logic;
        [SerializeField] private TotalTableHorizontalGroup _positions;
        [SerializeField] private TotalTableHorizontalGroup _results;
        private TotalPointsTable _totalPointsTable;
        private List<string> _positionsList;
        private List<string> _resultsList;
        private void Awake()
        {
            _totalPointsTable = new TotalPointsTable();
        }

        private void Start()
        {
            _totalPointsTable.InitializeSaveData();
            var position = _totalPointsTable.Save.GetSupposedPositionByValue(_logic.TotalPoints);
            _totalPointsTable.Save.SetValueWithShift(_logic.TotalPoints);
            _totalPointsTable.StoreSaveData();
            
            foreach (var totalPoints in _totalPointsTable.Save.list)
            {
                var selector = position == totalPoints.position;
                _positions.CreateElementList(totalPoints.position, selector);
                _results.CreateElementList(totalPoints.value, selector);
            }

            if (position == 0)
            {
                _positions.CreateElementList(position, true);
                _results.CreateElementList(_logic.TotalPoints, true);
            }
        }
    }
}