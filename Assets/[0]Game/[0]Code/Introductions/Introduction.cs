using TMPro;
using UnityEngine;
using YG;
using YG.Example;

namespace Game
{
    public class Introduction : MonoBehaviour
    {
        [SerializeField] 
        private string[] _texts;
        
        [SerializeField] 
        private TMP_Text _label;

        private int _index;
        
        public void NextText()
        {
            if (_index >= _texts.Length)
                return;
            
            _label.text = _texts[_index];
            _index++;
        }
    }
}