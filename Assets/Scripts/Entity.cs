using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityState
{
    Idle,
    Moving,
    Attacking,
    Dead,
    NoTarget,
}
public class Entity : MonoBehaviour
{
    public EntityState state = EntityState.Idle;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public float moveSpeed = 5f;
    public int damage = 100;
    public float attackSpeed  = 1.5f;
    public float attackRange  = 1.5f;
    public float resistance = 0.5f;
    public int cost = 50;
    public int prize = 30;

}
