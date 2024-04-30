using System.Collections.Generic;
using Engine;
using UnityEngine;

namespace View.GUI.Grid
{
    public class TotalTableGrid : MonoBehaviour
    {
        [SerializeField] private Logic logic;
        [SerializeField] private TotalTableHorizontalGroup positions;
        [SerializeField] private TotalTableHorizontalGroup results;
        private TotalPointsTable _totalPointsTable;
        private List<string> _positionsList;
        private List<string> _resultsList;
        private bool _isInit;
        private void Awake()
        {
            _totalPointsTable = new TotalPointsTable();
        }

        private void Start()
        {
            _totalPointsTable.InitializeSaveData();
        }

        public void GenerateTotalTable()
        {
            var position = _totalPointsTable.Save.GetSupposedPositionByValue(logic.TotalPoints);
            _totalPointsTable.Save.SetValueWithShift(logic.TotalPoints);
            _totalPointsTable.StoreSaveData();
            
            foreach (var totalPoints in _totalPointsTable.Save.list)
            {
                var selector = position == totalPoints.position;
                positions.CreateElementList(totalPoints.position, selector);
                results.CreateElementList(totalPoints.value, selector);
            }

            if (position == 0)
            {
                positions.CreateElementList(position, true);
                results.CreateElementList(logic.TotalPoints, true);
            }
        }
    }
}