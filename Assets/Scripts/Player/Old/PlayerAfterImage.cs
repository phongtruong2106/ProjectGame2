using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImage : MonoBehaviour
{
    //theo doi thoi gian hoat dong
    [SerializeField]
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;
    //kich hoat alpha cho doi tuong player
    [SerializeField]
    private float alphaSet = 0.8f;
    [SerializeField]
    private float alphaDecay = 0.85f;
    //private float alphaMutilier = 0.85f;
    //tham chieu den doi tuong Player
    private Transform player;

    private SpriteRenderer SR;
    private SpriteRenderer playerSR;

    private Color color;

    private void OnEnable() {
        //tham chieu doi tuong
        SR = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform; //o day dung Object KHONG dung Objects . Right
        playerSR = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = playerSR.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
    }

    private void Update() {
        alpha -= alphaDecay * Time.deltaTime;
        //pha mau voi mau bang mau moi bang cach su dung tham so 1 1 1
        color = new Color(1f, 1f, 1f, alpha);
        //dat mau nay thanh sprite cua minh
        SR.color = color;

        //neu timer lon hon hoac bang thoi gian duoc kich hoat voi thoi gian hoat dong
        if(Time.time >= (timeActivated + activeTime)){
                //tro loi pool
                PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
