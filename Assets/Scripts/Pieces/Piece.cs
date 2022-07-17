﻿using System;
using System.Collections.Generic;
using Core;
using Enums;
using Pieces.Movement;
using UnityEngine;

namespace Pieces
{
    [RequireComponent(typeof(PieceMaterialSetter))]
    [RequireComponent(typeof(IPieceMover))]
    public abstract class Piece : MonoBehaviour
    {
        private PieceMaterialSetter _pieceMaterialSetter;
        private IPieceMover _pieceMover;
        protected Vector2Int SquarePosition { get; private set; }
        protected Board Board { get; private set; }
        protected bool HasMoved { get; } = false;
        public TeamColor Team { get; private set; }
        public Dictionary<Vector3, PieceMoveType> MovesDict { get; } = new Dictionary<Vector3, PieceMoveType>();

        public abstract void SetAvailableMoves();
        private void Awake()
        {
            SetupDependencies();
        }

        private void SetupDependencies()
        {
            _pieceMaterialSetter = GetComponent<PieceMaterialSetter>();
            _pieceMover = GetComponent<IPieceMover>();
        }

        private void OnMouseDown()
        {
            Board.OnPieceSelected(this);
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

            gameObject.transform.position = board.CalculateBoardPositionFromSquarePosition(SquarePosition);
        }

        public bool IsPieceFromSameTeam(Piece piece)
        {
            return Team == piece.Team;
        }
    }
}