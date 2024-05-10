using System.Collections.Generic;
using Engine;
using Save.Data.SaveDataElements;
using UnityEngine;

namespace View.GUI.Grid
{
    public class TotalTableGrid : MonoBehaviour
    {
        [SerializeField] private Logic logic;
        [SerializeField] private TotalTableHorizontalGroup positions;
        [SerializeField] private TotalTableHorizontalGroup results;
        private List<string> _positionsList;
        private List<string> _resultsList;
        private bool _isInit;
        private TotalPointList Save => Saver.SaveData.TotalPoints;

        public void GenerateTotalTable()
        {
            var position = Save.GetSupposedPositionByValue(logic.TotalPoints);
            Save.SetValueWithShift(logic.TotalPoints);
            Saver.Instance.Save();
            
            foreach (var totalPoints in Save.List)
            {
                var selector = position == totalPoints.Position;
                positions.CreateElementList(totalPoints.Position, selector);
                results.CreateElementList(totalPoints.Value, selector);
            }

            if (position == 0)
            {
                positions.CreateElementList(position, true);
                results.CreateElementList(logic.TotalPoints, true);
            }
        }
    }
}