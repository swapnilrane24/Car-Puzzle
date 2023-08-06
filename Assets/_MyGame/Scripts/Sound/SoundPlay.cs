using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Curio.Gameplay
{
    public class SoundPlay : MonoBehaviour
    {
        [SerializeField] private bool playOnActive;
        [SerializeField] private bool playOnDeactive;
        [SerializeField] private SoundType soundType;

        private void OnEnable()
        {
            if (playOnActive)
                Play();
        }

        private void OnDisable()
        {
            if (playOnDeactive)
                Play();
        }

        public void Play()
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.Play(soundType);
        }
    }
}