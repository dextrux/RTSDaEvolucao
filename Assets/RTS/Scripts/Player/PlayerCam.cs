using System.Collections;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    #region Public Variables
    public Camera playerCamera; // Referência para a câmera do jogador
    public float moveSpeed = 15f; // Velocidade de movimento da câmera
    public float inputHorizontalSensitivity = 1f; // Sensibilidade horizontal ajustável
    public float inputVerticalSensitivity = 1f;   // Sensibilidade vertical ajustável
    public float edgeMoveSpeed = 10f; // Velocidade de movimento nas bordas
    public float edgeSensitivity = 1f; // Sensibilidade para a detecção das bordas

    public float zoomSpeed = 10f; // Velocidade de zoom
    public float minZoom = 20f; // Limite mínimo de zoom
    public float maxZoom = 60f; // Limite máximo de zoom
    public float zoomSmoothing = 5f; // Controle de suavização do zoom
    #endregion

    #region Constants
    public const float minY = -20f; // Altura mínima permitida para a câmera
    public const float maxY = 46f;  // Altura máxima permitida para a câmera
    public const float minX = -30f; // Limite mínimo no eixo X
    public const float maxX = 30f;  // Limite máximo no eixo X
    public const float minZ = -35f; // Limite mínimo no eixo Z
    public const float maxZ = 24f;  // Limite máximo no eixo Z
    #endregion

    #region Private Variables
    private Coroutine moveCoroutine; // Referência para a corrotina ativa de movimento
    private Vector3 resetPosition;   // Posição inicial da câmera para reset
    private Vector3 lastMousePosition; // Última posição registrada do mouse
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        resetPosition = new Vector3(0, maxY, -35); // Define a posição de reset
        ResetPlayerCameraPosition(); // Reseta a posição da câmera no início
    }

    private void Update()
    {
        HandleCameraDrag(); // Movimenta a câmera ao arrastar com o botão direito
        HandleScreenEdgeMovement(); //Movimenta a câmera ao posicionar o mouse nas bordas
        HandleCameraZoom(); // Zoom mais responsivo

        // Zoom da câmera com a rolagem do mouse
        /*if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ChangeCameraPosition(20);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ChangeCameraPosition(-20);
        }*/

        // Reseta a posição da câmera ao pressionar a tecla "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayerCameraPosition();
        }
    }
    #endregion

    #region Camera Movement
    private void HandleCameraDrag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition; // Registra a posição inicial do mouse
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; // Calcula a diferença
            lastMousePosition = Input.mousePosition; // Atualiza a última posição registrada

            Vector3 move = new Vector3(-delta.x * inputHorizontalSensitivity, 0, -delta.y * inputVerticalSensitivity) * moveSpeed * Time.deltaTime;
            Vector3 newPosition = playerCamera.transform.position + move;

            // Limita a posição da câmera
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            newPosition.y = playerCamera.transform.position.y;

            playerCamera.transform.position = newPosition;
        }
    }
    private void HandleCameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollInput) > 0.01f)
        {
            // Calcula o novo valor de zoom
            float targetY = playerCamera.transform.position.y - scrollInput * zoomSpeed;

            // Aplica os limites de zoom
            targetY = Mathf.Clamp(targetY, minZoom, maxZoom);

            // Suaviza a transição entre o valor atual e o alvo
            float smoothedY = Mathf.Lerp(playerCamera.transform.position.y, targetY, Time.deltaTime * zoomSmoothing);

            // Cria a nova posição da câmera
            Vector3 newPosition = new Vector3(
                playerCamera.transform.position.x,
                smoothedY,
                playerCamera.transform.position.z
            );

            // Atualiza a posição da câmera
            playerCamera.transform.position = newPosition;
        }
    }
    private void HandleScreenEdgeMovement()
    {
        float edgeSize = 10f * edgeSensitivity;
        Vector3 move = Vector3.zero;

        if (Input.mousePosition.x <= edgeSize)
            move.x = -1;
        else if (Input.mousePosition.x >= Screen.width - edgeSize)
            move.x = 1;

        if (Input.mousePosition.y <= edgeSize)
            move.z = -1;
        else if (Input.mousePosition.y >= Screen.height - edgeSize)
            move.z = 1;

        if (move != Vector3.zero)
        {
            Vector3 newPosition = playerCamera.transform.position + move * edgeMoveSpeed * Time.deltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            playerCamera.transform.position = newPosition;
        }
    }


    private void ChangeCameraPosition(int factorDeepness)
    {
        // Para a corrotina atual se existir
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        // Calcula a nova posição no eixo Y com limites
        Vector3 targetPosition = new Vector3(
            playerCamera.transform.position.x,
            Mathf.Clamp(playerCamera.transform.position.y + factorDeepness, 20, resetPosition.y),
            playerCamera.transform.position.z
        );

        // Inicia uma nova corrotina para mover a câmera suavemente
        moveCoroutine = StartCoroutine(MoveCameraToPosition(targetPosition));
    }

    public void ChangeCameraPositionToFocusPiece(GameObject piece)
    {
        // Calcula uma posição focada em uma peça específica
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
        // Reseta a posição e rotação da câmera
        playerCamera.transform.position = resetPosition;
        playerCamera.transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    public void ResetRotation()
    {
        // Reseta apenas a rotação da câmera
        playerCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    #endregion

    #region Coroutines
    private IEnumerator MoveCameraToPosition(Vector3 targetPosition)
    {
        float duration = 1.0f; // Duração da animação
        Vector3 startPosition = playerCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Move a câmera suavemente usando Lerp
            playerCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante que a posição final seja definida corretamente
        playerCamera.transform.position = targetPosition;
    }
    #endregion

    #region Mobile Handling (Placeholder)
    private void HandlerCameraMovementMobile()
    {
        // Implementação para dispositivos móveis ainda não desenvolvida
    }
    #endregion
}