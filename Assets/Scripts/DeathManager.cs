using UnityEngine;

public class DeathManager : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void HitDeathPart()
    {
        FindObjectOfType<GameManager>().ShowGameOverUI();
    }
}
