using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedTextItem : IItem
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    

}
