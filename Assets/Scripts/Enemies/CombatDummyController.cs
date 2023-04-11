using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    //tao max health
    [SerializeField] 
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;
    [SerializeField]
    private bool applyKnockback;
    //tuan tu hoa  doi tuong game danh 
    [SerializeField]
    private GameObject hitParticle;
    
    private float currentHealth, knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private PlayerControls pc;

    //tham chieu 3 doi tuong 
    private GameObject aliveGO, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start() 
    {
        currentHealth = maxHealth;

        ///tham nchieu doi tuong nguoi choi
        pc = GameObject.Find("Player").GetComponent<PlayerControls>();
        //tim thay doi tuong con song
        aliveGO = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();
        rbAlive = aliveGO.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Update() {
        CheckKnockback();
    }



    //go nguoc lai
    private void Knockback(){
        knockback = true;
        //knockback bat dau = voi thoi gian do
        knockbackStart = Time.time;
        //van toc cua minh va van toc con song cua chung ta  = 1 vector 2 toc do bat speed x voi doi mat player
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    //kiem tra go 
    private void CheckKnockback(){
        if(Time.time >= knockbackStart + knockbackDuration && knockback){
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    //tao ham die
    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedX);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -playerFacingDirection, ForceMode2D.Impulse);
    }
}
