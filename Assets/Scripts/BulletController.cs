using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public float speed = 500.0f;
    [SerializeField] private float lifetime = 2.0f;
    [SerializeField] private float range = 50f;

    public void Inintialize(Transform spawn, Quaternion spreadOffset)
    {
        transform.position = spawn.position;
        transform.rotation = spawn.rotation * spreadOffset;

        rb.AddForce(spawn.forward * speed, ForceMode.Impulse);

        //raycast debug
        if(Physics.Raycast(spawn.position,(spawn.rotation*spreadOffset*Vector3.forward), out RaycastHit hit, range))
        {
            Debug.DrawLine(spawn.position, hit.point, Color.red, 1f);
        }
        
        //reset lifetime
        lifetime = 2.0f;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) ObjectPooler.EnqueueObject(this, "Bullet");
    }
}
