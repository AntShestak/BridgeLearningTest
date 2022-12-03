using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeLearningTest
{
    /// <summary>
    /// Square surface unit
    /// </summary>
    public class Tile
    {
        public Vector3 WorldPosition { get; set; }

        public List<Tile> NeighbourTiles { get; set; }
        public bool IsOccupied { get; set; }    //is tile occupied by collectible

        public bool IsBlocked { get; set; }     //is tile blocked with obstacle

        public Tile(Vector3 pos, bool isOccupied = false, bool isBlocked = false)
        {
            WorldPosition = pos;
            IsOccupied = isOccupied;
            IsBlocked = isBlocked;
        }
    }
}