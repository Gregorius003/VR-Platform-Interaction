using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAxes : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
