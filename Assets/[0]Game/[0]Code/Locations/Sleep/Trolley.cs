using System;
using System.Collections;
using UnityEngine;

namespace Game.Sleep
{
    public enum StopEnum : byte
    {
        Bed = 0,
        City = 1,
        ThroneRoom = 2
    }
    
    public sealed class Trolley : UseObject
    {
        [SerializeField]
        private TrolleyMenu _trolleyMenu;

        [SerializeField]
        private StopEnum _stopEnum;

        [SerializeField]
        private Transform[] _stopTransforms;

        [SerializeField]
        private Transform _sitPoint;

        [SerializeField]
        private AudioClip _sitSFX;
        
        [SerializeField]
        private AudioSource _trolleySFX;
        
        public override void Use()
        {
            _trolleyMenu.Open(this, _stopEnum);
        }

        public void StartMove(int stopEnum)
        {
            StartCoroutine(AwaitMove(stopEnum));
        }

        private IEnumerator AwaitMove(int stopEnum)
        {
            _trolleyMenu.Close();
            GameData.Character.enabled = false;
            
            GameData.Character.View.Flip(false);
            GetComponent<SpriteRenderer>().sortingOrder = -5;

            var delta = 0f;
            var startPosition = GameData.Character.transform.position;
            
            while (delta < 1)
            {
                delta += Time.deltaTime / 1f;
                GameData.Character.transform.position = Vector2.Lerp(startPosition, _sitPoint.position, delta);
                yield return null;
            }
            
            GameData.Character.View.Sit(true);
            GameData.Character.transform.SetParent(transform);
            GameData.EffectAudioSource.clip = _sitSFX;
            GameData.EffectAudioSource.Play();

            _trolleySFX.Play();

            transform.localScale = new Vector3(transform.position.x > _stopTransforms[stopEnum].position.x ? -1 : 1, 1, 1);
            
            delta = 0f;

            while (Vector2.Distance(transform.position, _stopTransforms[stopEnum].position) > 0.1f)
            {
                delta += Time.deltaTime / 1f;
                transform.position = Vector2.MoveTowards(transform.position, _stopTransforms[stopEnum].position, Time.deltaTime * 5);
                yield return null;
            }

            _trolleySFX.Stop();
            GameData.Character.transform.SetParent(null);
            GameData.Character.View.Sit(false);
            GameData.Character.transform.localScale = new Vector3(1, 1, 1);

            delta = 0f;
            
            while (delta < 1)
            {
                delta += Time.deltaTime / 1f;
                GameData.Character.transform.position = Vector2.Lerp(_sitPoint.position, transform.position + new Vector3(0, stopEnum switch
                {
                    0 => -1,
                    1 => 1,
                    2 => 1,
                    _ => throw new ArgumentOutOfRangeException(nameof(stopEnum), stopEnum, null)
                }), delta);
                
                yield return null;
            }
            
            GameData.EffectAudioSource.clip = _sitSFX;
            GameData.EffectAudioSource.Play();
            
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            GameData.Character.enabled = true;
        }
    }
}