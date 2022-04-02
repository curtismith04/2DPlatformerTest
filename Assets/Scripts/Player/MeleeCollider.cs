using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCollider : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public Health playerHealth;
    public Animator playerAnim;
    public EnemyHealth enemyHealh;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnmCollider")
        {
            if (playerAttack.meleeAttackPoint) { }
                //I was trying to say that if the melee weapon collides with the enmy colldier and the attack is light
                //deal light damage
                
        }
    }
}
