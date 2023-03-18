using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;

    //Queue la hang doi , collection luu tru phan tu theo nguyen lý FIFO (First In First Out)
    private Queue<GameObject> availableObject = new Queue<GameObject>();
    public static PlayerAfterImagePool Instance {get; private set;}
    
    private void Awake() {
        Instance = this;
        GrowPool();
    }
    private void GrowPool(){
        for(int i = 0; i < 10; i++){
            var instenceToAdd = Instantiate(afterImagePrefab);
            instenceToAdd.transform.SetParent(transform);
            AddToPool(instenceToAdd);
        }
    }

    public void AddToPool(GameObject instance){
        instance.SetActive(false);
        //them gia tri vao hang doi
        availableObject.Enqueue(instance);
    }

    public GameObject GetFromPool(){
        if(availableObject.Count == 0){
            GrowPool();
        }

        var instance = availableObject.Dequeue(); //lay gia tri tu Queue
        instance.SetActive(true); //xet cac gia tri vua lay thanh true
        return instance; //tra ve gia tri da lay
    }
}
