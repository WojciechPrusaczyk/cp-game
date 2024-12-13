using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class UnitSpawner : MonoBehaviour
{
    public GameObject playerUnitPrefab; // Prefab of the unit to 
    public GameObject enemyUnitPrefab; // Prefab of the unit to 
    
    public GameObject[] units;
    public GameObject selectedUnit;
    
    public Transform[] bottomLanePoints; // Start points for the lanes
    public Transform[] TopLanePoints;   // End points for the lanes
    public List<Lane> lanes;
    
    public Wallet playerWallet;
    public Wallet enemyWallet;
    
    public float timerCoolDown;
    public float currTime;
    [SerializeField]
    public Queue<QueueObject> unitsQueue = new Queue<QueueObject>();
    
    
    private void Start()
    {
        selectedUnit = units[0];
        currTime = timerCoolDown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnUnit(1, 0, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnUnit(2, 1, true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnUnit(3, 2, true);
        }
        
    }

    public void QueueUnit(int unitID, int laneIndex)
    {
        QueueObject q = new QueueObject(laneIndex, unitID);
        lanes[laneIndex].unitsQueue.Enqueue(q);
    }

    public void SpawnUnit(int unitID, int laneIndex, bool isBottom)
    {
        if (!CanBuyUnit(unitID, isBottom))
        {
            return;
        }

        if (!lanes[laneIndex].canSpawn)
        {
            return;
        }
        
        if (unitID > 4)
            return;
        // check for correct lane index
        if (laneIndex < 0 || laneIndex >= bottomLanePoints.Length)
        {
            Debug.LogError("Invalid lane index: " + laneIndex);
            return;
        }
        // assign values to new unit
        Side side = Side.Bottom;
        Transform spawnPosition = bottomLanePoints[laneIndex];
        
        GameObject newUnit = Instantiate(units[unitID - 1], spawnPosition.position, Quaternion.identity);

        if (isBottom)
        {
            GameObject obj = newUnit.transform.Find("SpriteRenderer").gameObject;
            obj.transform.Rotate(180.0f, 0.0f, 0.0f, Space.Self);
        }
        newUnit.name = "BottomUnit " + (laneIndex + 1).ToString();

        Unit unitComponent = newUnit.GetComponent<Unit>();
        
        Lane lane = lanes[laneIndex];
        lane.playerUnits.Add(newUnit);
        newUnit.transform.SetParent(lane.transform);
        lane.AddUnitValue(unitComponent.state.prize, true);
        
        playerWallet.SubtractMoney(unitComponent.state.cost);
        
        if (unitComponent != null)
        {
            unitComponent.side = side;
            unitComponent.lane = laneIndex;
            unitComponent.SetTarget(bottomLanePoints[laneIndex + 3]);
        }
        else
        {
            Debug.LogError("Spawned object does not have a Unit script attached.");
        }
    }
    
    public void SpawnEnemyUnit(int unitID, int laneIndex, bool isBottom)
    {
        if (!CanBuyUnit(unitID, isBottom))
        {
            return;
        }
        if (!lanes[laneIndex].canSpawnEnemy)
        {
            return;
        }
        // check for correct lane index
        if (laneIndex < 0 || laneIndex >= TopLanePoints.Length)
        {
            Debug.LogError("Invalid lane index: " + laneIndex);
            return;
        }
        // assign values to new unit
        Side side = Side.Top;
        Transform spawnPosition = TopLanePoints[laneIndex];
        GameObject newEnemyUnit = Instantiate(units[unitID], spawnPosition.position, Quaternion.identity);
        newEnemyUnit.name = "TopUnit " + (laneIndex + 1);

        Unit unitComponent = newEnemyUnit.GetComponent<Unit>();
        
        Lane lane = lanes[laneIndex];
        lane.enemyUnits.Add(newEnemyUnit);
        newEnemyUnit.transform.SetParent(lane.transform);
        lane.AddUnitValue(unitComponent.state.prize, false);
        
        enemyWallet.SubtractMoney(unitComponent.state.cost);
        
        if (unitComponent != null)
        {
            unitComponent.side = side;
            unitComponent.lane = laneIndex;
        }
        else
        {
            Debug.LogError("Spawned object does not have a Unit script attached.");
        }
    }

    public bool CanBuyUnit(int unitID, bool isBottom)
    {
        if (isBottom)
        {
            if (units[unitID].GetComponent<Entity>().cost <= playerWallet.money)
            {
                return true;
            }

            return false;
        }
        if (units[unitID].GetComponent<Entity>().cost <= enemyWallet.money)
        {
            return true;
        }
        return false;
    }
}
[Serializable]
public struct QueueObject
{
    public int laneIndex;
    public int unitID;

    public QueueObject(int laneIndex, int unitID)
    {
        this.laneIndex = laneIndex;
        this.unitID = unitID;
    }
}