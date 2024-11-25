using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class BavCylinderContent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _table;

        [SerializeField]
        private GameObject _platformsContainer;

        [SerializeField]
        private GameObject _wall;
        
        private void Start()
        {
            if (Lua.IsTrue("Variable[\"IsCylinder\"] == true"))
            {
                _table.SetActive(true);
                _platformsContainer.SetActive(true);
                _wall.SetActive(false);
            }
        }
    }
}