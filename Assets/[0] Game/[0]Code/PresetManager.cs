using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PresetManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _presets;

        private void Start()
        {
            if (GameData.IsBadEnd && GameData.IsGoodEnd && !GameData.IsStrangeEnd) 
                _presets[1].SetActive(true);
            else
                _presets[0].SetActive(true);
        }
    }
}