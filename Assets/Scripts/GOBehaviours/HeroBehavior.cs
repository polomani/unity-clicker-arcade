using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour {

    public SkinObject skin;
    public GameObject bulletPrefab;
    private int layerMask;
    public float bulletSpeed = 20;
    public GameObject[] skinObjects;

    private void Awake()
    {
        Director.Hero = this;
        ActivateSkin(Repository.Data.HeroSkin);
    }

    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("ShootingLayer");
    }

	void Update () {
        if (Input.GetMouseButtonDown(0) && Director.CanShoot())
        {
            GameObject bullet;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50, layerMask))
            {
                bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
                bullet.GetComponent<Rigidbody>().velocity = (hit.point - transform.position).normalized * bulletSpeed;
            }
        }
	}

    void OnCollisionEnter(Collision col)
    {
        Destroy(col.collider.gameObject);
        Director.HeroDied();
    }

    void DeactivateSkins()
    {
        foreach (GameObject skinObject in skinObjects)
        {
            skinObject.SetActive(false);
        }
    }

    public void ActivateSkin(int number)
    {
        DeactivateSkins();
        skinObjects[number].SetActive(true);
    }
}
