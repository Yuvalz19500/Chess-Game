using System;
using System.Collections.Generic;
using Enums;
using Pieces;
using UnityEngine;

namespace Core
{
    public class PieceCreator : MonoBehaviour
    {
        [SerializeField] private GameObject[] piecesPrefabs;
        [SerializeField] private Material blackMaterial;
        [SerializeField] private Material whiteMaterial;

        private Dictionary<string, GameObject> _piecesDict;

        private void Awake()
        {
            foreach (var piece in piecesPrefabs)
            {
                _piecesDict.Add(piece.GetComponent<Piece>().GetType().ToString(), piece);
            }
        }

        public Material GetTeamMaterial(TeamColor team)
        {
            return team == TeamColor.Black ? blackMaterial : whiteMaterial;
        }

        public GameObject CreatePiece(Type pieceType)
        {
            var prefab = _piecesDict[pieceType.ToString()];
            return prefab ? Instantiate(prefab) : null;
        }
    }
}