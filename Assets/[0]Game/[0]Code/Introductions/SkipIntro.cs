using UnityEngine;
using YG;

namespace Game
{
    public class SkipIntro : MonoBehaviour
    {
        [SerializeField]
        private Transform _point;

        [SerializeField]
        private GameObject _locationWorld, _locationHerobrineHome;
        
        public void Skip()
        {
            GameData.EffectAudioSource.clip = GameData.AssetProvider.DoorSound;
            GameData.EffectAudioSource.Play();
            GameData.Character.transform.position = _point.position;
            _locationHerobrineHome.SetActive(false);
            _locationWorld.SetActive(true);
            GameData.Character.gameObject.SetActive(true);
        }
    }
}