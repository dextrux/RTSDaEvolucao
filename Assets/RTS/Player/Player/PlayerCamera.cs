using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;
    public float rotationSpeed = 100f; // Velocidade de rota��o
    public float moveSpeed = 30f; // Velocidade de movimenta��o da c�mera
    public float minY = 20f; // Limite inferior para a posi��o Y da c�mera
    public float maxY = 90f; // Limite superior para a posi��o Y da c�mera
    private Coroutine moveCoroutine;
    private bool _isRotating = false;
    private Vector3 resetPosition = new Vector3(0, 90, 0); // Posi��o de reset da c�mera

    void Start()
    {
        ResetPlayerCameraPosition();
    }

    void Update()
    {
        HandleCameraMovement();
        HandleCameraRotation();
        if (Input.GetMouseButtonDown(1))
        {
            _isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _isRotating = false;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ChangeCameraPosition(-20);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ChangeCameraPosition(20);
        }
    }
    private void ChangeCameraPosition(int factorDeepness)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        // Calcula a nova posi��o com os limites de y
        Vector3 targetPosition = new Vector3(
            playerCamera.transform.position.x,
            Mathf.Clamp(playerCamera.transform.position.y + factorDeepness, 20, resetPosition.y), // Limita o y
            playerCamera.transform.position.z
        );

        moveCoroutine = StartCoroutine(MoveCameraToPosition(targetPosition));
    }
    private void HandleCameraMovement()
    {
        if (Input.GetMouseButton(0)) // Segure o bot�o direito do mouse para mover a c�mera
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Fator de deslocamento aumentado
            float movementFactor = 5.0f; // Ajuste este valor conforme necess�rio
            Vector3 move = new Vector3(mouseX, 0, mouseY) * moveSpeed * movementFactor * Time.deltaTime;

            // Atualiza a posi��o da c�mera somente nos eixos X e Z
            Vector3 newPosition = playerCamera.transform.position + move;

            // Mant�m o valor de Y da posi��o atual da c�mera (ou ajusta conforme necess�rio)
            newPosition.y = playerCamera.transform.position.y;

            playerCamera.transform.position = newPosition;
        }
    }



    private void HandleCameraRotation()
    {
        // Rota��o da c�mera com base na entrada do mouse
        if (_isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.left, mouseY * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void ResetPlayerCameraPosition()
    {
        playerCamera.transform.position = resetPosition;
        playerCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Altera a posi��o da c�mera para focar em um tile espec�fico
    public void ChangeCameraPositionToFocusTile(GameObject tile)
    {
        Vector3 targetPosition = new Vector3(tile.transform.position.x, Mathf.Clamp(tile.transform.position.y + 10, minY, maxY), tile.transform.position.z);
        StartCoroutine(MoveCameraToPosition(targetPosition));
    }

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition)
    {
        float duration = 1.0f; // Dura��o da transi��o em segundos
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

    private void RotateCamera()
    {
        if (_isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.forward, mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public void ResetRotation()
    {
        playerCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
