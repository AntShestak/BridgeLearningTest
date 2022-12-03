using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BridgeLearningTest
{
    public sealed class Floodfill : MonoBehaviour
    {
        public GameObject go;

        private Transform _player;
        private SurfaceTileController _tiles;
        private Queue<Tile> _tilesToInspect;
        private List<Tile> _inspectedTiles;
        

        private void Awake()
        {
            _tiles = this.GetComponent<SurfaceTileController>();
        }
        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _tilesToInspect = new Queue<Tile>();
            _inspectedTiles = new List<Tile>();
            
        }

        private void OnEnable()
        {
            ObstacleController.OnObstacleSpawn += CheckForPossiblePath;
        }

        private void OnDisable()
        {
            ObstacleController.OnObstacleSpawn -= CheckForPossiblePath;
        }

        /// <summary>
        /// Checks if there's any pass for player to collectible object
        /// Calls game end if path is not found
        /// </summary>
        private void CheckForPossiblePath()
        {
            if (_player == null) return;
            bool solutionFound = false;

            Tile tile = _tiles.GetTileFromPosition(_player.position);
            _tilesToInspect.Clear();    //clear the queue
            _inspectedTiles.Clear();    //clear the list
            _tilesToInspect.Enqueue(tile);

            while (_tilesToInspect.Count > 0)
            {
                
                Tile inspecting = _tilesToInspect.Dequeue();
                //get neighbours
                var neighbourTiles = inspecting.NeighbourTiles;

                foreach (var neighbour in neighbourTiles)
                {
                    
                    //check if neighbour is occupied by collectible
                    if (neighbour.IsOccupied) 
                    {
                        //neighbour is collectible, path exists
                        solutionFound = true;
                        
                        break;
                    }
                    //check if neighbour is blocked
                    if (neighbour.IsBlocked) continue; //continue to next neighbour

                    //neighbour is a normal tile, has to be inspected, if it wasn't yet
                    if (!_inspectedTiles.Contains(neighbour))
                        _tilesToInspect.Enqueue(neighbour);
                }

                if (solutionFound) break;

                _inspectedTiles.Add(inspecting);
            }

            //if solution wasn't found game is over
            if (!solutionFound) GameController.Instance.GameOver();

            

           
        }
    }
}