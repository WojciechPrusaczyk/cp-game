using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public UnitSpawner spawner;
    public List<GameObject> playerUnits;
    public List<GameObject> enemyUnits;
    public int lanePlayerAmount;
    public int laneEnemyAmount;

    public bool canSpawn = true;
    public bool canSpawnEnemy = true;
    public float currTime = 0;
    
    public Queue<QueueObject> unitsQueue = new Queue<QueueObject>();
    void Start()
    {
        currTime = spawner.timerCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (unitsQueue.Count <= 0)
        {
            return;
        }
        currTime -= Time.deltaTime;
        if (currTime < 0)
        {
            QueueObject unit =  unitsQueue.Dequeue();
            spawner.SpawnUnit(unit.unitID, unit.laneIndex, true);
        
            currTime = spawner.timerCoolDown;
        }
    }
    public void AddUnitValue(int value, bool isPlayer)
    {
        if (isPlayer)
        {
            lanePlayerAmount += value;
        }
        else
        {
            laneEnemyAmount += value;
        }
    }
    
    public void SubtractUnitValue(int value, bool isPlayer)
    {
        if (isPlayer)
        {
            lanePlayerAmount -= value;
        }
        else
        {
            laneEnemyAmount -= value;
        }
    }
}
