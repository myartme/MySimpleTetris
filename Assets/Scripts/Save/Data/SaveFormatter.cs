using System;
using System.Collections.Generic;
using Save.Data.Format;

using UnityEngine;

namespace Save.Data
{
    [Serializable]
    public class SaveFormatter<T, TS> : AbstractSaveData where T: IFormatSave<TS>, new() where TS : struct
    {
        public T SaveData { get; private set; }
        [SerializeField] private List<TS> data;

        public SaveFormatter()
        {
            SaveData = new T();
            data = SaveData.GetData();
        }
    }

    [Serializable]
    public struct Option
    {
        public string name;
        public float value;
    }

    [Serializable]
    public class TotalPoints
    {
        public int position;
        public int value;
    }
}