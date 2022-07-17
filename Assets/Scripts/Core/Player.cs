using System.Collections.Generic;
using Enums;
using Managers;
using Pieces;
using UnityEngine;

namespace Core
{
    public class Player
    {
        private Board _board;
        public TeamColor Team { get; }
        public List<Piece> ActivePieces { get; }

        public Player(TeamColor team, Board board)
        {
            Team = team;
            _board = board;
        }
        public void AddPiece(Piece piece)
        {
            if (!ActivePieces.Contains(piece))
            {
                ActivePieces.Add(piece);
            }
        }
        
        public void RemovePiece(Piece piece)
        {
            if (ActivePieces.Contains(piece))
            {
                ActivePieces.Remove(piece);
            }
        }

        public void GeneratePossibleMoves()
        {
            foreach (var piece in ActivePieces)
            {
                if (_board.IsPieceOnBoard(piece))
                {
                    piece.SetAvailableMoves();
                }
            }
        }
    }
}