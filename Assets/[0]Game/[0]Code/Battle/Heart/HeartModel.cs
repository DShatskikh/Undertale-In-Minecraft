using System;
using UnityEngine;

namespace Game
{
    public class HeartModel
    {
        public event Action<float> SpeedChange;
        public event Action<bool> ShieldActivate;
        public event Action<bool> Invulnerability;
        public event Action<Vector2> DirectionChange;
        
        public bool IsInvulnerability;
        public float Speed;
        public Vector2 Direction;

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

        public void SetDirection(Vector2 value)
        {
            if (value == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
                value = DirectionExtensions.GetDirection8(GameData.Joystick.Direction.normalized);

            Direction = value;
            DirectionChange?.Invoke(value);
        }
        
        public void SetIsInvulnerability(bool isInvulnerability)
        {
            IsInvulnerability = isInvulnerability;
            Invulnerability?.Invoke(isInvulnerability);
            
            if (isInvulnerability)
                ShieldActivate?.Invoke(false);
        }

        public void SetSpeed(float value)
        {
            Speed = value;
            SpeedChange?.Invoke(value);
        }
    }
}