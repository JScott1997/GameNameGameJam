using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 500.0f;
    private float lifetime = 2.0f;

    public void Inintialize()
    {

    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) ObjectPooler.EnqueueObject(this, "Bullet");
    }
}
