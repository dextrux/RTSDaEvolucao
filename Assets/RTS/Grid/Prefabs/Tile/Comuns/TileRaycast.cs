using Unity.VisualScripting;
using UnityEngine;

public class TileRaycast : MonoBehaviour
{
    // Definir o alcance do raycast
    public const float rayDistance = 3f;
    public GameObject tile;
    public Tile tileScript;


    private void Start()
    {
        tileScript = tile.GetComponent<Tile>();
        Raycast();
    }

    private void Update()
    {
        RaycastDraw();
    }
    // Fun��o que calcula os vetores de dire��o para os seis lados de um hex�gono
    Vector3[] GetHexDirections()
    {
        Vector3[] directions = new Vector3[6];
        float angleOffset = 60f;  // Offset para alinhar com os v�rtices de um hex�gono

        for (int i = 0; i < 6; i++)
        {
            float angleDeg = i * 60f + angleOffset;
            float angleRad = Mathf.Deg2Rad * angleDeg;

            // Dire��es no plano XY
            directions[i] = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
        }
        return directions;
    }

    void Raycast()
    {
        // Obter as dire��es
        Vector3[] hexDirections = GetHexDirections();

        // Para cada dire��o, disparar um raycast
        foreach (Vector3 direction in hexDirections)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, direction * rayDistance, Color.red); // Visualiza os raycasts
                                                                                       // Get the closest parent GameObject of the hit object
                GameObject parentObject = hit.collider.transform.parent?.gameObject ?? hit.collider.gameObject;

                // Add the parent object to the adjacent tiles
                tileScript.AddTilesAdjacentes(parentObject);

            }
            else
            {
                Debug.DrawRay(transform.position, direction * rayDistance, Color.green); // Visualiza os raycasts que n�o acertam nada
            }
        }
    }

    void RaycastDraw()
    {
        Vector3[] hexDirections = GetHexDirections();

        // Para cada dire��o, disparar um raycast
        foreach (Vector3 direction in hexDirections)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, direction * rayDistance, Color.red); // Visualiza os raycasts

            }
            else
            {
                Debug.DrawRay(transform.position, direction * rayDistance, Color.green); // Visualiza os raycasts que n�o acertam nada
            }
        }
    }
}

