
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

public class AudioCenter : MonoBehaviour
{
    public static AudioCenter instance;
    
    float t = 5;
    public bool gameStarted = false;
    public AudioClip humanDeathClip;
    public AudioClip demonDeathClip;
    public AudioClip plasmaShot;
    public AudioClip actionClip;

    public List<AudioSource> humanDeathSource = new List<AudioSource>();
    public List<AudioSource> demonDeathSource = new List<AudioSource>();
    public List<AudioSource> plasmaShotSource = new List<AudioSource>();
    public AudioSource actionPassiveSource;
    public AudioSource BGM;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        foreach (AudioSource source in humanDeathSource)
        {
            source.clip = humanDeathClip;
        }
        foreach (AudioSource source in demonDeathSource)
        {
            source.clip = demonDeathClip;
        }
        foreach (AudioSource source in plasmaShotSource)
        {
            source.clip = plasmaShot;
        }        
        actionPassiveSource.clip = actionClip;
    }
    void Update()
    {
        if (gameStarted)
        {
            t -= Time.deltaTime;
            if (t < 0)
            {
                t = 5 + Random.Range(-0.75f, 2.25f);
                actionPassiveSource.Play();
            }
        }
    }

    public void DemonDeath()
    {
        for(int i = 0; i < demonDeathSource.Count; i++)
        {
            if(!demonDeathSource[i].isPlaying)
            {
                demonDeathSource[i].Play();
                break;
            }
            if(i == demonDeathSource.Count - 1 && demonDeathSource[i].isPlaying)
            {

                if (i == demonDeathSource.Count - 1 && demonDeathSource[i].isPlaying)
                {
                    demonDeathSource[i].pitch = Random.Range(0.75f, 1.25f);
                    demonDeathSource.Add(
                    Instantiate(new AudioSource()
                    {
                        name = "NewHumanDeathAudioSource",
                        clip = demonDeathClip,
                        playOnAwake = false,
                        loop = false
                    }));
                    demonDeathSource[demonDeathSource.Count - 1].Play();
                    break;
                }
            }
        }
    }

    public void HumanDeath()
    {
        for (int i = 0; i < humanDeathSource.Count; i++)
        {
            if (!humanDeathSource[i].isPlaying)
            {
                humanDeathSource[i].pitch = Random.Range(0.75f, 1.25f);
                humanDeathSource[i].Play();
                break;
            }
            if (i == humanDeathSource.Count - 1 && humanDeathSource[i].isPlaying)
            {
                humanDeathSource[i].pitch = Random.Range(0.75f, 1.25f);
                humanDeathSource.Add(
                Instantiate(new AudioSource()
                {
                    name = "NewHumanDeathAudioSource",
                    clip = humanDeathClip,
                    playOnAwake = false,
                    loop = false
                }));
                humanDeathSource[humanDeathSource.Count - 1].Play();
                break;
            }
        }
    }

    public void PlasmaShotSound()
    {
        for(int i = 0; i < plasmaShotSource.Count; i++)
        {
            if (!plasmaShotSource[i].isPlaying)
            {
                plasmaShotSource[i].Play();
                break;
            }
        }
    }

    public void SetGameActive()
    {
        gameStarted = true;
        BGM.Play();
    }

    public void SetGameInactive()
    {
        gameStarted = false;
        BGM.Stop();
    }
}
