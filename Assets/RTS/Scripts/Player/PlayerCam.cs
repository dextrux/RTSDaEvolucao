using System.Collections;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    #region Public Variables
    public Camera playerCamera;
    public float moveSpeed = 15f;
    #endregion

    #region Constants
    public const float minY = -20f;
    public const float maxY = 46f;
    public const float minX = -30f;
    public const float maxX = 30f;
    public const float minZ = -35f;
    public const float maxZ = 24f;
    #endregion

    #region Private Variables
    private Coroutine moveCoroutine;
    private Vector3 resetPosition;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        resetPosition = new Vector3(0, maxY, -35);
        ResetPlayerCameraPosition();
    }

    private void Update()
    {
        HandleCameraMovement();

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ChangeCameraPosition(20);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ChangeCameraPosition(-20);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerCameraPosition();
        }

        /*
         if(playerCamera.transform.position.x > 50f || playerCamera.transform.position.x < -50f)
        {
            ResetPlayerCameraPosition();
        }
        else if (playerCamera.transform.position.z > 40f || playerCamera.transform.position.z < -40f)
        {
            ResetPlayerCameraPosition();
        }
         */
    }
    #endregion

    #region Camera Movement
    private void HandleCameraMovement()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            float movementFactor = 5.0f;

            Vector3 move = new Vector3(mouseX, 0, mouseY) * moveSpeed * movementFactor * Time.deltaTime;
            Vector3 newPosition = playerCamera.transform.position + move;

            // Apply movement limits
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            newPosition.y = playerCamera.transform.position.y;

            playerCamera.transform.position = newPosition;
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

    public void ChangeCameraPositionToFocusPiece(GameObject piece)
    {
        Vector3 targetPosition = new Vector3(
            piece.transform.position.x + 5.4f,
            Mathf.Clamp(piece.transform.position.y + 10, minY, maxY),
            piece.transform.position.z - 4.1f
        );
        StartCoroutine(MoveCameraToPosition(targetPosition));
    }
    #endregion

    #region Camera Reset
    private void ResetPlayerCameraPosition()
    {
        playerCamera.transform.position = resetPosition;
        playerCamera.transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    public void ResetRotation()
    {
        playerCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    #endregion

    #region Coroutines
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
    #endregion

    #region Mobile Handling (Placeholder)
    private void HandlerCameraMovementMobile()
    {
        // Implementation for mobile handling goes here.
    }
    #endregion
}
