using UnityEngine;
using System.Collections;

public class RollingImpulse : MonoBehaviour
{

    private Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(- transform.right * 10, ForceMode.Impulse);
    }

}
