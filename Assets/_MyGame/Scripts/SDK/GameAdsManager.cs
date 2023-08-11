using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CrazyGames;
using TMPro;
using ScriptableObjectArchitecture;
using DG.Tweening;

public class GameAdsManager : MonoBehaviour
{
    private static GameAdsManager instance;

    #region Serialized Variables
    [SerializeField] private TextMeshProUGUI noAdsText;
    [SerializeField] private float interstitialInterval;
    [SerializeField] private bool disableAds;
    [SerializeField] private float startRVDelay;

    public float StartRVDelay => startRVDelay;
    #endregion

    #region Private Variables
    [SerializeField] private float _delayInterstitial;
    private bool rewardSuccess = false, midAdSuccess = false;
    private Action<bool> rewardCallback;
    private Action midAdCallback;
    #endregion

    #region Getters & Setters
    public bool AdsDisabled => disableAds;
    public static GameAdsManager Instance { get => instance; }
    #endregion

    #region Unity Methods
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

        _delayInterstitial = interstitialInterval;
    }

    private void Update()
    {
        if (disableAds == false)
        {
            if (_delayInterstitial > 0)
            {
                _delayInterstitial -= Time.deltaTime;
            }

            if (startRVDelay > 0)
            {
                startRVDelay -= Time.deltaTime;
            }
        }
    }
    #endregion

    #region Private Methods
    private void NoAdsPopUp()
    {
        noAdsText.transform.localScale = Vector3.zero;
        noAdsText.gameObject.SetActive(true);
        Sequence noAdSequence = DOTween.Sequence();
        noAdSequence.Append(noAdsText.transform.DOScale(Vector3.one, 0.2f));
        noAdSequence.AppendInterval(0.5f);
        noAdSequence.Append(noAdsText.transform.DOScale(Vector3.zero, 0.2f));
    }

    public void AdError()
    {
        NoAdsPopUp();
    }

    public void AdFinished()
    {

    }

    private void SetAudioTime(float value)
    {
        AudioListener.volume = value;
        Time.timeScale = value;
    }

    private void RewardSuccess()
    {
        SetAudioTime(1);
        rewardSuccess = true;
        rewardCallback(rewardSuccess);
    }

    private void Rewardfailed()
    {
        NoAdsPopUp();
        SetAudioTime(1);
        rewardSuccess = false;
        rewardCallback(rewardSuccess);
    }

    private void MidAdSuccess()
    {
        SetAudioTime(1);
        midAdSuccess = true;
        midAdCallback();
        _delayInterstitial = interstitialInterval;
    }

    private void MidAdfailed()
    {
        NoAdsPopUp();
        SetAudioTime(1);
        midAdSuccess = false;
        midAdCallback();
        _delayInterstitial = 30;
    }
    #endregion

    #region Public Methods
    public bool ShowRewardedAds(Action<bool> callback)
    {
        rewardCallback = callback;
        SetAudioTime(0);
        rewardSuccess = false;
        CrazyAds.Instance.beginAdBreakRewarded(RewardSuccess, Rewardfailed);
        return rewardSuccess;
    }

    public void ShowNormalAd(Action callback)
    {
        if (_delayInterstitial <= 0)
        {
            midAdCallback = callback;
            SetAudioTime(0);
            CrazyAds.Instance.beginAdBreak(MidAdSuccess, MidAdfailed);
        }
        else
        {
            callback();
        }
    }
    #endregion

    public bool InternetAvailable()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable) return false;
        return true;
    }
}