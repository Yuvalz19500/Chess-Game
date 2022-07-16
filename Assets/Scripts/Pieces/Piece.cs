using System;
using Core;
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
        public Board Board { get; set; }

        private void Awake()
        {
            _pieceMaterialSetter = GetComponent<PieceMaterialSetter>();
        }

        public void SetPieceMaterial(Material pieceMaterial)
        {
            _pieceMaterialSetter.SetParentMaterial(pieceMaterial);
        }

        public void SetPieceData(Vector2Int squarePos, TeamColor team, Board board)
        {
            SquarePosition = squarePos;
            Team = team;
            Board = board;
        }
    }
}