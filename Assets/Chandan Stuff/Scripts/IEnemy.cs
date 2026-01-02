using UnityEngine;
using System.Collections;

public interface IEnemy
{
    int ID { get; }
    void Die();
}
