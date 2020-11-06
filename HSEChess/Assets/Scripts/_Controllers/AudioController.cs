using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : SerializedSingleton<AudioController>
{
    private AudioSource thisSource;
    [SerializeField] private Dictionary<string, AudioClip> audioClips;

    void Start()
    {
        thisSource = GetComponent<AudioSource>();
    }

    public void PlaySound (string _name)
    {
        AudioClip clip;
        if (audioClips.TryGetValue (_name, out clip))
        {
            thisSource.PlayOneShot(clip);
        }
    }
}
