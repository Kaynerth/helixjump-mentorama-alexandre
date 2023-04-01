using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] float bounceForce;

    [SerializeField] GameObject splashPrefab;

    public AudioSource effectsAudioSource;
    [SerializeField] AudioClip bounceSound;
    public AudioClip breakSound;

    Vector3 startPosition;

    GameManager gameManager;

    [HideInInspector] public int comboMultiplier = 1;

    public bool isSuperSpeedActive;

    bool isAbleToCollide;

    void Awake()
    {
        startPosition = transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isAbleToCollide = true;
    }

    void Update()
    {
        if (comboMultiplier >= 4 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 5, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isAbleToCollide)
        {
            if (isSuperSpeedActive)
            {
                if (other != null)
                {
                    other.gameObject.GetComponentInParent<ScoreCheck>().OnTriggerEnter(this.gameObject.GetComponent<Collider>());
                }
            }
            else
            {
                DeathManager deathPart = other.transform.GetComponent<DeathManager>();
                if (deathPart)
                {
                    deathPart.HitDeathPart();
                }
            }

            effectsAudioSource.PlayOneShot(bounceSound);

            isAbleToCollide = false;
            Invoke("AllowCollision", 0.3f);

            rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);

            GameObject newSplash = Instantiate(splashPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.24f, transform.position.z), splashPrefab.transform.rotation);
            newSplash.GetComponent<Renderer>().material.color = gameManager.allStages[gameManager.currentStage].stageBallColor;
            newSplash.transform.localScale = Vector3.one * Random.Range(0.3f, 0.5f);
            newSplash.transform.parent = other.transform;

            comboMultiplier = 1;
            isSuperSpeedActive = false;
        }
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }

    void AllowCollision()
    {
        isAbleToCollide = true;
    }
}
