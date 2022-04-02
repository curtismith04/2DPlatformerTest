using UnityEngine;

//Greetings from Sid!

//Thank You for watching my tutorials
//I really hope you find my tutorials helpful and knowledgeable
//Appreciate your support.

public class EnemyBehaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Check if Player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    public Health playerHealth;
    public EnemyHealth myHealth;
    public float dealLightDamage;
    public float dealHeavyDamage;
    [HideInInspector] int lightHitCount = 0; //count how many times player has been hit
    #endregion

    #region Private Variables
    private Animator anim;
    private float distance; //Store the distance b/w enemy and player
    private bool attackMode;
    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer;
    #endregion

    void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
        lightHitCount = 0;
    }

    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && ((!anim.GetBool("lteAtk") == true) || (!anim.GetBool("hvyAtk") == true))) //if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_lteAtk") || !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_hvyAtk")) 
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }

        if (lightHitCount >= 6)
            lightHitCount = 0;
    }


    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("lteAtk", false);
            anim.SetBool("hvyAtk", false);
        }
    }

    void Move()
    {
        anim.SetBool("moving", true);


        if ((!anim.GetBool("lteAtk") == true) || (!anim.GetBool("hvyAtk") == true))//if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_lteAtk"))
        {

            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("moving", false);
        DealDamage();

    }

    private void DealDamage()
    {
        if (lightHitCount < 6)
        {
            anim.SetBool("lteAtk", true);

            ++lightHitCount;
            //play sound
        }
        else if (lightHitCount >= 6)
        {
            anim.SetBool("hvyAtk", true);

            lightHitCount = 0;
            //play sound
        }
    }

    public void ScoreHit() //this is called in the hitbox script
    {
        if (lightHitCount < 6)
            playerHealth.TakeDamage(dealLightDamage);
        else if (lightHitCount >= 6)
            playerHealth.TakeDamage(dealHeavyDamage);
    }


    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("lteAtk", false);
        anim.SetBool("hvyAtk", false);

    }


    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }


        transform.eulerAngles = rotation;
    }

    public void Die()
    {

        //die animation
        anim.SetBool("die", true);
        //dioable enemy

        GetComponentInChildren<Collider2D>().enabled = false;
        
        this.enabled = false;
    }
}
