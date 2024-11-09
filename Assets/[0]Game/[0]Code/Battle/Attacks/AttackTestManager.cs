using UnityEngine;

namespace Game
{
    public class AttackTestManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _attack;

        [Header("Initilization")]
        [SerializeField]
        private HeartController _heartController;

        [SerializeField]
        private BattleArena _battleArena;
        
        private void Awake()
        {
            GameData.CoroutineRunner = this;
            GameData.HeartController = _heartController;
        }

        private void Start()
        {
            _battleArena.gameObject.SetActive(true);
            _battleArena.ShowAdditionalObjects(true);
            _battleArena.UpgradeA(1);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Submit"))
            {
                var attack = Instantiate(_attack);
                attack.SetActive(true);

                if (attack.TryGetComponent(out AttackBase attackBase))
                {
                    attackBase.Execute(() => { });
                }
            }
        }
    }
}