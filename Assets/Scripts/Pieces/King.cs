using UnityEngine;

namespace Pieces
{
    public class King : Piece
    {
        public override void SetAvailableMoves()
        {
            MovesDict.Clear();
        }
    }
}