using System.Collections;
using System.Linq;
using Cinemachine;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Game
{
    public class BattleIntro : MonoBehaviour
    {
        [SerializeField]
        private MMF_Player _feedback;

        [SerializeField]
        private GameObject _battle;

        public IEnumerator Intro()
        {
            GameData.Heart.gameObject.SetActive(false);
            yield return _feedback.PlayFeedbacksCoroutine(Vector3.zero);
        }

        public void ChangeLocation()
        {
            GameData.Locations.ToArray()[GameData.LocationIndex].gameObject.SetActive(false);
            GameData.Heart.transform.position = GameData.Character.transform.position;
            GameData.Character.gameObject.SetActive(false);
            GameData.Heart.gameObject.SetActive(true);
            
            _battle.SetActive(true);
        }
    }
}