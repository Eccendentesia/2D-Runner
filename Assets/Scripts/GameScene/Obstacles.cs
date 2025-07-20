using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] ObstaclePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int ThresholdScore;
    [SerializeField] private int additionScore; 
    [SerializeField] private float spawnInterval;
    [SerializeField] private float tempTimer;
    [SerializeField] private int minimumDecrement; 
    private void Start()
    {
      tempTimer = spawnInterval;
    }
    private void Update()
    {
        tempTimer -= Time.deltaTime;
        if (tempTimer <= 0)
        {
            spawnPos();
            tempTimer = spawnInterval;
        }
        if( InGameUI.Instance.score >= ThresholdScore && spawnInterval >= minimumDecrement )
        {
            spawnInterval -= 1f;
            ThresholdScore += additionScore ;
        }

    }
    private void spawnPos()
    {
        int index1 = Random.Range(0, spawnPoints.Length);
        int index2;
        do
        {
            index2 = Random.Range(0, spawnPoints.Length);
        } while (index1 == index2);
        spawn(spawnPoints[index1 ], 0 );
        spawn(spawnPoints[index2]  , 1);
    }
    private void spawn(Transform pos , int slot )
    {
        GameObject obst = Instantiate(ObstaclePrefab[Random.Range(0, ObstaclePrefab.Length)], pos.position, Quaternion.identity);
        Debug.Log("Object Spawned successfully ");
    }
   
}
