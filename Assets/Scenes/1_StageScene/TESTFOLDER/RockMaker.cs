using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMaker : MonoBehaviour
{
    public GameObject rockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("setRock", 0.8f);
    }

    void setRock()
	{
        GetComponent<CircleCollider2D>().enabled = false;
        Instantiate(rockPrefab, gameObject.transform.position, Quaternion.identity, null);
        Destroy(gameObject);
    }
}
