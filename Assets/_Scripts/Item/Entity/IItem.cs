
using UnityEngine;

public class IItem : MonoBehaviour, Item
{
    private string item_name;

    public void Collect()
    {
        Debug.Log("collected " + item_name + "item.");
    }
}
