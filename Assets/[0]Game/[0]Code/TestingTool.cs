using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Game
{
    public class TestingTool : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;

        [SerializeField]
        private Transform _container;
        
        [SerializeField]
        private TMP_Dropdown _dropdown;
        
        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private Button _button;
        
        private void Start()
        {
            InitDropdown();
            CreateButton("Полное удаление данных", FullReset);
            CreateButton("Дать торт", AddCake);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.LeftShift)) 
                _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
        }

        private void InitDropdown()
        {
            _button.onClick.AddListener(() => SelectLocation((LocationEnum)_dropdown.value));
            _dropdown.options = new List<TMP_Dropdown.OptionData>();
            
            foreach (var location in Enum.GetValues(typeof(LocationEnum)))
                _dropdown.options.Add(new TMP_Dropdown.OptionData(location.ToString()));
        }

        private void CreateButton(string text, UnityAction action)
        {
            var button = Instantiate(GameData.AssetProvider.TestButton, _container);
            button.Init(text, action);
        }

        [ContextMenu("Полное удаление данных")]
        private void FullReset()
        {
            YandexGame.savesData.FullReset();
            SceneManager.LoadScene(SceneManager.loadedSceneCount);
        }

        [ContextMenu("Дать торт")]
        private void AddCake()
        {
            YandexGame.savesData.IsCake = !YandexGame.savesData.IsCake;
        }

        [ContextMenu("Сменить локацию")]
        private void SelectLocation(LocationEnum location)
        {
            if (int.TryParse(_inputField.text, out int result))
            {
                GameData.LocationsManager.SwitchLocation((int)location, result);
            }
        }
    }
}