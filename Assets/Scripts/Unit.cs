using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum Side
{
    Top,
    Bottom,
}
public class Unit : MonoBehaviour
{
    public Entity state;
    public UnitSpawner unitSpawner;
    public Side side;
    public int lane;
    public Transform target;
    private SpriteRenderer _spriteRenderer;

    public Vector2 direction;

    public float allyCheckDistance = .01f;
    public Transform eyes;
    
    // Layers for detection
    public LayerMask enemyLayer;
    public LayerMask allyLayer;

    private Coroutine _attackCoroutine;
    public Castle castle;

    private Animator unitAnimator;
    public int attackEffectSoundIndex;
    
    public GameObject audioManagerObject;
    [SerializeField] private AudioManager audioManager;
    
    private void Awake()
    {
        unitAnimator = GetComponentInChildren<Animator>();
        audioManagerObject = GameObject.Find("AudioManager");
        audioManager = audioManagerObject.GetComponent<AudioManager>();
    }

    void Start()
    {
        // get components
        state = GetComponent<Entity>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        unitSpawner = GameObject.FindFirstObjectByType<UnitSpawner>();

        InitializeUnit();
    }

    // set values depending on unit side
    void InitializeUnit()
    {
        state.currentHealth = state.maxHealth;
        if (side == Side.Top)
        {
            direction = Vector2.down;
            gameObject.tag = "Enemy";
            target = unitSpawner.TopLanePoints[lane + 3];
            castle = GameObject.Find("PlayerCastle").GetComponent<Castle>();
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            AddLayer(ref allyLayer, LayerMask.NameToLayer("Enemy"));
            AddLayer(ref enemyLayer, LayerMask.NameToLayer("Player"));
            eyes.localPosition= new Vector3(eyes.localPosition.x, eyes.localPosition.y * -1, eyes.localPosition.z);
        }
        else
        {
            direction = Vector2.up;
            gameObject.tag = "Player";
            target = unitSpawner.bottomLanePoints[lane + 3];
            castle = GameObject.Find("EnemyCastle").GetComponent<Castle>();
            gameObject.layer = LayerMask.NameToLayer("Player");
            AddLayer(ref allyLayer, LayerMask.NameToLayer("Player"));
            AddLayer(ref enemyLayer, LayerMask.NameToLayer("Enemy"));

        }
    }

    void Update()
    {
        // check for targer (currently end of line)
        if (target == null)
        {
            state.state = EntityState.NoTarget;
            return;
        }
        
        if (EnemyInRange())
        {
            state.state = EntityState.Attacking;
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(Attack());
            }
            return;
        }

        if (CanMove())
        {
            state.state = EntityState.Moving;
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * state.moveSpeed);
        }
    }
    
    public void AddLayer(ref LayerMask mask, int layer)
    {
        mask |= (1 << layer);
    }
    
    bool CanMove()
    {
        // checks if ray detects ally unit
        RaycastHit2D hit = Physics2D.Raycast(eyes.position, direction, allyCheckDistance, allyLayer);
        Debug.DrawRay(eyes.position, direction * allyCheckDistance, Color.blue);

        if (hit.collider != null)
        {
            return false;
        }
        return true;
    }
    
    bool EnemyInRange()
    {
        // checks if ray detects enemy unit
        RaycastHit2D hit = Physics2D.Raycast(eyes.position, direction, state.attackRange, enemyLayer);
        Debug.DrawRay(eyes.position, direction * state.attackRange, Color.red);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
    
    IEnumerator Attack()
    {
        // unit attacks using a timer set to state.attackSpeed
        while (true)
        {
            audioManager.SetEffectsClip(attackEffectSoundIndex);
            
            unitAnimator.SetBool("isAttacking", true);
            
            RaycastHit2D hit = Physics2D.Raycast(eyes.position, direction, state.attackRange, enemyLayer);
            if (hit.collider == null)
            {
                unitAnimator.SetBool("isAttacking", false);
                yield break;
            }
            if (hit.collider.CompareTag("Castle"))
            {
                GameObject castle = hit.collider.gameObject;
                Castle castleComponent = castle.GetComponent<Castle>();
                castleComponent.health -= state.damage;
                castleComponent.DamageFromLane(lane, true);

            }
            if (!hit.collider.CompareTag("Castle"))
            {
                Unit enemyUnit = hit.collider.GetComponent<Unit>();
                if (enemyUnit != null)
                {
                    enemyUnit.TakeDamage(state.damage);
                }
            }
            yield return new WaitForSeconds(state.attackSpeed);
            unitAnimator.SetBool("isAttacking", false);
        }
    }
    
    public void TakeDamage(float amount)
    {
        // hurt unit applying resistance 
        state.currentHealth -= (int)(amount * state.resistance);
        UpdateColorBasedOnHealth();
        if (state.currentHealth <= 0)
        {
            Die();
        }
    }
    
    void UpdateColorBasedOnHealth()
    {
        // visualize unit health (placeholder)
        float healthPercentage = (float)state.currentHealth / state.maxHealth;
        _spriteRenderer.color = new Color(1f, healthPercentage, healthPercentage);
    }

    void Die()
    {
        // kill unit, can add effects before death
        state.state = EntityState.Dead;
        
        
        Destroy(gameObject);
    }

    public void SetTarget(Transform laneEndPoint)
    {
        target = laneEndPoint;
    }
    void OnMouseDown()
    {
        Die();
    }

    private void OnDestroy()
    {
        bool s = false;
        if (side == Side.Bottom)
        {
            s = true;
            unitSpawner.enemyWallet.AddMoney(state.prize);
        }
        else
        {
            s = false;
            unitSpawner.playerWallet.AddMoney(state.prize);
        }
        unitSpawner.lanes[lane].playerUnits.Remove(this.gameObject);

        if (null != castle)
        {
            Castle castleComponent = castle.GetComponent<Castle>();
            castleComponent.DamageFromLane(lane, true);
        }
    }
}
