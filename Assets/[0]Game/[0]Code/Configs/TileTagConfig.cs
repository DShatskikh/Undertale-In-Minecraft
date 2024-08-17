using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    [CreateAssetMenu(fileName = "TileTagConfig", menuName = "Data/TileTagConfig", order = 81)]
    public class TileTagConfig : ScriptableObject
    {
        public List<TileBase> Stone;
        public List<TileBase> Grass;
        public List<TileBase> Dirt;
        public List<TileBase> Wood;

        public StepSoundPair GetPair(TileBase tile, StepSoundPairsConfig config)
        {
            if (Stone.Any(currentTile => tile == currentTile))
                return config.StoneStepPair;

            if (Grass.Any(currentTile => tile == currentTile))
                return config.GrassStepPair;

            if (Dirt.Any(currentTile => tile == currentTile))
                return config.DirtStepPair;

            if (Wood.Any(currentTile => tile == currentTile))
                return config.WoodStepPair;

            return config.StoneStepPair;
        }
    }
}