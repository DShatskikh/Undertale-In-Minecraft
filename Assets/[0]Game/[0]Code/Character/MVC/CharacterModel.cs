using System;
using UnityEngine;

namespace Game
{
    public class CharacterModel
    {
        public event Action<float> SpeedChange;
        public event Action<Vector2> DirectionChange;
        public event Action<bool> FlyChange;

        public float Speed;
        public bool IsRun;
        public bool IsFly;
        public Vector2 Direction;

        public void SetSpeed(float value)
        {
            Speed = value;
            SpeedChange?.Invoke(value);
        }

        public void SetDirection(Vector2 value)
        {
            if (value == Vector2.zero && GameData.Joystick.Direction.magnitude > 0.5f)
            {
                value = DirectionExtensions.GetDirection8(GameData.Joystick.Direction.normalized);
                IsRun = GameData.Joystick.Direction.magnitude == 1f;
            }

            Direction = value;
            DirectionChange?.Invoke(value);
        }

        public void SetFly(bool value)
        {
            IsFly = value;
            FlyChange.Invoke(value);
        }
    }
}