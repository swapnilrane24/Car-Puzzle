using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance;
        public static LevelManager Instance => instance;

#if UNITY_EDITOR
        [SerializeField] private bool playAssignedLevel;
        [SerializeField] private int assignedLevelIndex;
#endif
        [SerializeField] private LevelData[] levels;

        private int levelIndex;

        public int LevelIndex => levelIndex;
        private LevelData currentLevel;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            levelIndex = PlayerPrefs.GetInt("LevelIndex", 0);
        }

        private void Start()
        {
            int index = levelIndex % levels.Length;

#if UNITY_EDITOR
            if (playAssignedLevel)
            {
                index = assignedLevelIndex % levels.Length;
            }
#endif
            currentLevel = Instantiate(levels[index], transform);
        }

        public void LevelCompleted()
        {
            levelIndex++;
            PlayerPrefs.SetInt("LevelIndex", levelIndex);
        }

        public void SetRewardAmount()
        {
            GameManager.Instance.RoundEarning = currentLevel.RewardAmount;
        }

    }
}