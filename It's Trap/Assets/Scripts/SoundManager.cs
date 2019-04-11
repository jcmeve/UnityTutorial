using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string soundName;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;

    void Start()
    {
        if (instance == null)
            instance = this;
        else {
            Destroy(this);
        }
        PlayRandomBgm();
    }

    public void PlayRandomBgm() {
        int random = Random.Range(0, bgmSounds.Length);
        bgmPlayer.clip = bgmSounds[random].clip;
        bgmPlayer.Play();
    }
    public void PlaySE(string _soundName) {
        for(int i = 0; i < sfxSounds.Length; i++) {
            if (sfxSounds[i].soundName == _soundName) {
                for (int j = 0; j < sfxPlayer.Length; j++) {
                    if (!sfxPlayer[j].isPlaying) { 
                        sfxPlayer[j].clip = sfxSounds[i].clip;
                        sfxPlayer[j].Play();
                        return;

                    }

                }
                Debug.Log("빈 오디오소스가 없음");
                return;
            }
        }
        Debug.Log("찾는 사운드가 없음");
    }
}
