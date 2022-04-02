using UnityEngine;

public class HitBox : MonoBehaviour
{
    private EnemyBehaviour enemyParent;
 
    private bool impact;


    public void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehaviour>();
     
    }


    private void OnTriggerEnter2D(Collider2D collider)//Collider2D collider
    {
        if (collider.gameObject.CompareTag("Player"))//if(colliderName == "hitBox" && other.tag == "Player")
        {
           
            enemyParent.ScoreHit();
         
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            return;
        }
    }


}// end main
