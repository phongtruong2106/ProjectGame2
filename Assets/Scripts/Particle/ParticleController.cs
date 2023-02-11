using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    //ket thuc anim
    private void FinishAnim()
    {
        //huy doi tuong tro choi de khi nao chuc nang nay duoc goi
        Destroy(gameObject);
    }
}
