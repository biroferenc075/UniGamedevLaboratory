using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip menuClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip slamClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayHit() => PlayClip(hitClip);
    public void PlayDeath() => PlayClip(deathClip);
    public void PlayPickup() => PlayClip(pickupClip);
    public void PlayMenu() => PlayClip(menuClip);
    public void PlayShoot() => PlayClip(shootClip);
    public void PlaySlam() => PlayClip(slamClip);

    private void PlayClip(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

}
