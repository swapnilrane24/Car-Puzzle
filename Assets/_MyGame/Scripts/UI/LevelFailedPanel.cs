using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Curio.Gameplay
{
    public class LevelFailedPanel : MonoBehaviour
    {
        [SerializeField] private TogglePanel togglePanel;
        [SerializeField] private Button reviveAdsButton;
        [SerializeField] private Button noThanksButton;

        private void Awake()
        {
            reviveAdsButton.onClick.AddListener(ReviveAdsButton);
            noThanksButton.onClick.AddListener(NoThanksButton);
        }

        public void Initialize()
        {
            togglePanel.ToggleVisibility(true);
        }

        private void NoThanksButton()
        {
            GameAdsManager.Instance.ShowNormalAd(() =>
            {
                GameManager.Instance.GameState = GameState.PLAYING;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
            
        }

        private void ReviveAdsButton()
        {

        }

    }
}