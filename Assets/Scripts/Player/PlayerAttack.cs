using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Ranged Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Generic Attack Info")]
    public Transform meleeAttackPoint;
    public float meleeAttackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Light Attack")]
    public int lteAtkDamage = 2;
    public bool madeLightAttack = false;

    [Header("Heavy Attack")]
    public int hvyAtkDamage = 2;

    [Header("Animator Info")]
    public Animator anim;

    [Header("Player Movement")]
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            RangedAttack();
        else if (Input.GetKeyUp(KeyCode.Q))
           // madeRangedAttack == false;

        if (Input.GetKeyDown(KeyCode.W) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            LiteAttack();

        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            HeavyAttack();

        cooldownTimer += Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        //id like to say if melee point collides with enemy and the type of damage is light, then run the light dfamage function
        if(collision.gameObject.tag == "Enemy")
        {

        }
    }

    public void LiteAttack()
    {
        //play attack anim
        anim.SetTrigger("lteAtk");
        cooldownTimer = 0;




    }
    private void HeavyAttack()
    {
        //play attack anim
        anim.SetTrigger("hvyAtk");
        cooldownTimer = 0;

        //Detect enemies in range of attack
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRange, enemyLayers);

     

        //Damage them
    /*    foreach (Collider2D enemy in hitEnemies)
        {
        
            enemy.GetComponent<EnemyHealth>().TakeDamage(hvyAtkDamage);
        
        }*/
    }

    private void RangedAttack() //ranged
    {
        anim.SetTrigger("rngAtk");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    public void OnDrawGizmosSelected()
    {
        if (meleeAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackRange);
    }


    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}