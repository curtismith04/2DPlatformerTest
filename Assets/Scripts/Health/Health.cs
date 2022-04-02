using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    public Component[] Renderer;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //play hurt sound
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {

                //Play Death Sound

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;
               
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");
               
                dead = true;

                GetComponent<Collider2D>().enabled = false;

                
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>(); //My add

        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            for (int j = 0; j < sprites.Length; j++)
            {
                sprites[j].color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
                sprites[j].color = Color.white;
                yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }

        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}