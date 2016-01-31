using UnityEngine;
using System.Collections;

public class TriggerObjectCreation : MonoBehaviour {

    public GameObject prefab;
    public int distance;
    private BoxCollider triggerBox;

	// Use this for initialization
	void Start () {
        triggerBox = gameObject.GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameObject pizzaKurrier = (GameObject)Instantiate(prefab, triggerBox.transform.position + new Vector3(distance, 0, 0), Quaternion.identity);
        }
    }
}
