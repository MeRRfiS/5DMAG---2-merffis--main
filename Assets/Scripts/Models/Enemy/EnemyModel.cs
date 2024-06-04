using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    public int Health { get; set; }
    public int Damage { get; protected set; }
    public int Reward { get; protected set; }
    public int ChaseDistance { get; protected set; }
}
