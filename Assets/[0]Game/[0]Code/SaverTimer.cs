using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class SaverTimer : MonoBehaviour
    {
        [SerializeField]
        private float _saveTime = 10f;

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
                yield return new WaitUntil(() => !GameData.Battle.gameObject.activeSelf);
                GameData.Saver.Save();  
            }

            /*
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
            }*/
        }
    }
}