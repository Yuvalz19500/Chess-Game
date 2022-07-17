using System.Collections.Generic;
using Enums;
using Pieces;
using UnityEngine;

namespace Core
{
    public class AvailableMoveSquareCreator : MonoBehaviour
    {
        [SerializeField] private GameObject squarePrefab;
        [SerializeField] private Material moveMaterial;
        [SerializeField] private Material takeMaterial;
        [SerializeField] private Transform moveSquaresParentObjectTransform;

        private readonly List<GameObject> _activeSquares = new List<GameObject>();
        
        private void ClearActiveSquares()
        {
            foreach (GameObject square in _activeSquares)
            {
                Destroy(square);
            }
            
            _activeSquares.Clear();
        }
        
        public void DisplayAvailableSquareMoves(Dictionary<Vector3, PieceMoveType> movesDict)
        {
            ClearActiveSquares();
            
            foreach (KeyValuePair<Vector3, PieceMoveType> move in movesDict)
            {
                GameObject moveSquare = Instantiate(squarePrefab, moveSquaresParentObjectTransform, true);
                moveSquare.transform.position = move.Key;
                MeshRenderer[] renderers = moveSquare.GetComponentsInChildren<MeshRenderer>();

                foreach (MeshRenderer mRenderer in renderers)
                {
                    mRenderer.material = move.Value == PieceMoveType.Take ? takeMaterial : moveMaterial;
                }
                
                _activeSquares.Add(moveSquare);
            }
        }
    }
}