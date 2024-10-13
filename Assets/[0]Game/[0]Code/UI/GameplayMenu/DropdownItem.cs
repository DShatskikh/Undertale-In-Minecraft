using TMPro;
using UnityEngine;

namespace Game
{
    public class DropdownItem : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _label;
        
        public void Select(bool value)
        {
            _label.color = value ? GameData.AssetProvider.SelectColor : GameData.AssetProvider.DeselectColor;
        }
    }
}