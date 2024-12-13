using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    public int health = 1000;
    public Side side;
    public BoxCollider2D collider;
    public AI ai;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        ai = GameObject.FindGameObjectWithTag("AI").GetComponent<AI>();
        collider.offset = side == Side.Bottom ? new Vector2(0, 1) : new Vector2(0, -1);;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            var test = GameObject.Find("TransitionController").GetComponent<TransitionController>();
            test.MakeTransition();
        }
}

    public void OnTriggerEnter2D(Collider2D other)
    {
        string tag = Side.Bottom == side ? "Enemy" : "Player";
        if (other.CompareTag(tag))
        {
            int laneID = other.gameObject.GetComponent<Unit>().lane;
            ai.isTakingDamageFromLane[laneID] = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        string tag = Side.Bottom == side ? "Enemy" : "Player";
        if (other.CompareTag(tag))
        {
            int laneID = other.gameObject.GetComponent<Unit>().lane;
            ai.isTakingDamageFromLane[laneID] = false;
        }
    }
    
    public bool DamageFromLane(int laneIndex, bool canDamage)
    {
        return ai.isTakingDamageFromLane[laneIndex] = canDamage;
    }

}
