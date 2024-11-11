using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class TestFakeHeroCutscene : MonoBehaviour
    {
        [SerializeField]
        private FakeHeroCutscene_1 _cutscene;

        [SerializeField]
        private FakeHeroCutscene_2 _cutscene2;
        
        private void Start()
        {
            _cutscene.gameObject.SetActive(false);
            Lua.Run("Variable[\"FakeHeroState\"] = 1");
            _cutscene2.StartCutscene();
        }
    }
}