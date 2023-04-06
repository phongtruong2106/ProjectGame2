using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core core;


    protected virtual void Awake() {
        //dinh nghia core
        core = transform.parent.GetComponent<Core>();

        if(core == null)
        {
            Debug.LogError("There is no Core on the parent");
        }
    }
}
