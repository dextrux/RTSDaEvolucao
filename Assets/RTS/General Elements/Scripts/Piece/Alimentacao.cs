using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alimentacao : MonoBehaviour
{
    public GameObject Jubileu;
    public void Alimentar(GameObject alimento)
    {
        Jubileuson jub = this.GetComponent<Jubileuson>();
        jub._pieceRemainingActions += Random.Range(2, 5);
        transform.position = alimento.transform.position;
        Destroy(alimento);
    }
}
