using System.Collections;
using UnityEngine;

namespace Game
{
    public class SaverTimer : MonoBehaviour
    {
        [SerializeField]
        private float _saveTime = 10f;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_saveTime);
                yield return new WaitUntil(() => !GameData.Battle.gameObject.activeSelf);
                GameData.Saver.SaveAll();
            }
        }
    }
}