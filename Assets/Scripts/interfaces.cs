using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void takeDamage(float amount);
}

public interface IInstantiater
{
    void spawnObject(GameObject spawn, Transform spawnPoint);
}

