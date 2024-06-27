using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace Game
{
    public class GideButton : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private TMP_Text _label;

        [SerializeField]
        private Sprite _heartSprite;
        
        private GideConfig _data;
        private GideManager _manager;

        public GideConfig GetData => _data;

        public void Init(GideManager manager, GideConfig gideData)
        {   
            GetComponent<Button>().onClick.AddListener(() => _manager.Select(this));
            _manager = manager;
            _data = gideData;
            
            _icon.sprite = _data.Icon;
            _label.text = _data.Name.GetLocalizedString();
        }
        
        public void Select()
        {
            _icon.sprite = _heartSprite;
            _label.color = Color.yellow;
        }
        
        public void UnSelect()
        {
            _icon.sprite = _data.Icon;
            _label.color = Color.white;
        }
    }
}