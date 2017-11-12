using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Managers
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Singleton;

        AudioSource[] AudioSources;

        List<AudioClip> SFXClips = new List<AudioClip>();
        List<AudioClip> MusicClips = new List<AudioClip>();


        public enum AUDIO_SOURCE_ID
        {
            MUSIC_SOURCE,
            SFX_SOURCE,
            SFX_SHORT_DUR,
            SFX_LONGISH_DUR,
            SFX_ALWAYS_ON,
            DROP_IN_SOURCE,
            MAX_SOURCES
        };

        public enum MUSIC_ID
        {
            INTRO_LOOP,
            MAIN_LOOP,
            RAVE_MUSIC,
            NUM_LOOPS
        };

        public enum SFX_ID
        {
            GATE_RAISE,
            MIRROR_HIT,
            MIRROR_MOVE,
            NUM_SOUNDS
        };

        public enum SFX_SHORT_DUR_ID
        {
            GATE_RAISE,
            MIRROR_HIT,
            MIRROR_MOVE,
            LEVER_SWITCH,
            TIME_SPEED_UP,
            TIME_SLOW_DOWN,
            COGSWORTH_MOVING,
            EXPLOSION,
            STONE_DOOR_MOVE,
            NUM_SOUNDS
        };

        public enum SFK_LONGISH_DUR_ID
        {
            BOOING_CROWD,
            CLAPPING_CROWD,
            GAME_START,
            LEVEL_START,
            MAGIC_PORTAL_SOUND,
            NUM_SOUNDS
        };

        public enum SFX_ALWAYS_ON_ID
        {
            LASER_LOOP,
            NUM_SOUNDS
        };

        Dictionary<MUSIC_ID, AudioClip> musicClipDict = new Dictionary<MUSIC_ID, AudioClip>();
        Dictionary<SFX_ID, AudioClip> sfxClipDict = new Dictionary<SFX_ID, AudioClip>();
        Dictionary<SFX_SHORT_DUR_ID, AudioClip> sfxShortClipDict = new Dictionary<SFX_SHORT_DUR_ID, AudioClip>();
        Dictionary<SFK_LONGISH_DUR_ID, AudioClip> sfxLongClipDict = new Dictionary<SFK_LONGISH_DUR_ID, AudioClip>();
        Dictionary<SFX_ALWAYS_ON_ID, AudioClip> sfxAlwaysOnClipDict = new Dictionary<SFX_ALWAYS_ON_ID, AudioClip>();


        // Use this for initialization
        void Start()
        {
			DontDestroyOnLoad (gameObject);

            if (!Singleton)
            {
                Singleton = this;
                AudioSources = gameObject.GetComponentsInChildren<AudioSource>();
                Assert.AreNotEqual(0, AudioSources.Length);

                musicClipDict[MUSIC_ID.INTRO_LOOP] = Resources.Load("Audio/Custom/StartingGameMusic", typeof(AudioClip)) as AudioClip;
                musicClipDict[MUSIC_ID.MAIN_LOOP] = Resources.Load("Audio/Custom/GreatThings", typeof(AudioClip)) as AudioClip;
                musicClipDict[MUSIC_ID.RAVE_MUSIC] = Resources.Load("Audio/Custom/TheGrayEdited", typeof(AudioClip)) as AudioClip;


                //TheGrayEdited
                sfxAlwaysOnClipDict[SFX_ALWAYS_ON_ID.LASER_LOOP] = Resources.Load("Audio/Electric/Wav/Ambience/Ambience_Sinister_Electric_Cello_Loop_02", typeof(AudioClip)) as AudioClip;

                sfxClipDict[SFX_ID.MIRROR_HIT] = Resources.Load("Audio/Electric/Wav/UI_Synth/UI_Synth_01", typeof(AudioClip)) as AudioClip;
                sfxClipDict[SFX_ID.GATE_RAISE] = Resources.Load("Audio/Horror/Wav/Gate_Open_00", typeof(AudioClip)) as AudioClip;

                sfxShortClipDict[SFX_SHORT_DUR_ID.MIRROR_HIT] = Resources.Load("Audio/Electric/Wav/UI_Synth/UI_Synth_01", typeof(AudioClip)) as AudioClip;
                sfxShortClipDict[SFX_SHORT_DUR_ID.GATE_RAISE] = Resources.Load("Audio/Horror/Wav/Gate_Open_00", typeof(AudioClip)) as AudioClip;
                sfxShortClipDict[SFX_SHORT_DUR_ID.STONE_DOOR_MOVE] = Resources.Load("Audio/Custom/DoorMoving", typeof(AudioClip)) as AudioClip;
                sfxShortClipDict[SFX_SHORT_DUR_ID.LEVER_SWITCH] = Resources.Load("Audio/Custom/LeverSwitch", typeof(AudioClip)) as AudioClip;
                sfxShortClipDict[SFX_SHORT_DUR_ID.TIME_SPEED_UP] = Resources.Load("Audio/Custom/TimeSpeedUp", typeof(AudioClip)) as AudioClip;
                sfxShortClipDict[SFX_SHORT_DUR_ID.TIME_SLOW_DOWN] = Resources.Load("Audio/Custom/TimeSlowDown", typeof(AudioClip)) as AudioClip;
                sfxShortClipDict[SFX_SHORT_DUR_ID.COGSWORTH_MOVING] = Resources.Load("Audio/Custom/CogsworthMovingSound", typeof(AudioClip)) as AudioClip;
                sfxShortClipDict[SFX_SHORT_DUR_ID.EXPLOSION] = Resources.Load("Audio/Custom/Explosion", typeof(AudioClip)) as AudioClip;

                sfxLongClipDict[SFK_LONGISH_DUR_ID.BOOING_CROWD] = Resources.Load("Audio/Custom/BooingCrowd", typeof(AudioClip)) as AudioClip;
                sfxLongClipDict[SFK_LONGISH_DUR_ID.CLAPPING_CROWD] = Resources.Load("Audio/Custom/ClappingCrowd", typeof(AudioClip)) as AudioClip;
                sfxLongClipDict[SFK_LONGISH_DUR_ID.GAME_START] = Resources.Load("Audio/Custom/GameStart", typeof(AudioClip)) as AudioClip;
                sfxLongClipDict[SFK_LONGISH_DUR_ID.LEVEL_START] = Resources.Load("Audio/Custom/LevelStart", typeof(AudioClip)) as AudioClip;
                sfxLongClipDict[SFK_LONGISH_DUR_ID.MAGIC_PORTAL_SOUND] = Resources.Load("Audio/Custom/MagicSounds", typeof(AudioClip)) as AudioClip;
            }
            else
            {
                DestroyObject(this);
            }
        }

        // Play drop in source - AUDIO_SOURCE_ID.DROP_IN_SOURCE
        public void PlaySoundClip(AUDIO_SOURCE_ID source, AudioClip clip)
        {
            AudioSources[(int)source].clip = clip;
            AudioSources[(int)source].Play();
        }

        public void SwitchLoopingForSource(AUDIO_SOURCE_ID src, bool isLooping)
        {
            AudioSources[(int)src].loop = isLooping;
        }
        public void StopAudioForSource(AUDIO_SOURCE_ID src)
        {
            AudioSources[(int)src].Stop();
        }

        public void SetAudioSourceVol(AUDIO_SOURCE_ID src, float vol)
        {
            AudioSources[(int)src].volume = vol;
        }

        public bool IsClipPlayingInSource(AUDIO_SOURCE_ID src)
        {
            return AudioSources[(int)src].isPlaying;
        }
        
        public void PlaySound(AUDIO_SOURCE_ID sourceToPlay)
        {
            AudioSources[(int)sourceToPlay].Play();
        }


        public void PlaySFXSound(SFX_ID sfxID)
        {
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_SOURCE].clip = sfxClipDict[sfxID];
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_SOURCE].Play();
        }

        public void PlayAlwaysOnSFXSound(SFX_ALWAYS_ON_ID sfxID)
        {
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_ALWAYS_ON].clip = sfxAlwaysOnClipDict[sfxID];
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_ALWAYS_ON].Play();
        }

        public void PlaySFXShortDurSound(SFX_SHORT_DUR_ID sfxID)
        {
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_SHORT_DUR].clip = sfxShortClipDict[sfxID];
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_SHORT_DUR].Play();
        }

        public void PlaySFXLongtDurSound(SFK_LONGISH_DUR_ID sfxID)
        {
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_LONGISH_DUR].clip = sfxLongClipDict[sfxID];
            AudioSources[(int)AUDIO_SOURCE_ID.SFX_LONGISH_DUR].Play();
        }

        public void PlayMusicSound(MUSIC_ID musicID)
        {
            AudioSources[(int)AUDIO_SOURCE_ID.MUSIC_SOURCE].clip = musicClipDict[musicID];
            AudioSources[(int)AUDIO_SOURCE_ID.MUSIC_SOURCE].Play();
        }
    }
}