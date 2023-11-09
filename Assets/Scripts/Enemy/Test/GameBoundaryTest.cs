using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoundaryTest : MonoBehaviour
{
    [SerializeField, Range(10.0f, 100.0f)] private float length = 50.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float offsetY = 1.0f;
    [SerializeField] private Color colour = Color.red;

    private void OnDrawGizmos()
    {
        Gizmos.color = colour;
        Vector3 offset = new Vector3(0.0f, offsetY, 0.0f);
        Gizmos.DrawWireCube(transform.position + offset, new Vector3(length, 0.0f, length));
    }

}
