using System;
using UnityEngine;

namespace Game
{
    public class HeartModel
    {
        public event Action<bool> ShieldActivate;
        public bool IsInvulnerability;

        public void AddTurnProgress(int value)
        {
            GameData.BattleProgress += value;
            
            if (GameData.BattleProgress > 100)
                GameData.BattleProgress = 100;
            
            EventBus.BattleProgressChange?.Invoke(GameData.BattleProgress);
        }

        public void SetIsShield(bool isActive)
        {
            ShieldActivate?.Invoke(isActive);
        }

        public void SetIsInvulnerability(bool isInvulnerability)
        {
            IsInvulnerability = isInvulnerability;
            
            if (isInvulnerability)
                ShieldActivate?.Invoke(false);
        }
    }
}