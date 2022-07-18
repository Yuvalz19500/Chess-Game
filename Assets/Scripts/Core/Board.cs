using Managers;
using Move_Squares;
using Pieces;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(AvailableMoveSquareCreator))]
    public class Board : MonoBehaviour
    {
        [SerializeField] private Transform bottomLeftSquare;
        [SerializeField] private float squareSize = 1.5f;

        private const int BoardSize = 8;
        private readonly Piece[,] _piecesGrid = new Piece[BoardSize, BoardSize];
        private Piece _activePiece;
        private AvailableMoveSquareCreator _squareCreator;

        private void Awake()
        {
            SetupDependencies();
        }

        private void SetupDependencies()
        {
            _squareCreator = GetComponent<AvailableMoveSquareCreator>();
        }
        
        private void MoveActivePieceAndTake(Piece pieceToTake, MoveInfo moveInfo)
        {
            EndTurn();
        }
        
        private void MoveActivePiece(MoveInfo moveInfo)
        {
            UpdateBoardOnPieceMove(_activePiece, moveInfo.GridPosition, _activePiece.SquarePosition);
            _activePiece.MovePiece(moveInfo);

            EndTurn();
        }

        private void EndTurn()
        {
            DeselectPiece();
            GameManager.Instance.EndTurn();
        }

        private void UpdateBoardOnPieceMove(Piece piece, Vector2Int newPos, Vector2Int oldPos)
        {
            _piecesGrid[oldPos.x, oldPos.y] = null;
            _piecesGrid[newPos.x, newPos.y] = piece;
        }

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
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (_piecesGrid[i, j] == piece)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void OnPieceSelected(Piece piece)
        {
            if (_activePiece == piece)
            {
                DeselectPiece();
                return;
            }
            if (piece.Team != GameManager.Instance.GetActiveTeamColorTurn()) return;
            
            _activePiece = piece;
            _squareCreator.DisplayAvailableSquareMoves(_activePiece.MovesDict);
        }

        private void DeselectPiece()
        {
            _activePiece = null;
            _squareCreator.ClearActiveSquares();
        }

        public Piece GetPieceOnBoardFromSquareCoords(Vector2Int coords)
        {
            return CheckIfCoordsAreOnBoard(coords) ? _piecesGrid[coords.x, coords.y] : null;
        }
        
        public bool CheckIfCoordsAreOnBoard(Vector2Int coords)
        {
            return coords.x is >= 0 and < BoardSize && coords.y is >= 0 and < BoardSize;
        }

        public void OnSquareSelected(MoveInfo moveInfo)
        {
            Piece targetSquarePiece = GetPieceOnBoardFromSquareCoords(moveInfo.GridPosition);
            if (targetSquarePiece)
            {
                MoveActivePieceAndTake(targetSquarePiece, moveInfo);
            }
            else
            {
                MoveActivePiece(moveInfo);
            }
        }
    }
}