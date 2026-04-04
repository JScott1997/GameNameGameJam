using System.Collections;
using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpread;
    [SerializeField] private int pelletCount;
    [SerializeField] private float pumpDelay;
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

    public void Shoot()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            BulletController instance = ObjectPooler.DequeueObject<BulletController>("Bullet");
            instance.gameObject.SetActive(true);

            //find a random spread
            float spreadX = Random.Range(-bulletSpread, bulletSpread);
            float spreadY = Random.Range(-bulletSpread, bulletSpread);

            //set spread according to bullet spawn position and rotation
            Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0);

            instance.Inintialize(bulletSpawn, spreadRotation);
        }
    }

    IEnumerator DelayedShot()
    {
        canShoot = false;
        yield return new WaitForSeconds(pumpDelay);
        canShoot = true;
    }
}
