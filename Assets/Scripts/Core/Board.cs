using Pieces;
using UnityEngine;

namespace Core
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Transform bottomLeftSquare;
        [SerializeField] private float squareSize = 1.5f;

        private const int BoardSize = 8;
        private readonly Piece[,] _piecesGrid = new Piece[BoardSize, BoardSize];

        public Vector3 CalculateBoardPositionFromSquarePosition(Vector2Int squarePos)
        {
            return bottomLeftSquare.position + new Vector3(squarePos.x * squareSize, 0f, squarePos.y * squareSize);
        }

        public void SetPieceOnBoard(Piece piece, Vector2Int coords) 
        {
            if (CheckIfCoordsAreOnBoard(coords))
            {
                _piecesGrid[coords.x, coords.y] = piece;
            }
        }

        public bool IsPieceOnBoard(Piece piece)
        {
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    if (_piecesGrid[i, j] == piece)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckIfCoordsAreOnBoard(Vector2Int coords)
        {
            return coords.x is >= 0 and < BoardSize && coords.y is >= 0 and < BoardSize;
        }
    }
}