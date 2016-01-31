using UnityEngine;
using System.Collections;

public class TriggerObjectCreation : MonoBehaviour {

    public GameObject prefab;
    public int distance;
    private BoxCollider triggerBox;
    private int lastSpawn = 100;

    // Use this for initialization
    void Start () {
        triggerBox = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lastSpawn++;
    }

    void OnTriggerEnter(Collider other)
    {
        if (lastSpawn > 20 && other.tag.Equals("Player"))
        {
            lastSpawn = 0;
            GameObject pizzaKurrier = (GameObject)Instantiate(prefab, triggerBox.transform.position + new Vector3(distance, 0, 0), Quaternion.identity);
        }
    }
}
