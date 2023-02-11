using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    // Start is called before the first frame update
    //tao trang thai mau 
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private GameObject
            deathChunkParticle,
            deathBloodParticle;
    //set Current Health
    private float currentHealth;

    private GameManager GM;

    private void Start() {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //giam health nguoi choi 
    public void DecreaseHealth(float amount) //so luong health phai giam
    {
        currentHealth  -= amount;

        //neu ma health mac dinh nguoi choi giam ve 0 => die
        if(currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die(){
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation); //kich hoat 2 hoat anh chunk and blood;
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        //xoa di object
        Destroy(gameObject);
    }
}
