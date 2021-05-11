using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // 곡의 이름
    public AudioClip clip; // 곡
}
public class SoundMgr : MonoBehaviour
{
    static public SoundMgr instance;
    // 싱글턴 singleton 1개   
    // Start is called before the first frame update
    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    
        else
            Destroy(this.gameObject);
    }
    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSouneName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    private void Start()
    {
        playSouneName = new string[audioSourceEffects.Length];
    }


    public  void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)   
        {
            if (_name == effectSounds[i].name)  
            {
                for (int j = 0; j < audioSourceEffects.Length; j++) 
                {
                    if (!audioSourceEffects[j].isPlaying)   
                    {
                        playSouneName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip; // 사운드 교체
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("모든 AudioSource사용중");
                return;
            }
            Debug.Log(_name + "사운드 메니저에 등록되지 않았음");
        }
    }
    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++) 
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = audioSourceEffects.Length; i >= 0;)    
        {
            if (playSouneName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("재생중인 + " + _name + " 사운드가 없다");
    }
}
