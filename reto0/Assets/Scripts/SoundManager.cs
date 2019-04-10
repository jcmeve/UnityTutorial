using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource efxSource;
    public static SoundManager instance =  null;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;
    public void Awake()
    {
        if (instance == null)
        {
            Debug.Log("핫 하 새로운 사메 생성");
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("사메 뿌서ㅠㅠ");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySingle(AudioClip audioClip)
    {
        efxSource.clip = audioClip;
        efxSource.Play();
    }
    public void RandomizeSfx(params AudioClip[] audioClip)
    {
        int randomIdx = Random.Range(0, audioClip.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = audioClip[randomIdx];
        efxSource.Play();
    }
    // Update is called once per frame

}
