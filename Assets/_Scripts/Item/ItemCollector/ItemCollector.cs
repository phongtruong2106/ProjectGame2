
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour, IItemCollector
{
    private IItem iitem{get; set;}
    private int item = 0;
    [SerializeField] private int item_Default = 5;
    [SerializeField] private Text Item_Text;
    [SerializeField] private Text Item_Default_Text;


    public void CollectItem(Item item)
    {
        item.Collect();
        this.item++;
        Item_Text.text = "Item: " + this.item;
        Item_Default_Text.text = " / " + item_Default;
        if(this.item == item_Default)
        {
            Debug.Log("Congratulations! You collected all the items.");
        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();

            if(item != null)
            {
                CollectItem(item);
                Destroy(collision.gameObject);
            }
        }
    }

 
}
