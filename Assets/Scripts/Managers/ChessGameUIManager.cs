using UnityEngine;
using Utils;

namespace Managers
{
    public class ChessGameUIManager : Singleton<ChessGameUIManager>
    {
        [SerializeField] private GameObject promotionCanvas;

        private bool _isWaitingForPromotionInput = false;

        public void TogglePromotionUI(bool toggle)
        {
            promotionCanvas.SetActive(toggle);
            _isWaitingForPromotionInput = toggle;
        }
    }
}
