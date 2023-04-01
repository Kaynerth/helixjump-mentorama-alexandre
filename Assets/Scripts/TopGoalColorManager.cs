using UnityEngine;

public class TopGoalColorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPieces;

    void Start()
    {
        for (int i = 0; i < floorPieces.Length; i++)
        {
            if (Mathf.Abs(i) % 2 == 0)
            {
                floorPieces[i].GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                floorPieces[i].GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }
}
