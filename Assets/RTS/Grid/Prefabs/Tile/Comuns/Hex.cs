using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Hex : MonoBehaviour
{
    new Renderer renderer;
    Color color;
    public LayerMask layerMask;
    Vector3[] directions = { Vector3.right, Quaternion.Euler(0, 60 * 1, 0) * Vector3.right, Quaternion.Euler(0, 60 * 2, 0) * Vector3.right , Quaternion.Euler(0, 60 * 3, 0) * Vector3.right , Quaternion.Euler(0, 60 * 4, 0) * Vector3.right , Quaternion.Euler(0, 60 * 5, 0) * Vector3.right};
    public bool rayCastBool;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    public float duration = 3.0f;

    private float elapsedTime = 0f;
    private void Start()
    {
        _startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        _endPosition = new Vector3(_startPosition.x, 0, _startPosition.z);
        renderer = this.GetComponent<Renderer>();
        color = RandomizeColor(color);      
        renderer.material.color = color;
        rayCastBool = false;
    }

    private void Update()
    {
        if (rayCastBool == true)
        {
            Raycast(directions[0], 0);
            Raycast(directions[1], 1);
            Raycast(directions[2], 2);
            Raycast(directions[3], 3);
            Raycast(directions[4], 4);
            Raycast(directions[5], 5);
        }

        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = (elapsedTime * 10f) / duration;  // Increase speed by 50%
            transform.position = Vector3.Slerp(_startPosition, _endPosition, t);
        }

    }


    private Color RandomizeColor(Color color)
    {
        if(this.GetComponent<Tile>().GetBiome() == Biome.Araucaria_Atlantica && this.GetComponent<Tile>().GetTileType() == TileType.Vazio)
        {
            return Color.green;
        }
        if (this.GetComponent<Tile>().GetBiome() == Biome.Araucaria_Atlantica && this.GetComponent<Tile>().GetTileType() == TileType.Vazio)
        {
            return Color.blue;
        }
        if (this.GetComponent<Tile>().GetBiome() == Biome.Araucaria_Atlantica && this.GetComponent<Tile>().GetTileType() == TileType.Vazio)
        {
            return Color.gray;
        }
        if (this.GetComponent<Tile>().GetBiome() == Biome.Araucaria_Atlantica && this.GetComponent<Tile>().GetTileType() == TileType.Vazio)
        {
            return Color.yellow;
        }
        return Color.red;
    }

    private void Raycast(Vector3 direction, int i)
    {
        if (Physics.Raycast(transform.position, directions[i] * 1f, out RaycastHit hit, 2f, layerMask))
        {
            Debug.Log($"Is hitting: {hit.collider.name}");
            Debug.DrawRay(transform.position, directions[i] * hit.distance, Color.red);
        }
        else
        {
            Debug.Log("Is not hitting");
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * 1f, Color.green, 2f); // Use an arbitrary length like 1000 units
        }
    }
}
