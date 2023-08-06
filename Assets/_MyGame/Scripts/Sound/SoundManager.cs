using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Curio.Gameplay
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField] private SoundType soundType;
        [SerializeField] private AudioClip clip;
        [Range(0f, 1f)][SerializeField] private float volume = 0.5f;
        [Range(0.1f, 3.0f)][SerializeField] private float pitch = 1f;

        public AudioClip Clip { get => clip; }
        public float Volume { get => volume; }
        public float Pitch { get => pitch; }
        public SoundType SoundType { get => soundType; }
    }

    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;

        [SerializeField] private AudioClip mainMusic;
        [Range(0f, 1f)][SerializeField] private float musicVolume;
        [SerializeField] private Sound[] pickUpSounds, boosterSounds, extraSounds, uiSounds;

        [SerializeField] private AudioSource pickUpAudioSource, boosterAudioSource, extraAudioSource, uiAudioSource, musicSource;

        public static SoundManager Instance { get => instance; }

        private bool soundOn;

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
        }

        private void Start()
        {
            if (mainMusic)
            {
                musicSource.clip = mainMusic;
                musicSource.volume = musicVolume;
                musicSource.loop = true;
                musicSource.Play();
            }

            //SoundOn(true, true);
        }

        public void SoundOn(bool value, bool effectAudioSource)
        {
            soundOn = value;

            //if (effectAudioSource)
                AudioListener.volume = soundOn == true ? 1 : 0;
        }

        public void ActivateAudio()
        {
            AudioListener.volume = soundOn == true ? 1 : 0;
        }

        public void Play(SoundType soundType)
        {
            if (soundOn == false) return;

            switch (soundType.SoundCategory)
            {
                case SoundCategory.None:
                    break;
                case SoundCategory.Extra:
                    for (int i = 0; i < extraSounds.Length; i++)
                    {
                        if (extraSounds[i].SoundType == soundType)
                        {
                            extraAudioSource.clip = extraSounds[i].Clip;
                            extraAudioSource.volume = extraSounds[i].Volume;
                            extraAudioSource.pitch = extraSounds[i].Pitch;

                            extraAudioSource.Play();
                        }
                    }
                    break;
                case SoundCategory.PickUp:
                    for (int i = 0; i < pickUpSounds.Length; i++)
                    {
                        if (pickUpSounds[i].SoundType == soundType)
                        {
                            pickUpAudioSource.clip = pickUpSounds[i].Clip;
                            pickUpAudioSource.volume = pickUpSounds[i].Volume;
                            pickUpAudioSource.pitch = pickUpSounds[i].Pitch;

                            pickUpAudioSource.Play();
                        }
                    }
                    break;
                case SoundCategory.PowerUps:
                    for (int i = 0; i < boosterSounds.Length; i++)
                    {
                        if (boosterSounds[i].SoundType == soundType)
                        {
                            boosterAudioSource.clip = boosterSounds[i].Clip;
                            boosterAudioSource.volume = boosterSounds[i].Volume;
                            boosterAudioSource.pitch = boosterSounds[i].Pitch;

                            boosterAudioSource.Play();
                        }
                    }
                    break;
                case SoundCategory.UI:
                    for (int i = 0; i < uiSounds.Length; i++)
                    {
                        if (uiSounds[i].SoundType == soundType)
                        {
                            uiAudioSource.clip = uiSounds[i].Clip;
                            uiAudioSource.volume = uiSounds[i].Volume;
                            uiAudioSource.pitch = uiSounds[i].Pitch;

                            uiAudioSource.Play();
                        }
                    }
                    break;
            }
        }
    }
}