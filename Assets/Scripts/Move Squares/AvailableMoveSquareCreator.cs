using System.Collections.Generic;
using Enums;
using Pieces;
using UnityEngine;

namespace Move_Squares
{
    public class AvailableMoveSquareCreator : MonoBehaviour
    {
        [SerializeField] private GameObject squarePrefab;
        [SerializeField] private Material moveMaterial;
        [SerializeField] private Material takeMaterial;
        [SerializeField] private Transform moveSquaresParentObjectTransform;

        private readonly Dictionary<GameObject, MoveInfo> _activeSquares = new Dictionary<GameObject, MoveInfo>();
        
        public void ClearActiveSquares()
        {
            foreach (KeyValuePair<GameObject, MoveInfo> square in _activeSquares)
            {
                Destroy(square.Key);
            } 
            
            _activeSquares.Clear();
        }
        
        public void DisplayAvailableSquareMoves(Dictionary<MoveInfo, PieceMoveType> movesDict)
        {
            ClearActiveSquares();
            
            foreach (KeyValuePair<MoveInfo, PieceMoveType> move in movesDict)
            {
                GameObject moveSquare = Instantiate(squarePrefab, moveSquaresParentObjectTransform, true);
                moveSquare.transform.position = move.Key.WorldPosition;
                MeshRenderer[] renderers = moveSquare.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer mRenderer in renderers)
                {
                    mRenderer.material = move.Value switch
                    {
                        PieceMoveType.Take => takeMaterial,
                        PieceMoveType.Move => moveMaterial,
                        _ => null
                    };
                }
                
                _activeSquares.Add(moveSquare, move.Key);
            }
        }

        public MoveInfo GetMoveInfoForSelectedSquare(GameObject square)
        {
            return _activeSquares[square];
        }
    }
}