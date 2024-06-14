using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class DohShooting : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Adaptative_pool pool;
    [SerializeField] float Doh_bullet_speed;

    private void Awake()
    {
        
    }
    public void GetBullet(Vector3 position, Transform target)
    {
        DohShot(position, target);
    }
    void DohShot(Vector3 position, Transform target)
    {
        GameObject bullet = pool.GetPoolBullet();
        bullet.SetActive(true);
        bullet.transform.position = position;
        Vector3 bulletDir = target.position - position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDir.normalized * Doh_bullet_speed;
    }



}
