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

        [SerializeField]
        private StartBattleTrigger _dragonStartBattle;
        
        private void Start()
        {
            _dragonStartBattle.StartBattle();
            return;
            
            _cutscene.gameObject.SetActive(false);
            Lua.Run("Variable[\"FakeHeroState\"] = 1");
            _cutscene2.StartCutscene();
        }
    }
}