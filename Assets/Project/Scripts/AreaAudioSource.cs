using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(CircleCollider2D))]
public class AreaAudioSource : MonoBehaviour
{
    [Range(1,4)]
    public float smoothVolume = 2;

    [SerializeField, HideInInspector]
    AudioSource audioSource;
    [SerializeField, HideInInspector]
    CircleCollider2D circleCollider;

    private void Reset()
    {
        audioSource = GetComponent<AudioSource>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        Debug.Log("reset");
    }

    private void Awake()
    {
        audioSource.volume = 0;
        audioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        audioSource.volume = 0;
        audioSource.Play();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        audioSource.volume =
             Mathf.Pow( 1 -  Mathf.Clamp01(
                    Vector2.Distance(collision.transform.position, transform.position)
                    / (circleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y))
                ), smoothVolume);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        audioSource.volume = 0;
        audioSource.Pause();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, circleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y));
        Gizmos.DrawIcon(transform.position, "AE", true);
    }
#endif
}
