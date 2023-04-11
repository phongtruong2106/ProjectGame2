using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IknockBackable
{
    void Knockback(Vector2 angle, float strength, int direction);
}
