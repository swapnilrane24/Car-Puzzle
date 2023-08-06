using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Curio.Gameplay
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private TogglePanel togglePanel;
        [SerializeField] private Button backButton;

        private void Start()
        {
            backButton.onClick.AddListener(BackButtonListner);
        }

        public void InitializePanel()
        {
            togglePanel.ToggleVisibility(true);
        }

        private void BackButtonListner()
        {
            togglePanel.ToggleVisibility(false);
        }

    }
}