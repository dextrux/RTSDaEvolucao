using UnityEngine;
using System.Collections;
public class GridBuilder : MonoBehaviour
{
    public GameObject hexTile;
    private Vector3 _startPosition = new Vector3(0, 0, 0);
    private float _lado;
    private float _oddOffset;
    private float _evenOffset;
    const int maxLevel = 8;
    public GameObject parentObject;
    GameObject newObject;
    Vector3[] directions = {
        Vector3.right,
        Quaternion.Euler(0, 60 * 1, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 2, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 3, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 4, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 5, 0) * Vector3.right
    };
    private void Awake()
    {
        _lado = 1 * 3;
        _oddOffset = _lado * 1.7f;
        _evenOffset = _lado * 3;
        StartCoroutine(GridBuild(maxLevel, hexTile));
    }
    private IEnumerator GridBuild(int maxLevel, GameObject hex)
    {
        if (hex == null)
        {
            Debug.LogError("Hex tile is not assigned.");
            yield break;
        }
        newObject = Instantiate(hex, _startPosition, Quaternion.identity);
        newObject.transform.SetParent(parentObject.transform);
        //yield return new WaitForSeconds(0.2f);
        for (int level = 1; level < maxLevel; level++)
        {
            Vector3 currentPos = _startPosition + directions[4] * _oddOffset * level;
            for (int dirIndex = 0; dirIndex < 6; dirIndex++)
            {
                for (int i = 0; i < level; i++)
                {
                    newObject = Instantiate(hex, currentPos, Quaternion.identity);
                    newObject.transform.SetParent(parentObject.transform);
                    //yield return new WaitForSeconds(0.2f);
                    currentPos += directions[dirIndex] * _oddOffset;
                }
            }
        }
    }
}