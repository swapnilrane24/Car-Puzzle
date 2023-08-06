using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Curio.Gameplay
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private TogglePanel togglePanel;
        [SerializeField] private Button backButton;

        private void Start()
        {
            backButton.onClick.AddListener(() => togglePanel.ToggleVisibility(false));
        }

        public void ActivatePanel()
        {
            togglePanel.ToggleVisibility(true);
        }


    }
}