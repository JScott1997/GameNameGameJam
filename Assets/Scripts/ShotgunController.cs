using UnityEngine;

public class ShotgunController : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpread = 5f;
    [SerializeField] private float range = 50f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 1; i++)
            {
                BulletController instance = ObjectPooler.DequeueObject<BulletController>("Bullet");
                instance.gameObject.SetActive(true);
                instance.Inintialize();

                //set position and rotation based on bullet spread
                instance.transform.parent = bulletSpawn;

                //shoot forward
                instance.GetComponent<Rigidbody>().AddForce(transform.forward * instance.speed);
            }
        }
    }
}
