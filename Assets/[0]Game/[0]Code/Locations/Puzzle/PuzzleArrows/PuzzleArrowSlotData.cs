using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [Serializable]
    public struct PuzzleArrowSlotData
    {
        public Sprite View;
        public SpriteRenderer ArrowSpriteRenderer;
        public Transform ArrowCenter;
        public ReactiveProperty<ArrowDirection> ArrowDirection;
        public ArrowDirection StartArrowDirection;
        public ArrowDirection Decision;
    }
}