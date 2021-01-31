using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2DEvents : MonoBehaviour
{
    public UnityEvent onTiggerEnter2D;
    public UnityEvent onTiggerStay2D;
    public UnityEvent onTiggerExit2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTiggerEnter2D?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        onTiggerStay2D?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onTiggerExit2D?.Invoke();
    }
}
