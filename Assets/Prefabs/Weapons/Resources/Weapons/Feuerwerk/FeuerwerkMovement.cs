using UnityEngine;

public class FeuerwerkMovement : MonoBehaviour
{
    public GameObject player;
    public bool directionRight;
    public float thrust = 60.0f;
    private Vector3 direction;
    private Vector3 offset;
    private Rigidbody rigidBody;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        if (directionRight)
        {
            direction = new Vector3(1.0f, 0.0f, 0.0f);
            offset = new Vector3(1.0f, 1.0f, 0.0f);
        }
        else
        {
            direction = new Vector3(-1.0f, 0.0f, 0.0f);
            offset = new Vector3(-2.0f, 1.0f, 0.0f);
        }

        if (!directionRight)
        {
            SpriteRenderer spriterenderer = gameObject.GetComponent<SpriteRenderer>();
            Vector3 scale = spriterenderer.transform.localScale;
            scale.x *= -1;
            spriterenderer.transform.localScale = scale;
        }

        gameObject.transform.position = player.gameObject.transform.position + offset;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(direction * thrust, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}

