using System;
using System.Collections;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int pelletCount;
    [SerializeField] private float pumpDelay;
    [SerializeField] private float pelletForce;
    [SerializeField] private float pelletRange;
    private bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DelayedShot());
            Shoot();
        }
    }

    /*
     * This method contains commented out physical bullet logic that is not currently being used
     */
    public void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            //BulletController instance = ObjectPooler.DequeueObject<BulletController>("Bullet");
            //instance.gameObject.SetActive(true);

            //find a random spread
            float spreadX = UnityEngine.Random.Range(-bulletSpread, bulletSpread);
            float spreadY = UnityEngine.Random.Range(-bulletSpread, bulletSpread);

            //set spread according to bullet spawn position and rotation
            Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);
            Vector3 spreadDirection = bulletSpawn.rotation * spreadRotation * Vector3.forward;

            if (Physics.Raycast(bulletSpawn.position, spreadDirection, out RaycastHit hit, pelletRange))
            {
                Debug.DrawLine(bulletSpawn.position, hit.point, Color.red, 1f);
                if (hit.collider.GetComponentInParent<EnemyController>() != null)
                {
                    hit.collider.GetComponentInParent<EnemyController>().ToggleRagdoll();
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForceAtPosition(pelletForce * transform.forward, hit.point);
                }
            }

            //instance.Inintialize(bulletSpawn, spreadRotation);
        }
    }

    IEnumerator DelayedShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(pumpDelay);
        canShoot = true;
    }
}
