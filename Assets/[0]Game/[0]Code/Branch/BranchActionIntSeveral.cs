using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class BranchActionIntSeveral : MonoBehaviour
    {
        [SerializeField]
        private SaveKeyInt _saveKey;
        
        [SerializeField]
        private List<UnityEvent> _events = new();
        
        public void Use()
        {
            var index = GameData.Saver.LoadKey(_saveKey);
            
            if (index > _events.Count)
                throw new Exception("Нужно добавить евент");
            
            _events[index].Invoke();
        }
    }
}