﻿using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 5f;
    public const float minY = -1;
    public const float maxY = 10;
    public const float minX = -10;
    public const float maxX = 10;
    public const float minZ = -20;
    public const float maxZ = 10;
    private Coroutine moveCoroutine;
    private Vector3 resetPosition; 

    void Start()
    {
        resetPosition = new Vector3(0, maxY, -10);
        ResetPlayerCameraPosition();
    }

    void Update()
    {
        HandleCameraMovement();
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ChangeCameraPosition(20);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ChangeCameraPosition(-20);
        }
    }
    private void ChangeCameraPosition(int factorDeepness)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        Vector3 targetPosition = new Vector3(
            playerCamera.transform.position.x,
            Mathf.Clamp(playerCamera.transform.position.y + factorDeepness, 20, resetPosition.y),
            playerCamera.transform.position.z
        );

        moveCoroutine = StartCoroutine(MoveCameraToPosition(targetPosition));
    }
    private void HandleCameraMovement()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float movementFactor = 5.0f;
            Vector3 move = new Vector3(mouseX, 0, mouseY) * moveSpeed * movementFactor * Time.deltaTime;
            Vector3 newPosition = playerCamera.transform.position + move;

            // Aplicando os limites de movimento
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            newPosition.y = playerCamera.transform.position.y;

            playerCamera.transform.position = newPosition;
        }
    }
    private void ResetPlayerCameraPosition()
    {
        playerCamera.transform.position = resetPosition;
        playerCamera.transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    public void ChangeCameraPositionToFocusTile(GameObject tile)
    {
        Vector3 targetPosition = new Vector3(tile.transform.position.x, Mathf.Clamp(tile.transform.position.y + 10, minY, maxY), tile.transform.position.z);
        StartCoroutine(MoveCameraToPosition(targetPosition));
    }

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition)
    {
        float duration = 1.0f;
        Vector3 startPosition = playerCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.position = targetPosition;
    }
    public void ResetRotation(){ playerCamera.transform.rotation = Quaternion.Euler(90, 0, 0); }
}