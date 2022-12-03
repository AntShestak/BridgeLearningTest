using System.Collections.Generic;
using UnityEngine;

namespace BridgeLearningTest {
    public sealed class SurfaceTileController : MonoBehaviour
    {
        [SerializeField] int _gridHeight;
        [SerializeField] int _gridWidth;
        [SerializeField] float _tileSize = 1;

        private Tile[,] _tiles; //grid
        private List<Vector3> _freeTilePositions; //list of tile positions that are free

        // Start is called before the first frame update
        void Start()
        {
            _tiles = new Tile[_gridWidth,_gridHeight];
            _freeTilePositions = new List<Vector3>(_gridWidth*_gridHeight);
            GenerateTiling();
        }

        private void GenerateTiling()
        {
            
            
            float halfTile = _tileSize / 2; 

            for (int x = 0; x < _gridWidth; x++)
            {
                for (int y = 0; y < _gridHeight; y++)
                {
                    //calculating position for the new tile
                    float xPos = x - _gridWidth / 2 + halfTile;
                    float yPos = y - _gridHeight / 2 + halfTile;
                    Vector3 tilePos = new Vector3(xPos, 0f, yPos);
                
                    //create tile
                    Tile newTile = new Tile(tilePos);
                    _tiles[x,y] = newTile;

                    //assign neighbours East
                    newTile.NeighbourTiles = new List<Tile>();
                    if (x != 0)
                    {
                        Tile neighbour = _tiles[x - 1, y];
                        newTile.NeighbourTiles.Add(neighbour);  //add neighbours
                        neighbour.NeighbourTiles.Add(newTile);  //become a member

                    }
                    //assign neighbours South
                    if (y != 0)
                    {
                        Tile neighbour = _tiles[x, y - 1];
                        newTile.NeighbourTiles.Add(neighbour);
                        neighbour.NeighbourTiles.Add(newTile);
                    }            
                    
                    //save as free position
                    _freeTilePositions.Add(tilePos);

                }
            }
        }

        public Tile GetTileFromPosition(Vector3 pos)
        {
            float halfTile = _tileSize / 2;
            int x = Mathf.RoundToInt(pos.x + _gridWidth / 2 - halfTile);
            int y = Mathf.RoundToInt(pos.z + _gridHeight / 2 - halfTile);

            //Debug.Log(pos + " to tile" + _tiles[x,y].WorldPosition);

            return _tiles[x, y];
        }


        /// <summary>
        /// Method <c>GetRandomEmptyTile</c> returns random tile and removes from list.
        /// returns NULL if there's no empty tile
        /// </summary>
        public Vector3? GetRandomEmptyTile()
        {
            

            if (_freeTilePositions.Count > 0)
            {
                int rand = Random.Range(0, _freeTilePositions.Count);
                Vector3 pos = _freeTilePositions[rand];
                _freeTilePositions.Remove(pos);
                return pos;
            }
            
            return null;
        }

        /// <summary>
        /// Used by obstacle spawner
        /// Return random free position and set's tile as blocked
        /// </summary>
        /// <returns></returns>
        public Vector3? GetRandomPosAndBlockTile()
        {

            Vector3? pos = GetRandomEmptyTile();

            if (pos != null)
            {
                GetTileFromPosition((Vector3)pos).IsBlocked = true;
                return pos;

            }
            
            return null;
        }

        /// <summary>
        /// method used by CollectibleController class to get collectible spawn position
        /// </summary>
        /// <returns></returns>
        public Vector3? GetRandomPosAndOccupyTile()
        {

            Vector3? pos = GetRandomEmptyTile();

            if (pos != null)
            {
                GetTileFromPosition((Vector3)pos).IsOccupied = true;
                return pos;

            }

            return null;

        }

        public void ReturnOccupiedTile(Vector3 pos)
        {
            Tile tile = GetTileFromPosition(pos);

            if (tile.IsOccupied)
            {
                tile.IsOccupied = false;
                _freeTilePositions.Add(tile.WorldPosition);
            }
            else
                Debug.Log("ERRR!!!Returning non occupied tile");
        }
    }
}

