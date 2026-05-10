using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class ViewpointZone : MonoBehaviour
{
    [Tooltip("Nuova altezza Y del punto di vista quando entri in questa zona (in metri).")]
    public float newHeight = 1.2f;

    private void OnTriggerEnter(Collider other)
    {
        XROrigin xrOrigin = other.GetComponentInParent<XROrigin>();
        if (xrOrigin != null)
        {
            Vector3 pos = xrOrigin.transform.position;
            pos.y = newHeight; // imposta la nuova altezza
            xrOrigin.transform.position = pos;
        }
    }
}

