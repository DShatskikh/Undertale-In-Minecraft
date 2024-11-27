using System.Collections;
using UnityEngine;

namespace Game
{
    public class SaverTimer : MonoBehaviour
    {
        [SerializeField]
        private float _saveTime = 5f;

        private Coroutine _coroutine;
        
        private void OnEnable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(AwaitSave());
        }

        private IEnumerator AwaitSave()
        {
            while (true)
            {
                yield return new WaitForSeconds(_saveTime);
                GameData.SaveLoadManager.Save();
            }
        }
    }
}