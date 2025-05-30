using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource audioSource;
    public AudioClip chipSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: keep it alive between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayChipSound()
    {
        if (audioSource != null && chipSound != null)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f); // Optional
            audioSource.PlayOneShot(chipSound);
        }
        else
        {
            Debug.LogWarning("Missing AudioSource or chipSound on SoundManager.");
        }
    }
}
