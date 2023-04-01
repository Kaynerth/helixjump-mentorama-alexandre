using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector3 startRotation;

    [SerializeField] Transform topTransform;
    [SerializeField] Transform goalTransform;

    [SerializeField] GameObject helix;
    [SerializeField] GameObject helixLevelPrefab;
    [SerializeField] GameObject ballTrail;

    public List<StageManager> allStages = new List<StageManager>();

    float helixDistance;

    List<GameObject> spawnedLevels = new List<GameObject>();

    [HideInInspector] public int currentStage = 0;

    void Start()
    {
        startRotation = helix.transform.eulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);

        LoadStage(0);
    }

    public void NextLevel()
    {
        if (currentStage < allStages.Count - 1)
        {
            currentStage++;
            FindObjectOfType<BallManager>().ResetBall();
            LoadStage(currentStage);
        }
        else
        {
            ShowGameOverUI();
        }
    }

    void LoadStage(int stageNumber)
    {
        StageManager stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null)
        {
            Debug.LogError("No stage " + stageNumber + " found in stages. Please verify if there are any stages assigned in the list!");
            return;
        }

        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;

        FindObjectOfType<BallManager>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;
        ballTrail.GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        helix.transform.eulerAngles = startRotation;

        foreach (GameObject gobj in spawnedLevels)
        {
            Destroy(gobj);
        }

        float floorDistance = helixDistance / stage.floors.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.floors.Count; i++)
        {
            spawnPosY -= floorDistance;
            GameObject level = Instantiate(helixLevelPrefab, helix.transform);
            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.floors[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageFloorsColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            List<GameObject> deathParts = new List<GameObject>();

            while (deathParts.Count < stage.floors[i].deathPartCount)
            {
                GameObject randomPart = leftParts[(Random.Range(0, leftParts.Count))];
                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathManager>();
                    deathParts.Add(randomPart);
                }
            }
        }
    }

    public void ShowGameOverUI()
    {
        GameObject.Find("UIManager").GetComponent<PauseManager>().OnGameOver();
    }
}
