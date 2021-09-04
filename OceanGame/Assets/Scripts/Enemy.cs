using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform targetPos;
    public float moveSpeed;

    public GameObject spawnerRef;

    //LootsDrops
    public GameObject[] lootPrefabs;

    //Enemy Stats
    public int health = 50;

    //Attack
    public float fireRate = 0.5f;
    private float nextAttack;
    public GameObject projectile;
    public Transform barrelPos;

    //Raycast Stuff
    public int sightRange = 8;
    public Transform eyePos;
    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("TPOS").transform;
    }

    private void Update()
    {
        PlayerCheck();
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetPos.rotation, moveSpeed * Time.deltaTime);
    }

    void PlayerCheck()
    {
        Ray sightRay = new Ray(eyePos.position, eyePos.forward);
        RaycastHit hit;

        if (Physics.Raycast(sightRay, out hit, sightRange))
        {
            if (hit.collider.tag == "Player")
                Attack();
        }
    }

    void TakeDamage(int dmg)
    {
        if (health <= 0)
            Die();

        health -= dmg;
    }
    void Die()
    {
        int rnd = Random.Range(0, lootPrefabs.Length);
        Instantiate(lootPrefabs[rnd], transform.position, transform.rotation);
        spawnerRef.GetComponent<EnemySpawner>().enemyCount--;
        Destroy(gameObject);
    }

    void Attack()
    {
        if(Time.time < nextAttack)
            return;

        nextAttack = Time.time + fireRate;
        Instantiate(projectile,barrelPos.position,barrelPos.rotation);
    }
}
