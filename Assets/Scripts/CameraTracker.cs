using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Transform _plane;

    private void Update()
    {
        transform.position = new Vector3(_plane.position.x, transform.position.y, transform.position.z);
    }
}
