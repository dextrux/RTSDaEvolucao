using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSet : MonoBehaviour
{
    public float zRate;
    float aspectRatio;
    private Camera _mainCamera;
    public Transform[] objects;
    private float _baseDistance = 2.5f;
    private float _cameraDistance = 1.75f;
    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        UpdatePositions();
    }
    public void UpdatePositions()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            Vector3 mousePos = new Vector3(((0.1f * Screen.width) + (i * (0.2f * Screen.width))), (0.17f * Screen.height), zRate);
            Ray ray = _mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                objects[i].position = hit.point;
            }
        }
    }
}
