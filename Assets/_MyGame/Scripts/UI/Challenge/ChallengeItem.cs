using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Curio.Gameplay
{
    public enum ChallengeItemStatus
    {
        LOCK,
        UNLOCKED,
        PURCHASED,
        ISPLAYED
    }

    public class ChallengeItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI indexText;
        [SerializeField] private GameObject unlockHolder;
        [SerializeField] private GameObject lockHolder;

        [SerializeField] private TextMeshProUGUI lockInfoText;

        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private Button playButton;
        [SerializeField] private Button playWithLifeButton;
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI costText;


        [SerializeField] private GameObject rewardInfoHolder;
        [SerializeField] private GameObject unlockInfoHolder;

        [SerializeField] private ChallengeItemStatus _challengeItemStatus = ChallengeItemStatus.LOCK;

        private int _index;
        private bool winStatus = false;
        private int _unlockLevel;
        private int _unlockCost;
        private int _reward;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayButtonListner);
            buyButton.onClick.AddListener(BuyButtonListner);
            playWithLifeButton.onClick.AddListener(PlayWithLifeButtonListner);
        }

        public void Initialize(int unlockLevel, int index, int unlockCost, int reward)
        {
            gameObject.SetActive(true);
            indexText.text = "" + (index + 1);
            _reward = reward;
            _unlockLevel = unlockLevel;
            _unlockCost = unlockCost;
            _index = index;
            rewardText.text = "" + _reward;
            switch (_challengeItemStatus)
            {
                case ChallengeItemStatus.LOCK:
                    lockHolder.SetActive(true);
                    unlockHolder.SetActive(false);
                    lockInfoText.text = "Reach \n Level " + _unlockLevel;
                    break;
                case ChallengeItemStatus.UNLOCKED:
                    unlockInfoHolder.SetActive(true);
                    rewardInfoHolder.SetActive(false);
                    buyButton.gameObject.SetActive(true);
                    playButton.gameObject.SetActive(false);
                    playWithLifeButton.gameObject.SetActive(false);
                    costText.text = "" + _unlockCost;
                    break;
                case ChallengeItemStatus.PURCHASED:
                    unlockInfoHolder.SetActive(false);
                    rewardInfoHolder.SetActive(true);
                    buyButton.gameObject.SetActive(false);
                    playButton.gameObject.SetActive(true);
                    playWithLifeButton.gameObject.SetActive(false);
                    break;
                case ChallengeItemStatus.ISPLAYED:
                    unlockInfoHolder.SetActive(false);
                    rewardInfoHolder.SetActive(true);
                    buyButton.gameObject.SetActive(false);
                    playButton.gameObject.SetActive(false);
                    playWithLifeButton.gameObject.SetActive(true);
                    break;
            }


        }

        private void PlayButtonListner()
        {
            //play the level
        }

        private void BuyButtonListner()
        {
            unlockInfoHolder.SetActive(false);
            rewardInfoHolder.SetActive(true);
            buyButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }

        private void PlayWithLifeButtonListner()
        {
            //reduce 1 life and play the level.
        }

        public void Activate(bool value)
        {
            gameObject.SetActive(value);
        }










    }
}