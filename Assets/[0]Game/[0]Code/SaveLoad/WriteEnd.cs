using UnityEngine;

namespace Game
{
    public class WriteEnd : MonoBehaviour
    {
        [SerializeField]
        private SaveKeyInt _currentEndKey;
        
        [SerializeField]
        private SaveKeyInt _previousEndKey;

        [SerializeField]
        private Endings _end;
        
        public void Save()
        {
            GameData.Saver.Save(_previousEndKey, GameData.Saver.LoadKey(_currentEndKey));
            GameData.Saver.Save(_currentEndKey, (int)_end);
        }
    }
}