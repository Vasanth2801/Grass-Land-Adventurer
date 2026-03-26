using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource != null &&  audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}