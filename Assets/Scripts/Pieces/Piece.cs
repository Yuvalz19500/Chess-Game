using System;
using Enums;
using UnityEngine;

namespace Pieces
{
    [RequireComponent(typeof(PieceMaterialSetter))]
    public abstract class Piece : MonoBehaviour
    {
        private PieceMaterialSetter _pieceMaterialSetter;
        
        public TeamColor Team { get; set; }
        public Vector2Int SquarePosition { get; set; }

        private void Awake()
        {
            _pieceMaterialSetter = GetComponent<PieceMaterialSetter>();
        }
    }
}