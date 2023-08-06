using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Curio.Gameplay
{
    public class LevelCompletePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private TogglePanel togglePanel;

        public void Initialize()
        {
            rewardText.text = "" + GameManager.Instance.RoundEarning;
            togglePanel.ToggleVisibility(true);
        }

    }
}