using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    public float gravity = -9.8f;
    [SerializeField] private float jumpStrength = 5f;
    public Sprite[] sprites;
    private int spriteIndex;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * jumpStrength;
        }

        // mobile get touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // just touched the screen
            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * jumpStrength;
            }
        }

        // because is kinematic you need to move the object down
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        var rot = transform.rotation;
        if (direction.y > 0)
        {
            rot.z = direction.y * 0.02f;
            transform.rotation = rot;
        }
        else
        {
            rot.z = direction.y * 0.02f;
            transform.rotation = rot;
        }
    }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        direction = Vector3.zero;
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var tag = otherCollider.gameObject.tag;
        if (tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }

    }

}
