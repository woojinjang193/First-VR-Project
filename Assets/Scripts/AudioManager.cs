using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    [SerializeField] private float[] sfxVolumes;

    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;
    public float sfxMasterVolume = 1f;
   
    public enum Sfx {Teleport, ArrowTaking, ArrowLoading, StringPulling, Shoot, ArrowHit, MonsterDie, MonsterAttack, Button, GateGetDamaged, GateBrokeDown}

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
        Init();
    }


    void Init()
    {
        GameObject bgmObject = new GameObject("bgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false; // 시작시 재생 끔
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
        }
    }
    public void PlayBgm()
    {
        if (bgmPlayer != null && bgmClip != null && !bgmPlayer.isPlaying)
        {
            bgmPlayer.Play();
        }

        
    }

    public void PlaySfx(Sfx sfx)
    {
        int sfxIndex = (int)sfx;


        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            float finalVolume = sfxVolumes[sfxIndex] * sfxMasterVolume;
            sfxPlayers[loopIndex].clip = sfxClips[sfxIndex];
            sfxPlayers[loopIndex].volume = finalVolume;
            sfxPlayers[loopIndex].Play();
            break;
        }
        
    }

   //public void SetBgmVolume(float _volume)
   //{
   //    bgmVolume = _volume;
   //    if (bgmPlayer != null)
   //    {
   //        bgmPlayer.volume = bgmVolume;
   //    }
   //}
   //
   //public void SetSfxVolume(float _volume)
   //{
   //    sfxMasterVolume = _volume;
   //}

}  
