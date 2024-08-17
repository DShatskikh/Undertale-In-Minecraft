using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class CharacterStep : MonoBehaviour
    {
        [SerializeField] 
        private AudioSource _stepSource1, _stepSource2;

        [SerializeField] 
        private float _intervalStep = 0.7f;

        [SerializeField]
        private LayerMask _layerMask;
        
        private bool _isStepRight;
        private float _currentStepTime;
        private CharacterModel _model;

        private void Start()
        {
            _currentStepTime = _intervalStep;
        }

        public void SetModel(CharacterModel model)
        {
            _model = model;
            _model.SpeedChange += OnSpeedChange;
        }

        private void OnDestroy()
        {
            _model.SpeedChange -= OnSpeedChange;
        }
        
        private void OnSpeedChange(float value)
        {
            if (value == 0)
                return;
            
            _currentStepTime += Time.deltaTime;
                
            if (_currentStepTime >= (_model.IsRun ? _intervalStep / 2 : _intervalStep))
            {
                _currentStepTime = 0;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, _layerMask);
                TileBase tile = null;
                
                if (hit.collider != null)
                {
                    var tilemap = hit.collider.GetComponent<Tilemap>();
                    Vector3Int cellPosition = tilemap.WorldToCell(hit.point);
                    tile = tilemap.GetTile(cellPosition);
                }

                PlayFootstepSound(tile);
                _isStepRight = !_isStepRight;
            }
        }

        private void PlayFootstepSound(TileBase tile)
        {
            var assetProvider = GameData.AssetProvider;
            var config = assetProvider.StepSoundPairsConfig;
            var pair = assetProvider.TileTagConfig.GetPair(tile, config);
            
            if (_isStepRight)
            {
                AudioClip clipToPlay = pair.Right;
                _stepSource1.clip = clipToPlay;
                _stepSource1.Play();
            }
            else
            {
                AudioClip clipToPlay = pair.Left;
                _stepSource2.clip = clipToPlay;
                _stepSource2.Play();   
            }
        }
    }
}