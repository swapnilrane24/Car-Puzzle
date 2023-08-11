using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ScriptableObjectArchitecture;

namespace Curio.Gameplay
{
    public class GamePanel : MonoBehaviour
    {
        private static GamePanel instance;
        public static GamePanel Instance => instance;

        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button challengeButton;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI moveText;
        [SerializeField] private TextMeshProUGUI totalMoneytext;
        [SerializeField] private GameObject moveHolder;

        [SerializeField] private LevelCompletePanel levelCompletePanel;
        [SerializeField] private LevelFailedPanel levelFailedPanel;
        [SerializeField] private ChallengeManager challengeManager;
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private ShopPanel shopPanel;

        [SerializeField] private SoundType levelWinSound;

        private int _moveCount = 0;
        private bool unlimitedMoves = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            settingsBtn.onClick.AddListener(SettingsButtonListner);
            retryButton.onClick.AddListener(RetryButtonListner);
            shopButton.onClick.AddListener(ShopButtonListner);
            challengeButton.onClick.AddListener(ChallengeButtonListner);
            levelText.text = "Level " + (LevelManager.instance.LevelIndex + 1);
            totalMoneytext.text = CurrencyToString.Convert(GameManager.Instance.TotalMoney.Value);
            GameManager.Instance.TotalMoney.AddListener(UpdateTotalMoney);
        }

        private void SettingsButtonListner()
        {
            settingsPanel.ActivatePanel();
        }

        private void RetryButtonListner()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ShopButtonListner()
        {
            shopPanel.InitializePanel();
        }

        private void ChallengeButtonListner()
        {
            challengeManager.InitializeChallengePanel();
        }

        private void UpdateTotalMoney()
        {
            totalMoneytext.text = CurrencyToString.Convert(GameManager.Instance.TotalMoney.Value);
        }

        public void SetMoveValue(int moveCount)
        {
            moveHolder.SetActive(false);
            _moveCount = moveCount;
            if (_moveCount > 0)
            {
                moveHolder.SetActive(true);
                moveText.text = "Move " + _moveCount;
            }
            else
            {
                unlimitedMoves = true;
            }
        }

        public void ReduceMoveCount(int amount)
        {
            if (unlimitedMoves == false)
            {
                _moveCount -= amount;
                if (_moveCount <= 0)
                {
                    _moveCount = 0;
                    LevelFailed();
                }
                moveText.text = "Move " + _moveCount;
            }
        }

        public void IncreaseMoveCount(int amount)
        {
            _moveCount += amount;
            moveText.text = "Move " + _moveCount;
        }

        public void LevelCompleted()
        {
            SoundManager.Instance.Play(levelWinSound);
            LevelManager.instance.SetRewardAmount();
            GameManager.Instance.GameState = GameState.WIN;
            ConfettiController.Instance.PlayBigCelebrationFX(Vector3.zero);
            WaitExtension.Wait(this, 0.3f, () => levelCompletePanel.Initialize());
        }

        public void LevelFailed()
        {
            GameManager.Instance.GameState = GameState.LOSE;
            WaitExtension.Wait(this, 0.3f, () => levelFailedPanel.Initialize());
        }



    }
}