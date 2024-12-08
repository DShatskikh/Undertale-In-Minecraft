using System;
using PixelCrushers;

namespace Game
{
    public class IceCreamMachine : Saver, IUseObject
    {
        private Data _saveData = new();
        
        [Serializable]
        private class Data
        {
            
        }
        
        public void Use()
        {
            throw new System.NotImplementedException();
        }

        public override string RecordData()
        {
            return SaveSystem.Serialize(_saveData);
        }

        public override void ApplyData(string s)
        {
            var data = SaveSystem.Deserialize<Data>(s, _saveData);
            _saveData ??= data;
        }
    }
}