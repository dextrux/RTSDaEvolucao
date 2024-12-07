using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDestroyer : MonoBehaviour
{
    public void ReturnToPoolMusic()
    {
        ObjectPoolManager.ReturnObjectPool(gameObject);
    }
}
