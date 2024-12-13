using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    public Lane lane;

    public void OnTriggerEnter2D(Collider2D other)
    {
        lane.canSpawnEnemy = false;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        lane.canSpawnEnemy = true;
    }
}
