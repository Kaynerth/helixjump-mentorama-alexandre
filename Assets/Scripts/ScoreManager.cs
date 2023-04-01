using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] TMP_Text stageText;

    int score;
    int bestScore;
    int stage;

    private void Start()
    {
        score = 0;

        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        stage = GameObject.Find("GameManager").GetComponent<GameManager>().currentStage;
    }

    void Update()
    {
        stage = GameObject.Find("GameManager").GetComponent<GameManager>().currentStage;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }

        scoreText.text = score.ToString();
        bestScoreText.text = bestScore.ToString();
        stageText.text = stage.ToString();
    }

    public void AddScore(int value)
    {
        score += value;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
    }
}
