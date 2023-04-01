using UnityEngine;

public class ScoreCheck : MonoBehaviour
{
    [SerializeField] GameObject[] childFloors;

    ScoreManager scoreManager;
    BallManager multiplierManager;
    GameManager gameManager;

    void Awake()
    {
        scoreManager = GameObject.Find("UIManager").GetComponent<ScoreManager>();
        multiplierManager = GameObject.Find("Ball").GetComponent<BallManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < childFloors.Length; i++)
            {
                childFloors[i].GetComponent<Rigidbody>().isKinematic = false;
                childFloors[i].GetComponent<Rigidbody>().useGravity = true;
                childFloors[i].GetComponent<Rigidbody>().AddRelativeForce(1f, 2f, 3f, ForceMode.Impulse);
            }

            multiplierManager.effectsAudioSource.PlayOneShot(multiplierManager.breakSound);
            scoreManager.AddScore((gameManager.currentStage + 1) * multiplierManager.comboMultiplier);
            multiplierManager.comboMultiplier++;
            Destroy(gameObject, 0.5f);
        }
    }
}
