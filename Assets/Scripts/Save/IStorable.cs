﻿using JetBrains.Annotations;
using Save.Data;

namespace Save
{
    public interface IStorable
    {
        public bool IsExists { get; }
        public SaveData Create();
        public void Save(SaveData data);
        [CanBeNull] public SaveData Load();
    }
}