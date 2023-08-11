using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Curio.Gameplay
{
    public class SplashScene : MonoBehaviour
    {
        #region Serialized Variables
        [SerializeField] private Image fillImg;
        [SerializeField] private int gameSceneIndex;
        #endregion

        #region Private Variables
        float loadProgress;
        #endregion

        #region Getters & Setters
        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(FrameDelay());
        }
        #endregion

        #region Private Methods
        IEnumerator FrameDelay()
        {
            yield return new WaitForEndOfFrame();

            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(gameSceneIndex);

            while (!loadingOperation.isDone)
            {
                loadProgress = Mathf.Clamp01(loadingOperation.progress / .9f);
                fillImg.fillAmount = loadProgress;
                yield return null;
            }

        }
        #endregion

        #region Public Methods
        #endregion
    }
}