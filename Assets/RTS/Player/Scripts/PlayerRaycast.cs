using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    //Atributos da classe
    public Camera mainCamera;
    public float rayDistance = 100f;

    //Metódos
    void Update() { if (Input.GetMouseButtonDown(0)) { MouseRaycast();  } }
    private void MouseRaycast()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 2f);
            PlayerCamera playerCamera = this.GetComponent<PlayerCamera>();
            RayCastCollissionHandler(hit, playerCamera);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 2f);
        }
    }

    private void RayCastCollissionHandler(RaycastHit hit, PlayerCamera playerCamera)
    {
        if (hit.collider.CompareTag("Tile"))
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null) 
            {
                if (playerCamera != null)
                {
                    playerCamera.ChangeCameraPositionToFocusTile(hit.collider.gameObject);
                }
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
        else if (hit.collider.CompareTag("Bibli"))
        {

        }

    }
}