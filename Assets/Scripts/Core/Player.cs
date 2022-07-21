using System.Collections.Generic;
using System.Linq;
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
        public List<Piece> ActivePieces { get; } = new List<Piece>();

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
            foreach (Piece piece in ActivePieces.Where(piece => _board.IsPieceOnBoard(piece)))
            {
                piece.SetAvailableMoves();
            }
        }

        public Piece[] GetPiecesAttackingOppositePieceOfType<T>() where T : Piece
        {
            return ActivePieces.Where(p => p.IsAttackingPieceOfType<T>()).ToArray();
        }

        public void RemoveMovesEnablingAttackOnPieceOfType<T>(Player otherPlayer, Piece attackedPiece) where T : Piece
        {
            foreach (KeyValuePair<MoveInfo, PieceMoveType> move in attackedPiece.MovesDict)
            {
                //otherPlayer.GetPieceAttackingSquare
            }
        }

        public Piece[] GetPiecesOfType<T>() where T : Piece
        {
            return ActivePieces.Where(piece => piece is T).ToArray();
        }
    }
}