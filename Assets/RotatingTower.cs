using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script rotates the GameObject continuously around the x-axis.
/// </summary>
public class RotatingTower : MonoBehaviour
{
   public float speed = 20f;

    void Update()
    {
        transform.Rotate(Vector3.right * speed * Time.deltaTime);
    }
}