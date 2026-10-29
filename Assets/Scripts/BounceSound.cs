using UnityEngine;

// Plays a bounce sound every time the ball hits something. Faster hits sound louder.
[RequireComponent(typeof(AudioSource))]
public class BounceSound : MonoBehaviour
{
    public AudioClip bounceClip;

    // Below this speed the ball is just rolling or resting, so stay quiet
    public float minSpeed = 0.3f;

    // At this speed (or faster) the bounce plays at full volume
    public float maxSpeed = 5f;

    private AudioSource audioSource;
    private Rigidbody body;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        float speed = body.linearVelocity.magnitude;
        if (speed < minSpeed) return;

        // 0 at minSpeed, 1 at maxSpeed
        float volume = Mathf.InverseLerp(minSpeed, maxSpeed, speed);
        audioSource.PlayOneShot(bounceClip, volume);
    }
}
