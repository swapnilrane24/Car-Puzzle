using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Curio.Gameplay
{
    public static class WaitExtension
    {
        #region Private Methods
        private static IEnumerator ExecuteAction(float delay, UnityAction callBack)
        {
            yield return new WaitForSecondsRealtime(delay);
            callBack?.Invoke();
            yield break;
        }

        private static IEnumerator ExecuteAction(UnityAction callBack)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke();
            yield break;
        }
        #endregion

        #region Public Methods
        public static void Wait(this MonoBehaviour mono, float delay, UnityAction callBack)
        {
            mono.StartCoroutine(ExecuteAction(delay, callBack));
        }

        public static void WaitForFrame(this MonoBehaviour mono, UnityAction callBack)
        {
            mono.StartCoroutine(ExecuteAction(callBack));
        }
        #endregion
    }
}