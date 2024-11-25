using System;
using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace Game
{
    public class FakeHeroCutsceneManager : MonoBehaviour
    {
        [SerializeField]
        private FakeHeroCutscene_1 _cutscene1;
        
        [SerializeField]
        private FakeHeroCutscene_2 _cutscene2;
        
        [SerializeField]
        private FakeHeroCutscene_3 _cutscene3;
        
        [SerializeField]
        private FakeHeroCutscene_4 _cutscene4;
        
        [SerializeField]
        private FakeHeroCutscene_5 _cutscene5;
        
        [SerializeField]
        private FakeHeroCutscene_6 _cutscene6;
        
        private void Start()
        {
            var state = Lua.Run("return Variable[\"FakeHeroState\"]").AsInt;

            switch (state)
            {
                case 0:
                    _cutscene1.gameObject.SetActive(true);
                    break;
                case 1:
                    _cutscene2.StartCutscene();
                    break;
                case 2:
                    _cutscene3.StartCutscene();
                    break;
                case 3:
                    _cutscene4.StartCutscene();
                    break;
                case 4:
                    _cutscene5.StartCutscene();
                    break;
                case 5:
                    _cutscene6.StartCutscene();
                    break;
            }
        }
    }
}