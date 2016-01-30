using UnityEngine;
using System.Collections;

public class SphereSpawner : MonoBehaviour
{
    public GameObject sphere;
    private int i = 100;
    private int z = 0;

    // Update is called once per frame

    void Start() {
    }

    void FixedUpdate()
    {
        z++;
        if (z % i == 0)
        {
            SpawnSphere();
        }

    }

    void SpawnSphere()
    {
        Instantiate(sphere, transform.position, transform.rotation);
    }

}
