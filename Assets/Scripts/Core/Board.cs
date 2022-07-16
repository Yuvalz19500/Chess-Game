using UnityEngine;

namespace Core
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private Transform bottomLeftSquare;
        [SerializeField] private float squareSize = 1.5f;

        public Vector3 CalculateBoardPositionFromSquarePosition(Vector2Int squarePos)
        {
            return bottomLeftSquare.position + new Vector3(squarePos.x * squareSize, 0f, squarePos.y * squareSize);
        }
    }
}