using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [Header("Rota��o")]
    public float rotationSpeed = 30f;

    [Header("Movimenta��o Vertical")]
    public float floatAmplitude = 0.5f;
    public float floatSpeed = 2f;

    private Vector3 startPosition;

    void OnEnable()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

