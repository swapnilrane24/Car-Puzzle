using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Curio.Gameplay
{
    [System.Serializable]
    public struct ChallengeItemData
    {
        [SerializeField] private LevelPrefab levelPrefab;
        [SerializeField] private int rewardAmount;
        [SerializeField] private int unlockLevel;
        [SerializeField] private int unlockCost;

        public LevelPrefab LevelPrefab { get => levelPrefab; }
        public int RewardAmount { get => rewardAmount; }
        public int UnlockLevel { get => unlockLevel; }
        public int UnlockCost { get => unlockCost; }
    }

    public class ChallengeManager : MonoBehaviour
    {
        private enum ChallengeToggle { NORMAL, HARD }

        [SerializeField] private TogglePanel togglePanel;
        [SerializeField] private GameObject scrollContainer;
        [SerializeField] private ChallengeItem challengeItemPrefab;
        [SerializeField] private Button normalButton, hardButton;
        [SerializeField] private Button backButton;
        [SerializeField] private ChallengeItemData[] normalChallenges, hardChallenges;

        private List<ChallengeItem> challengeItems = new List<ChallengeItem>();
        private ChallengeToggle challengeToggle = ChallengeToggle.NORMAL;

        private bool initialized = false;

        private void Start()
        {
            normalButton.onClick.AddListener(PopulateNormalChallenges);
            hardButton.onClick.AddListener(PopulateHardChallenges);
            backButton.onClick.AddListener(BackButtonListner);
        }

        public void InitializeChallengePanel()
        {
            normalButton.interactable = false;
            hardButton.interactable = true;

            if (initialized == false)
            {
                for (int i = 0; i < normalChallenges.Length; i++)
                {
                    ChallengeItem challengeItem = Instantiate(challengeItemPrefab, scrollContainer.transform);
                    challengeItems.Add(challengeItem);

                    challengeItem.Initialize(normalChallenges[i].UnlockLevel, i, normalChallenges[i].UnlockCost, normalChallenges[i].RewardAmount);

                }

                initialized = true;
            }
            else
            {
                PopulateNormalChallenges();
            }

            togglePanel.ToggleVisibility(true);
        }

        private void PopulateNormalChallenges()
        {
            normalButton.interactable = false;
            hardButton.interactable = true;

            PopulateChallenge(normalChallenges);
        }

        private void PopulateHardChallenges()
        {
            normalButton.interactable = true;
            hardButton.interactable = false;

            PopulateChallenge(hardChallenges);
        }

        private void PopulateChallenge(ChallengeItemData[] currentChallengeItems)
        {
            int extraItemCount = currentChallengeItems.Length - challengeItems.Count;

            if (extraItemCount <= 0)
            {
                for (int i = 0; i < challengeItems.Count; i++)
                {
                    if (i < currentChallengeItems.Length)
                    {
                        challengeItems[i].Initialize(currentChallengeItems[i].UnlockLevel, i, currentChallengeItems[i].UnlockCost, currentChallengeItems[i].RewardAmount);
                        challengeItems[i].Activate(true);
                    }
                    else
                    {
                        challengeItems[i].Activate(false);
                    }
                }
            }
            else
            {
                int lastIndex = 0;
                for (int i = 0; i < challengeItems.Count; i++)
                {
                    challengeItems[i].Initialize(currentChallengeItems[i].UnlockLevel, i, currentChallengeItems[i].UnlockCost, currentChallengeItems[i].RewardAmount);
                    challengeItems[i].Activate(true);
                    lastIndex = i;
                }

                for (int i = 0; i < extraItemCount; i++)
                {
                    ChallengeItem challengeItem = Instantiate(challengeItemPrefab, scrollContainer.transform);
                    challengeItems.Add(challengeItem);

                    challengeItem.Initialize(currentChallengeItems[lastIndex + i].UnlockLevel, lastIndex + i,
                        currentChallengeItems[lastIndex + i].UnlockCost, currentChallengeItems[lastIndex + i].RewardAmount);
                    challengeItem.Activate(true);
                }
            }
        }

        private void BackButtonListner()
        {
            togglePanel.ToggleVisibility(false);
        }
    }
}