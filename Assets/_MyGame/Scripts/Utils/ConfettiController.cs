using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class ConfettiController : MonoBehaviour
    {
        private static ConfettiController instance;
        public static ConfettiController Instance => instance;

        [SerializeField] private ParticleSystem fx;
        //[SerializeField] private SoundType soundType;
        [SerializeField] private ParticleSystem[] bigCelebrationFx;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void PlayFX()
        {
            fx.Play();

            //if (SoundManager.Instance != null)
            //    SoundManager.Instance.Play(soundType);
        }

        public void PlayFx(Transform objTransform)
        {
            transform.position = objTransform.position;
            fx.Play();

            //if (SoundManager.Instance != null)
            //    SoundManager.Instance.Play(soundType);
        }

        public void PlayBigCelebrationFX(Vector3 position)
        {
            transform.position = position;
            for (int i = 0; i < bigCelebrationFx.Length; i++)
            {
                bigCelebrationFx[i].Play();
            }

            //if (SoundManager.Instance != null)
            //    SoundManager.Instance.Play(soundType);
        }
    }
}