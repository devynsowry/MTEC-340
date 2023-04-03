using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    // These numbers replicate the gravity of the actual game
    public float gravity = -9.8f;
    public float strength = 5f;
    public float animationSpeed = 0.15f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Constantly animate the bird
        InvokeRepeating(nameof(AnimateSprite), animationSpeed, animationSpeed);
    }

    private void Update()
    {
        if (GameBehavior.Instance.CurrentState == State.Play || GameBehavior.Instance.CurrentState == State.Invincible)
        {
            // If player clicks or presses space, move the bird up
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                direction = Vector3.up * strength;
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    direction = Vector3.up * strength;
                }
            }

            // Update bird position using deltaTime
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;
        }
    }

    private void AnimateSprite()
    {
        // Iterate through different bird sprites (animation)
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If bird collides with obstacle, game over, if not, increase score
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Invincible mode!
            if (GameBehavior.Instance.CurrentState == State.Invincible)
            {
                FindObjectOfType<GameBehavior>().IncreaseScore();
            }
            else
            {
                FindObjectOfType<GameBehavior>().GameOver();
            }
        }
        else if (collision.gameObject.CompareTag("Scoring"))
        {
            FindObjectOfType<GameBehavior>().IncreaseScore();
        }
        else if (collision.gameObject.CompareTag("Boost"))
        {
            FindObjectOfType<GameBehavior>().Boost();
            Destroy(GameObject.Find("Boost(Clone)"));
        }
        else if (collision.gameObject.CompareTag("Invincible"))
        {
            StartCoroutine(GameBehavior.Instance.Invincible());
            Destroy(GameObject.Find("Invincible(Clone)"));
        }
    }

    public void ResetPosition()
    {
        // Reset position of bird
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
}
