using System;
using Enums;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PromotionPanel : MonoBehaviour
    {
        private UIDocument _document;
        
        public static event Action<PieceType> OnPlayerPickPromotion; 

        public void TogglePromotionPanel(bool toggle)
        {
            gameObject.SetActive(toggle);
        }
    }
}
