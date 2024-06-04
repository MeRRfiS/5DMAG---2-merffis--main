using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiModel : EnemyModel
{
    public ZombiModel()
    {
        Health = 10;
        Damage = 5;
        Reward = 5;
        ChaseDistance = 5;
    }
}
