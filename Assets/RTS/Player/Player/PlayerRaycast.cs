using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public Camera mainCamera;
    public float rayDistance = 100f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Verifica se o botão do mouse foi clicado
        {
            Vector3 mousePos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePos); // Cria um Ray a partir da posição do mouse na tela
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance)) // Se o Ray atingir algo
            {
                Debug.Log("Hit: " + hit.collider.name);
                Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 2f); // Desenha o Ray na direção certa
                PlayerCamera playerCamera = this.GetComponent<PlayerCamera>();
                if (hit.collider.CompareTag("Tile")) // Verifica se o objeto atingido tem a tag "Tile"
                {
                    Renderer renderer = hit.collider.GetComponent<Renderer>();
                    if (renderer != null) // Certifique-se de que há um Renderer
                    {
                        if (playerCamera != null)
                        {
                            playerCamera.ChangeCameraPositionToFocusTile(hit.collider.gameObject); // Muda a posição da câmera para focar no tile atingido
                        }

                        // Alterna a cor entre amarelo e vermelho
                        if (renderer.material.color == Color.yellow)
                        {
                            renderer.material.color = Color.red;
                        }
                        else
                        {
                            renderer.material.color = Color.yellow;
                        }
                    }
                }

                if (hit.collider.CompareTag("Bibli"))
                {
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 2f); // Se não houver colisão, desenha um Ray verde
            }
        }
    }
}
