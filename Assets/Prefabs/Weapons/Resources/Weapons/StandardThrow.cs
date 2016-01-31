using UnityEngine;

public class StandardThrow : MonoBehaviour
{
    public GameObject player;
    public bool directionRight;
    public float thrust = 60.0f;
    public float rotation = 3f;
    private Vector3 direction;
    private Vector3 offset;
    private Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (directionRight)
        {
            direction = new Vector3(1.0f, 1.0f, 0.0f);
            offset = new Vector3(1.0f, 1.0f, 0.0f);
        }
        else
        {
            direction = new Vector3(-1.0f, 1.0f, 0.0f);
            offset = new Vector3(-2.0f, 1.0f, 0.0f);
        }

        gameObject.transform.position = player.gameObject.transform.position + offset;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(direction * thrust, ForceMode.Impulse);
        rigidBody.AddTorque(new Vector3(0, 0, rotation), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            PlayerBehaviourScript player = collision.gameObject.GetComponent<PlayerBehaviourScript>();
            player.menu.AddDirt(30);

            Destroy(gameObject);
        }
    }
}
