using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public override void Kill()
    {
        base.Kill();
        //Spawn sprite
        //Play sound
        Destroy(gameObject);
    }
}
