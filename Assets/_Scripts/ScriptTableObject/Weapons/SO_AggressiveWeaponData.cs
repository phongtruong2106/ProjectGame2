using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAAggressiveWeaponData", menuName ="Data/Weapon Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_weaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;

    public WeaponAttackDetails[] AttackDetails {get => attackDetails; private set => attackDetails = value;}

    private void OnEnable() {
        amountOfAttacks = attackDetails.Length;

        movementSpeed = new float[amountOfAttacks];   

        for(int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }


    }


}
