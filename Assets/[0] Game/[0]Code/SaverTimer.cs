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
            var text = GameData.SaveText;
            
            while (true)
            {
                yield return new WaitForSeconds(_saveTime);
                yield return new WaitUntil(() => !GameData.Battle.gameObject.activeSelf);
                GameData.Saver.Save();
                text.gameObject.SetActive(true);
                
                var alpha = 0f;
            
                while (alpha < 1)
                {
                    alpha = text.color.a;
                    alpha += Time.deltaTime / 1;
                    text.color = text.color.SetA(alpha);
                    yield return null;
                }
                
                yield return new WaitForSeconds(2);

                while (alpha != 0)
                {
                    alpha = text.color.a;
                    alpha -= Time.deltaTime / 1;
                    text.color = text.color.SetA(alpha);
                    yield return null;
                }
                
                GameData.SaveText.gameObject.SetActive(false);
            }
        }
    }
}