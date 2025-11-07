using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public sealed class KingMobs_7_Dialogue : UseObject
    {
        [SerializeField]
        private Replica[] _replicas_1;
        
        [SerializeField]
        private Replica[] _replicas_2;
        
        [SerializeField]
        private Replica[] _replicas_3;
        
        [SerializeField]
        private Replica[] _replicas_4;
        
        [FormerlySerializedAs("_replicas_isNoneItem")]
        [SerializeField]
        private Replica[] _replicas_isNoneItemFirst_0;
        
        [FormerlySerializedAs("_replicas_isNoneItemFirst")]
        [SerializeField]
        private Replica[] _replicas_isNoneItem_0;

        private int _currentReplica = 1;
        
        public override void Use()
        {
            if (YG.YandexGame.savesData.IsNoneItem)
            {
                if (YG.YandexGame.savesData.IsNoneItemFirst)
                {
                    GameData.Dialog.SetReplicas(_replicas_isNoneItemFirst_0);
                }
                else
                {
                    GameData.Dialog.SetReplicas(_replicas_isNoneItem_0);
                }
                
                return;
            }
            
            if (_currentReplica == 1)
            {
                _currentReplica = 2;
                GameData.Dialog.SetReplicas(_replicas_1);
            }
            else if (_currentReplica == 2)
            {
                _currentReplica = 3;
                GameData.Dialog.SetReplicas(_replicas_2);
            }
            else if (_currentReplica == 3)
            {
                _currentReplica = 4;
                GameData.Dialog.SetReplicas(_replicas_3);
            }
            else if (_currentReplica == 4)
            {
                GameData.Dialog.SetReplicas(_replicas_4);
            }
        }
    }
}