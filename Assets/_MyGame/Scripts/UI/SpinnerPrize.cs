using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

namespace Curio.Gameplay
{
    public class SpinnerPrize : MonoBehaviour
    {
        [SerializeField] private SoundType coinCollectSound;
        [SerializeField] private Animator spinnerAnim;
        [SerializeField] private TogglePanel levelCompletePanel;
        [SerializeField] private TextMeshProUGUI adsRewardText;
        [SerializeField] private TextMeshProUGUI rewardMultiplierText;
        [SerializeField] private Button rewardAdButton, noThanksButton;
        [SerializeField] private RectTransform mainCoinIcon;
        [SerializeField] private RectTransform[] coins;

        private int multiplier;
        private int totalRewardEarning;

        private void Start()
        {
            rewardAdButton.onClick.AddListener(RewardButtonListner);
            noThanksButton.onClick.AddListener(NoThanksButtonListner);
        }

        //method called by animation event
        public void MultiplierEventListner(int value)
        {
            rewardMultiplierText.text = "Get " + value + "x";
            multiplier = value;
            totalRewardEarning = multiplier * GameManager.Instance.RoundEarning;
            adsRewardText.text = "" + CurrencyToString.Convert((multiplier * GameManager.Instance.RoundEarning));
        }

        private void RewardCallBack()
        {
            GameManager.Instance.AddMoney(totalRewardEarning);
        }

        private void RewardButtonListner()
        {
            spinnerAnim.enabled = false;
            rewardAdButton.interactable = false;
        }

        private void NoThanksButtonListner()
        {
            //pay coins
            Coins_Animation();
            spinnerAnim.enabled = false;
            rewardAdButton.interactable = false;
            noThanksButton.interactable = false;
        }

        public void Coins_Animation()
        {
            SoundManager.Instance.Play(coinCollectSound);
            //play vibration
            int coinAnimationDoneCount = 0; //used just to make sure scene is loaded only on last coin animation
            for (int i = 0; i < coins.Length; i++)
            {
                float t = i / (float)coins.Length;

                float x_pos = Mathf.Cos(t * 360f * Mathf.Deg2Rad);
                float y_pos = Mathf.Sin(t * 360f * Mathf.Deg2Rad);

                Vector2 pos = new Vector2(x_pos, y_pos) * Random.Range(50f, 150f);
                pos += coins[i].anchoredPosition;

                coins[i].gameObject.SetActive(true);
                Sequence seq = DOTween.Sequence();

                seq.Append(coins[i].DOAnchorPos(pos, Random.Range(0.5f, 1f)).SetEase(Ease.OutBack))
                .Append(coins[i].DOLocalMove(mainCoinIcon.transform.localPosition, Random.Range(.5f, 1f)).SetEase(Ease.OutBack))
                .OnComplete(() => {
                    coinAnimationDoneCount++;
                    
                    if (coinAnimationDoneCount >= coins.Length)
                    {
                        GameManager.Instance.AddMoney(GameManager.Instance.RoundEarning);
                        LevelManager.instance.LevelCompleted();
                        GameManager.Instance.GameState = GameState.PLAYING;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                });

            }
        }






    }
}