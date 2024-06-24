using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Game
{
    public class DeveloperCall : MonoBehaviour
    {
        [SerializeField]
        private UseMonolog _call;
        
        [SerializeField]
        private UseMonolog _strange, _strange_notConditions;

        [SerializeField]
        private UseMonolog _good_first;

        [SerializeField]
        private UseMonolog _bad_first;

        [SerializeField]
        private UseMonolog _badAndGood_strangeConditions;

        [SerializeField]
        private UseDialog _palesos;

        [SerializeField]
        private ToMenu _toMenu;

        [SerializeField]
        private UseReset _useReset;
        
        private IEnumerator Start()
        {
            if (TryGetEnd(out UseMonolog end))
            {
                yield return new WaitForSeconds(2);
                _call.Use();
            
                yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
                end.Use();
                
                yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
            }

            if (YandexGame.savesData.Palesos == 3)
            {
                yield return new WaitForSeconds(2);
                _call.Use();
                
                yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
                
                _palesos.Use();
                YandexGame.savesData.Palesos += 1;
                
                yield return new WaitUntil(() => !GameData.Dialog.gameObject.activeSelf);
            }
            
            GameData.Saver.Reset();
            SceneManager.LoadScene(0);
        }

        private bool TryGetEnd(out UseMonolog end)
        {
            if (GameData.CurrentEnd == End.Strange)
            {
                YandexGame.savesData.IsDeveloperKey = true;
                
                if (YandexGame.savesData.IsBadEnd && YandexGame.savesData.IsGoodEnd)
                    end = _strange;
                else
                    end = _strange_notConditions;

                return true;
            }

            if (GameData.CurrentEnd == End.Good && !YandexGame.savesData.IsBadEnd)
            {
                end = _good_first;
                return true;
            }

            if (GameData.CurrentEnd == End.Bad && !YandexGame.savesData.IsGoodEnd)
            {
                end = _bad_first;
                return true;
            }

            if (!YandexGame.savesData.IsStrangeEnd)
            {
                end = _badAndGood_strangeConditions;
                return true;
            }
            
            end = null;
            return false;
        }
    }
}