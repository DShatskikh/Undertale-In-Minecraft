using UnityEngine;

namespace Game
{
    public class HeartView : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _source;
        
        [SerializeField]
        private SpriteRenderer _shield;

        private HeartModel _model;

        public void SetModel(HeartModel model)
        {
            _model = model;
            _model.ShieldActivate += OnShieldActivate;
        }

        private void OnDestroy()
        {
            _model.ShieldActivate -= OnShieldActivate;
        }
        
        private void OnShieldActivate(bool isActivate)
        {
            if (isActivate)
                _source.Play();
            
            _shield.gameObject.SetActive(isActivate);
        }
    }
}