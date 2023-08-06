using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

namespace Curio.Gameplay
{
    public enum GameState
    {
        PLAYING,
        WIN,
        LOSE
    }

    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance => instance;

        [SerializeField] private IntVariable totalMoney;

        public IntVariable TotalMoney => totalMoney;
        private int roundEarning;

        public int RoundEarning { get => roundEarning; set => roundEarning = value; }

        private GameState gameState = GameState.PLAYING;

        public GameState GameState { get => gameState; set => gameState = value; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            totalMoney.Value = ES3.Load<int>("TotalMoney", 0);
        }

        public void AddMoney(int value)
        {
            totalMoney.Value += value;
            ES3.Save<int>("TotalMoney", totalMoney.Value);
        }

        public void ReduceMoney(int value)
        {
            totalMoney.Value -= value;
            if (totalMoney.Value < 0)
                totalMoney.Value = 0;

            ES3.Save<int>("TotalMoney", totalMoney.Value);
        }






    }
}