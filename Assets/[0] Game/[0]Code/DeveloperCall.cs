using System.Collections;
using UnityEngine;

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

            if (GameData.Palesos == 3)
            {
                yield return new WaitForSeconds(2);
                _call.Use();
                
                yield return new WaitUntil(() => !GameData.Monolog.gameObject.activeSelf);
                
                _palesos.Use();
                GameData.Palesos += 1;
                
                yield return new WaitUntil(() => !GameData.Dialog.gameObject.activeSelf);
            }
            
            _useReset.Use();
            _toMenu.Use();
        }

        private bool TryGetEnd(out UseMonolog end)
        {
            if (GameData.CurrentEnd == End.Strange)
            {
                GameData.IsDeveloperKey = true;
                
                if (GameData.IsBadEnd && GameData.IsGoodEnd)
                    end = _strange;
                else
                    end = _strange_notConditions;

                return true;
            }

            if (GameData.CurrentEnd == End.Good && !GameData.IsBadEnd)
            {
                end = _good_first;
                return true;
            }

            if (GameData.CurrentEnd == End.Bad && !GameData.IsGoodEnd)
            {
                end = _bad_first;
                return true;
            }

            if (!GameData.IsStrangeEnd)
            {
                end = _badAndGood_strangeConditions;
                return true;
            }
            
            end = null;
            return false;
        }
    }
}