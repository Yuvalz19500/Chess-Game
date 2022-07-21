using System;
using System.Linq;
using Core;
using Enums;
using Pieces;
using Scriptable_Objects;
using UnityEngine;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(PieceCreator))]
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private BoardStartingLayout boardStartingLayout;
        [SerializeField] private Board board;

        private PieceCreator _pieceCreator;
        private Player _whitePlayer;
        private Player _blackPlayer;
        private Player _activePlayer;

        protected override void Awake()
        {
            base.Awake();
            SetupDependencies();
        }

        private void Start()
        {
            StartNewGame();
        }
        
        private void SetupDependencies()
        {
            UIManager.LoadUISceneIfNotLoaded();
            _pieceCreator = GetComponent<PieceCreator>();
            CreatePlayers();
        }
        
        private void StartNewGame()
        {
            InitBoardLayout();
            GenerateActiveMovesForPlayer(_activePlayer);
        }

        private void CreatePlayers()
        {
            _whitePlayer = new Player(TeamColor.White, board);
            _blackPlayer = new Player(TeamColor.Black, board);
            _activePlayer = _whitePlayer;
        }
 
        private void InitBoardLayout()
        {
            for (int i = 0; i < boardStartingLayout.GetPiecesCount(); i++)
            {
                Vector2Int piecePos = boardStartingLayout.GetPiecePositionAtIndex(i);
                string pieceName = boardStartingLayout.GetPieceNameAtIndex(i);
                TeamColor pieceColor = boardStartingLayout.GetPieceTeamColorAtIndex(i);
                
                CreatePieceFromLayout(piecePos, pieceName, pieceColor);
            }
        }

        private void CreatePieceFromLayout(Vector2Int piecePos, string pieceName, TeamColor pieceTeamColor)
        {
            Piece piece = _pieceCreator.CreatePiece(pieceName).GetComponent<Piece>();
            piece.SetPieceMaterial(_pieceCreator.GetTeamMaterial(pieceTeamColor));
            piece.SetPieceData(piecePos, pieceTeamColor, board);

            Player currentPlayer = pieceTeamColor == TeamColor.Black ? _blackPlayer : _whitePlayer;
            currentPlayer.AddPiece(piece);
            
            board.SetPieceOnBoard(piece, piecePos);
        }
        
        private Player GetOpponentToActivePlayer()
        {
            return _activePlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
        }
        
        private void GenerateActiveMovesForPlayer(Player player)
        {
            player.GeneratePossibleMoves();
        }

        private void ChangeActiveTeam()
        {
            _activePlayer = _activePlayer == _whitePlayer ? _blackPlayer : _whitePlayer;
        }

        public void EndTurn()
        {
            GenerateActiveMovesForPlayer(_activePlayer);
            GenerateActiveMovesForPlayer(GetOpponentToActivePlayer());

            if (IsGameFinished())
            {
                EndGame();
            }
            
            ChangeActiveTeam();
        }

        private void EndGame()
        {
            throw new NotImplementedException();
        }

        private bool IsGameFinished()
        {
            Piece[] piecesAttackingOppositeKing = _activePlayer.GetPiecesAttackingOppositePieceOfType<King>();

            if (piecesAttackingOppositeKing.Length > 0)
            {
                Player opponent = GetOpponentToActivePlayer();
                Piece attackedKing = opponent.GetPiecesOfType<King>().FirstOrDefault();
                opponent.RemoveMovesEnablingAttackOnPieceOfType<King>(_activePlayer, attackedKing);
            }
            return false;
        }

        public TeamColor GetActiveTeamColorTurn()
        {
            return _activePlayer == _whitePlayer ? TeamColor.White : TeamColor.Black;
        }

        public void OnPieceTaken(Piece piece)
        {
            GetOpponentToActivePlayer().RemovePiece(piece);
        }

        public Piece CreatePieceForPromotion(Piece oldPawn, PieceType type, Vector2Int position, TeamColor teamColor)
        {
            _activePlayer.RemovePiece(oldPawn);
            
            Piece piece = _pieceCreator.CreatePiece(type.ToString()).GetComponent<Piece>();
            piece.SetPieceMaterial(_pieceCreator.GetTeamMaterial(teamColor));
            piece.SetPieceData(position, teamColor, board);
            
            _activePlayer.AddPiece(piece);
            return piece;
        }
    }
}