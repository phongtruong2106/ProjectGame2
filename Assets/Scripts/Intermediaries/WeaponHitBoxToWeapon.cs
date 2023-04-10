using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitBoxToWeapon : MonoBehaviour
{   
    private AggresiveWeapon weapon;
    
    private void Awake() {
        weapon = GetComponentInParent<AggresiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        weapon.AddToDetected(collision); //xac dinh , neu no duoc them vao danh sach
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weapon.RemoveFromDetected(collision);
    }
}
