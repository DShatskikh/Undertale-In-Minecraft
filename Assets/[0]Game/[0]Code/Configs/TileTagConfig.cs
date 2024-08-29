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
        public List<TileBase> Sponge;
        public List<TileBase> Sand;

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
            
            if (Sponge.Any(currentTile => tile == currentTile))
                return config.SpongeStepPair;
            
            if (Sand.Any(currentTile => tile == currentTile))
                return config.SandStepPair;

            return config.StoneStepPair;
        }
    }
}