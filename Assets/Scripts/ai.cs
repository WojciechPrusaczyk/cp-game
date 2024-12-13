using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public UnitSpawner spawner;
    public List<GameObject> unitPrefabs;
    public float enemyTimerCoolDown = 1f;
    public float currTime;
    public List<GameObject> lanes;
    public float dangerousDistance;
    public float closestUnitDistance;
    public List<bool> isTakingDamageFromLane;
    void Start()
    {
        currTime = enemyTimerCoolDown;
        isTakingDamageFromLane = new List<bool>{false, false, false};
    }

    // Update is called once per frame
    void Update()
    {
        currTime -= Time.deltaTime;
        if (currTime < 0)
        {
            SpawnUnit();
            currTime = enemyTimerCoolDown;
        }
    }

    public void SpawnUnit()
    {
        spawner.SpawnEnemyUnit(SelectRandomUnit(), CheckDanger(), false);
    }

    public int SelectRandomLaneIndex()
    {
        return Random.Range(0, 3);
    }

    public int SelectRandomUnit()
    {
        int r =  Random.Range(0, unitPrefabs.Count);
        return r;
    }

    public int CheckDanger()
    {
        int max = 0;
        int laneIndex = 0;
        List<int> differences = new List<int>{0, 0, 0};
        foreach (GameObject lane in lanes)
        {
            Lane laneComponent = lane.GetComponent<Lane>();
            differences[lanes.IndexOf(lane)] = laneComponent.lanePlayerAmount - laneComponent.laneEnemyAmount;
            
        }
        max = Mathf.Max(differences.ToArray());
        laneIndex = differences.IndexOf(max);
        
        
        return laneIndex;
    }
}