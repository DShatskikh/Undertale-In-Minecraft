using System;
using UnityEngine;

namespace Game
{
    public class HerobrineCutScene1 : MonoBehaviour
    {
        [SerializeField]
        private DialogViewModel dialog;
        
        [SerializeField] 
        private Replica[] _replicas;

        private void Start()
        {
            dialog.Show(_replicas);
            GameData.CharacterController.enabled = false;
        }
    }
}