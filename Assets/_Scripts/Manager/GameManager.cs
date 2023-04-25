using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
     private Transform respawnPoint; //diem hoi sinh
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float respawnTime; //thoi gian ho sinh

    private float respawnTimeStart;

    private bool respawn;
    private CinemachineVirtualCamera CVC;

    private void Start() {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update() {
        CheckRespawn();
    }
   public void Respawn(){
    //thoi gian hoi sinh bat dau bang voi thoi gian trong game
    respawnTimeStart = Time.time;
    respawn = true;
    }

    private void CheckRespawn()
    {
        //neu thoi gian lon hon hoac bang thoi gian hoi sinh bat dau  + voi thoi gian hoi sinh
       if(Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp =  Instantiate(player, respawnPoint);
            playerTemp.transform.position = respawnPoint.position; // di chuyển player tới respawnPoint
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }
}
