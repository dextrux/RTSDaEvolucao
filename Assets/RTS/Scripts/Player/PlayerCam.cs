using System.Collections;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    #region Public Variables
    public Camera playerCamera; // Refer�ncia para a c�mera do jogador
    public float moveSpeed = 15f; // Velocidade de movimento da c�mera
    public float inputHorizontalSensitivity = 1f; // Sensibilidade horizontal ajust�vel
    public float inputVerticalSensitivity = 1f;   // Sensibilidade vertical ajust�vel
    public float edgeMoveSpeed = 10f; // Velocidade de movimento nas bordas
    public float edgeSensitivity = 1f; // Sensibilidade para a detec��o das bordas

    public float zoomSpeed = 10f; // Velocidade de zoom
    public float minZoom = 20f; // Limite m�nimo de zoom
    public float maxZoom = 60f; // Limite m�ximo de zoom
    public float zoomSmoothing = 5f; // Controle de suaviza��o do zoom
    #endregion

    #region Constants
    public const float minY = -20f; // Altura m�nima permitida para a c�mera
    public const float maxY = 46f;  // Altura m�xima permitida para a c�mera
    public const float minX = -30f; // Limite m�nimo no eixo X
    public const float maxX = 30f;  // Limite m�ximo no eixo X
    public const float minZ = -35f; // Limite m�nimo no eixo Z
    public const float maxZ = 24f;  // Limite m�ximo no eixo Z
    #endregion

    #region Private Variables
    private Coroutine moveCoroutine; // Refer�ncia para a corrotina ativa de movimento
    private Vector3 resetPosition;   // Posi��o inicial da c�mera para reset
    private Vector3 lastMousePosition; // �ltima posi��o registrada do mouse
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        resetPosition = new Vector3(0, maxY, -35); // Define a posi��o de reset
        ResetPlayerCameraPosition(); // Reseta a posi��o da c�mera no in�cio
    }

    private void Update()
    {
        HandleCameraDrag(); // Movimenta a c�mera ao arrastar com o bot�o direito
        HandleScreenEdgeMovement(); //Movimenta a c�mera ao posicionar o mouse nas bordas
        HandleCameraZoom(); // Zoom mais responsivo

        // Zoom da c�mera com a rolagem do mouse
        /*if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            ChangeCameraPosition(20);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            ChangeCameraPosition(-20);
        }*/

        // Reseta a posi��o da c�mera ao pressionar a tecla "R"
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
            lastMousePosition = Input.mousePosition; // Registra a posi��o inicial do mouse
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; // Calcula a diferen�a
            lastMousePosition = Input.mousePosition; // Atualiza a �ltima posi��o registrada

            Vector3 move = new Vector3(-delta.x * inputHorizontalSensitivity, 0, -delta.y * inputVerticalSensitivity) * moveSpeed * Time.deltaTime;
            Vector3 newPosition = playerCamera.transform.position + move;

            // Limita a posi��o da c�mera
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

            // Suaviza a transi��o entre o valor atual e o alvo
            float smoothedY = Mathf.Lerp(playerCamera.transform.position.y, targetY, Time.deltaTime * zoomSmoothing);

            // Cria a nova posi��o da c�mera
            Vector3 newPosition = new Vector3(
                playerCamera.transform.position.x,
                smoothedY,
                playerCamera.transform.position.z
            );

            // Atualiza a posi��o da c�mera
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

        // Calcula a nova posi��o no eixo Y com limites
        Vector3 targetPosition = new Vector3(
            playerCamera.transform.position.x,
            Mathf.Clamp(playerCamera.transform.position.y + factorDeepness, 20, resetPosition.y),
            playerCamera.transform.position.z
        );

        // Inicia uma nova corrotina para mover a c�mera suavemente
        moveCoroutine = StartCoroutine(MoveCameraToPosition(targetPosition));
    }

    public void ChangeCameraPositionToFocusPiece(GameObject piece)
    {
        // Calcula uma posi��o focada em uma pe�a espec�fica
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
        // Reseta a posi��o e rota��o da c�mera
        playerCamera.transform.position = resetPosition;
        playerCamera.transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    public void ResetRotation()
    {
        // Reseta apenas a rota��o da c�mera
        playerCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    #endregion

    #region Coroutines
    private IEnumerator MoveCameraToPosition(Vector3 targetPosition)
    {
        float duration = 1.0f; // Dura��o da anima��o
        Vector3 startPosition = playerCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Move a c�mera suavemente usando Lerp
            playerCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Garante que a posi��o final seja definida corretamente
        playerCamera.transform.position = targetPosition;
    }
    #endregion

    #region Mobile Handling (Placeholder)
    private void HandlerCameraMovementMobile()
    {
        // Implementa��o para dispositivos m�veis ainda n�o desenvolvida
    }
    #endregion
}