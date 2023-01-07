using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    private static bool initialized;
    private static AudioSource audioSource;
    private static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public static bool Initialized => initialized;

    public static void Initialize(AudioSource audio)
    {
        initialized = true;
        audioSource = audio;
        audioClips.Add("", Resources.Load<AudioClip>("background"));
        audioClips.Add("moo", Resources.Load<AudioClip>("moo"));
        audioClips.Add("explode", Resources.Load<AudioClip>("explode"));
        audioClips.Add("dizzy", Resources.Load<AudioClip>("moo2"));
        audioClips.Add("shoot", Resources.Load<AudioClip>("shoot"));
        audioClips.Add("ouch", Resources.Load<AudioClip>("ouch"));
        audioClips.Add("crush", Resources.Load<AudioClip>("crush"));
        audioClips.Add("powerup", Resources.Load<AudioClip>("powerup"));
        audioClips.Add("bounce", Resources.Load<AudioClip>("bounce"));
        audioClips.Add("chainsaw", Resources.Load<AudioClip>("chainsaw"));
        audioClips.Add("push", Resources.Load<AudioClip>("push"));
    }

    public static void Play(string name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}