using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformResetter : MonoBehaviour, IResetObject
{
    private Quaternion initialRotation;
    private Vector3 initialPosition;

    private void Start()
    {
        initialRotation = transform.rotation;
        initialPosition = transform.position;
    }

    public void ResetState()
    {
        transform.rotation = initialRotation;
        transform.position = initialPosition;
    }
}
