using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace masterFeature
{
    public class MusicEngine : MonoBehaviour
    {
        public static MusicEngine Instance { get; private set; }

        [SerializeField]
        private AudioMixer mixer;

        [SerializeField]
        private AudioSource music1_AudioSource;
        [SerializeField]
        private AudioSource music2_AudioSource;
        [SerializeField]
        private AudioSource sfx_AudioSource;
        [SerializeField]
        private AudioSource ambient_AudioSource;


        [Header("Music Clip")]
        [SerializeField]
        private AudioClip menuMusic;
        [SerializeField]
        private AudioClip gameCalmMusic;
        [SerializeField]
        private AudioClip gameActionMusic;
        [SerializeField]
        private AudioClip gameCombatMusic;

        [Header("Global SFX Clip")]
        [SerializeField]
        private AudioClip buttonOk;
        [SerializeField]
        private AudioClip buttonBack;
        [SerializeField]
        private AudioClip buttonClick;
        [SerializeField]
        private AudioClip playerJump;
        [SerializeField]
        private AudioClip hookEject;
        [SerializeField]
        private AudioClip hookHit;





        private FadeType music1_fadeType;
        private FadeType music2_fadeType;

        private float music1_initialVolume;
        private float music2_initialVolume;

        private float music1_CurrentVolume;
        private float music2_CurrentVolume;

        private float music1_targetValue;
        private float music2_targetValue;


        private float music1_timePassed_Fade = 0;
        private float music2_timePassed_Fade = 0;

        private float fadeTime = 3.5f;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }

            mixer.GetFloat("music1Vol", out music1_initialVolume);
            music1_initialVolume = Mathf.Pow(10, music1_initialVolume / 20);

            mixer.GetFloat("music2Vol", out music2_initialVolume);
            music2_initialVolume = Mathf.Pow(10, music2_initialVolume / 20);
        }

        void Start()
        {
            music1_fadeType = FadeType.None;
            music1_fadeType = FadeType.None;

            music1_CurrentVolume = music1_initialVolume;

            music2_CurrentVolume = music2_initialVolume;
        }


        void Update()
        {
            switch (music1_fadeType)
            {
                case FadeType.Fadein_Music:

                    music1_timePassed_Fade += Time.deltaTime;
                    if (music1_timePassed_Fade >= fadeTime)
                    {
                        music1_fadeType = FadeType.None;
                        music1_CurrentVolume = music1_initialVolume;
                        mixer.SetFloat("music1Vol", music1_initialVolume);
                        music1_timePassed_Fade = 0;

                    }
                    else
                    {
                        float newVol = Mathf.Lerp(music1_CurrentVolume, music1_targetValue, music1_timePassed_Fade / fadeTime);
                        if (newVol >= music1_initialVolume)
                        {
                            newVol = music1_initialVolume;
                        }
                        mixer.SetFloat("music1Vol", Mathf.Log10(newVol) * 20);
                    }

                    break;

                case FadeType.Fadeout_Music:

                    music1_timePassed_Fade += Time.deltaTime;
                    if (music1_timePassed_Fade >= fadeTime)
                    {
                        music1_AudioSource.Stop();
                        ResetAudioSource_Music1();
                    }
                    else
                    {
                        float newVol = Mathf.Lerp(music1_CurrentVolume, music1_targetValue, music1_timePassed_Fade / fadeTime);
                        mixer.SetFloat("music1Vol", Mathf.Log10(newVol) * 20);
                    }

                    break;
            }

            switch (music2_fadeType)
            {
                case FadeType.Fadein_Music:

                    music2_timePassed_Fade += Time.deltaTime;
                    if (music2_timePassed_Fade >= fadeTime)
                    {
                        music2_fadeType = FadeType.None;
                        music2_CurrentVolume = music2_initialVolume;
                        mixer.SetFloat("music2Vol", music2_initialVolume);
                        music2_timePassed_Fade = 0;

                    }
                    else
                    {
                        float newVol = Mathf.Lerp(music2_CurrentVolume, music2_targetValue, music2_timePassed_Fade / fadeTime);
                        if (newVol >= music2_initialVolume)
                        {
                            newVol = music2_initialVolume;
                        }
                        mixer.SetFloat("music2Vol", Mathf.Log10(newVol) * 20);
                    }

                    break;

                case FadeType.Fadeout_Music:

                    music2_timePassed_Fade += Time.deltaTime;
                    if (music2_timePassed_Fade >= fadeTime)
                    {
                        music2_AudioSource.Stop();
                        ResetAudioSource_Music2();
                    }
                    else
                    {
                        float newVol = Mathf.Lerp(music2_CurrentVolume, music2_targetValue, music2_timePassed_Fade / fadeTime);
                        mixer.SetFloat("music2Vol", Mathf.Log10(newVol) * 20);
                    }

                    break;
            }

        }


        public void Play_MusicSituation(Music_Situation music_Situation)
        {
            switch (music_Situation)
            {
                case Music_Situation.Start_MenuScene:

                    ResetAudioSource_Music1();
                    music1_AudioSource.clip = menuMusic;
                    music1_AudioSource.Play();

                    break;

                case Music_Situation.End_MenuScene:

                    StartFadeout_Music1();

                    break;

                case Music_Situation.Start_LevelScene:

                    ResetAudioSource_Music1();
                    music1_AudioSource.clip = gameCalmMusic;
                    music1_AudioSource.Play();

                    break;

                case Music_Situation.ActionZoneTrigger:

                    StartFadeout_Music1();
                    StartFadein_Music2();

                    break;

                case Music_Situation.CombatZoneTrigger:

                    StartFadeout_Music2();
                    StartFadein_CombatMusic1();

                    break;

                case Music_Situation.LevelFinished:

                    if (music1_AudioSource.isPlaying)
                    {
                        StartFadeout_Music1();
                    }
                    if (music2_AudioSource.isPlaying)
                    {
                        StartFadeout_Music2();
                    }

                    break;

                default:
                    break;
            }
        }

        public void PlaySFX(AudioClip clip)
        {
            sfx_AudioSource.clip = clip;
            sfx_AudioSource.Play();
        }

        public void PlaySFX(SFXType sfxType)
        {
            switch (sfxType)
            {
                case SFXType.buttonOk:
                    sfx_AudioSource.clip = buttonOk;
                    sfx_AudioSource.Play();
                    break;
                case SFXType.ButtonBack:
                    sfx_AudioSource.clip = buttonBack;
                    sfx_AudioSource.Play();
                    break;
                case SFXType.buttonClick:
                    sfx_AudioSource.clip = buttonClick;
                    sfx_AudioSource.Play();
                    break;
                case SFXType.HookEject:
                    sfx_AudioSource.clip = hookEject;
                    sfx_AudioSource.Play();
                    break;
                case SFXType.HookHit:
                    sfx_AudioSource.clip = hookHit;
                    sfx_AudioSource.Play();
                    break;
                case SFXType.jumpPlayer:
                    sfx_AudioSource.clip = playerJump;
                    sfx_AudioSource.Play();
                    break;
                default:
                    break;
            }

        }




        private void StartFadein_CombatMusic1()
        {
            music1_timePassed_Fade = 0;
            music1_CurrentVolume = 0;
            mixer.SetFloat("music1Vol", -80);
            //music1_CurrentVolume = Mathf.Pow(10, music1_CurrentVolume / 20);
            music1_targetValue = Mathf.Clamp(music1_initialVolume, 0.0001f, 1);
            music1_AudioSource.clip = gameCombatMusic;
            music1_AudioSource.Play();

            music1_fadeType = FadeType.Fadein_Music;
        }

        private void StartFadein_Music2()
        {
            music2_timePassed_Fade = 0;
            music2_CurrentVolume = 0;
            mixer.SetFloat("music2Vol", -80);
            //music2_CurrentVolume = Mathf.Pow(10, music2_CurrentVolume / 20);
            music2_targetValue = Mathf.Clamp(music2_initialVolume, 0.0001f, 1);
            music2_AudioSource.clip = gameActionMusic;
            music2_AudioSource.Play();

            music2_fadeType = FadeType.Fadein_Music;
        }

        private void StartFadeout_Music1()
        {
            music1_timePassed_Fade = 0;
            mixer.GetFloat("music1Vol", out music1_CurrentVolume);
            music1_CurrentVolume = Mathf.Pow(10, music1_CurrentVolume / 20);
            music1_targetValue = Mathf.Clamp(0, 0.0001f, 1);

            music1_fadeType = FadeType.Fadeout_Music;
        }

        private void StartFadeout_Music2()
        {
            music2_timePassed_Fade = 0;
            mixer.GetFloat("music2Vol", out music2_CurrentVolume);
            music2_CurrentVolume = Mathf.Pow(10, music2_CurrentVolume / 20);
            music2_targetValue = Mathf.Clamp(0, 0.0001f, 1);

            music2_fadeType = FadeType.Fadeout_Music;
        }

        private void ResetAudioSource_Music1()
        {
            music1_fadeType = FadeType.None;
            mixer.SetFloat("music1Vol", music1_initialVolume);
            music1_timePassed_Fade = 0;
            music1_CurrentVolume = music1_initialVolume;
        }

        private void ResetAudioSource_Music2()
        {
            music2_fadeType = FadeType.None;
            mixer.SetFloat("music2Vol", music2_initialVolume);
            music2_timePassed_Fade = 0;
            music2_CurrentVolume = music2_initialVolume;
        }


        private enum FadeType
        {
            None,
            Fadeout_Music,
            Fadein_Music,
        }

        public enum Music_Situation
        {
            Start_MenuScene,
            End_MenuScene,
            Start_LevelScene,
            ActionZoneTrigger,
            CombatZoneTrigger,
            LevelFinished
        }

        public enum SFXType
        {
            buttonOk,
            ButtonBack,
            buttonClick,
            jumpPlayer,
            HookEject,
            HookHit,
        }
    }
}
