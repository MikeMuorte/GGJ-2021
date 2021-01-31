using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2DEvents : MonoBehaviour
{
    public BoxCollider2D col;
    [Space]
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (col)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube((Vector3)col.offset + transform.position, col.size);
        }
        else
            col = GetComponent<BoxCollider2D>();
    }
#endif
}
