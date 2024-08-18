using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Game
{
    public class PresetManager : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _presets;

        private void Start()
        {
            if (YandexGame.savesData.IsAlternativeWorld)
                _presets[1].SetActive(true);
            else
                _presets[0].SetActive(true);
        }
    }
}