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
            if (YandexGame.savesData.IsBadEnd && YandexGame.savesData.IsGoodEnd && YandexGame.savesData.IsStrangeEnd 
                && YandexGame.savesData.IsPalesosEnd && YandexGame.savesData.NumberGame % 2 == 1) 
                _presets[1].SetActive(true);
            else
                _presets[0].SetActive(true);
        }
    }
}