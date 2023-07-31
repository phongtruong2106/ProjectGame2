
using UnityEngine;

public class IItem : MonoBehaviour, Item, IDDataPersistence
{
    
    [SerializeField] private string id;
    [SerializeField] private bool collected = false;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    
    private string item_name;

    public void Collect()
    {
        Debug.Log("collected " + item_name + "item.");
    }

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if(data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        data.coinsCollected.Add(id, collected);
    }
    

}
