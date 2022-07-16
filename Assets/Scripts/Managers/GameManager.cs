using System;
using Core;
using Enums;
using Scriptable_Objects;
using UnityEngine;
using Utils;

namespace Managers
{
    [RequireComponent(typeof(PieceCreator))]
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private BoardStartingLayout boardStartingLayout;
        [SerializeField] private Transform bottomLeftSquare;

        private PieceCreator _pieceCreator;

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
            _pieceCreator = GetComponent<PieceCreator>();
        }
        
        private void StartNewGame()
        {
            InitBoardLayout();
        }

        private void InitBoardLayout()
        {
            for (var i = 0; i < boardStartingLayout.GetPiecesCount(); i++)
            {
                var piecePos = boardStartingLayout.GetPiecePositionAtIndex(i);
                var pieceName = boardStartingLayout.GetPieceNameAtIndex(i);
                var pieceColor = boardStartingLayout.GetPieceTeamColorAtIndex(i);
                
                var pieceType = Type.GetType(pieceName);
                CreatePieceFromLayout(piecePos, pieceType, pieceColor);
            }
        }

        private void CreatePieceFromLayout(Vector2Int piecePos, Type pieceType, TeamColor pieceColor)
        {
            var piece = _pieceCreator.CreatePiece(pieceType);
        }
    }
}