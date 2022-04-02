using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int EnemStartingHealth;
    private int EnemCurrentHealth;

    public PlayerAttack playerAttack;
    private EnemyBehaviour enemyParent;
    private bool impact;
    private Animator anim;


    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehaviour>();
        anim = GetComponentInParent<Animator>();
        EnemCurrentHealth = EnemStartingHealth;
   

    }
    public void TakeDamage(int incDamage)
    {
        print("I took damage" + incDamage);
        EnemCurrentHealth -= incDamage;

        //Play Hurt animatopn
        anim.SetTrigger("hurt");

        if (EnemCurrentHealth <= 0)
            enemyParent.Die();         
           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlyrDamger")
        {
            if (playerAttack.madeLightAttack == true)
                return;
            TakeDamage(1);
        }
    }

}

