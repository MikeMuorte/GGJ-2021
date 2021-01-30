using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource), typeof(CircleCollider2D))]
public class AreaAudioSource : MonoBehaviour
{
    [Range(1,4)]
    public float smoothVolume = 2;

    public AudioMixer audioMixer;
    [SerializeField, HideInInspector]
    CircleCollider2D circleCollider;

    private void Reset()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
        Debug.Log("reset");
    }

    private void Start()
    {
        audioMixer.SetFloat("Ambient_V", NormalizedVolume(1));
        audioMixer.SetFloat("Area_V", NormalizedVolume(0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        audioMixer.SetFloat("Ambient_V", NormalizedVolume(1));
        audioMixer.SetFloat("Area_V", NormalizedVolume(0));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        float v = Mathf.Clamp01(
                                Vector2.Distance(collision.transform.position, transform.position)
                                / (circleCollider.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y)));

        audioMixer.SetFloat("Ambient_V", NormalizedVolume(Mathf.Pow(v, smoothVolume)));
        audioMixer.SetFloat("Area_V", NormalizedVolume(Mathf.Pow(1 - v, smoothVolume)));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        audioMixer.SetFloat("Ambient_V", NormalizedVolume(1));
        audioMixer.SetFloat("Area_V", NormalizedVolume(0));
    }

    float NormalizedVolume(float v)
    {
        v = Mathf.Max(v, 0.00001f);
        return 20 * Mathf.Log10(v);
        //return Mathf.Lerp(-40, 0, v);
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
