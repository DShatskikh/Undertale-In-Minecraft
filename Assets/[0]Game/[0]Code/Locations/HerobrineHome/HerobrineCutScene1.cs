using System;
using UnityEngine;

namespace Game
{
    public class HerobrineCutScene1 : MonoBehaviour
    {
        [SerializeField]
        private Dialog dialog;
        
        [SerializeField] 
        private Replica[] _replicas;

        private void Start()
        {
            dialog.SetReplicas(_replicas);
            GameData.Character.enabled = false;
        }
    }
}