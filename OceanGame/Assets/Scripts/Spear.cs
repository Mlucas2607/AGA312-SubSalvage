using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
	public int dmg = 10;
	public float speed;
	private Rigidbody rb;
    public GameObject explosionFx;
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		rb.AddForce(transform.forward * speed);
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player")
        {
            other.SendMessage("TakeDamage", dmg);
            Instantiate(explosionFx, transform.position, transform.rotation);
            Destroy(gameObject);
        }

		if (other.tag == "Enemy")
		{
			other.SendMessage("TakeDamage", dmg);

            Instantiate(explosionFx, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		else
            Instantiate(explosionFx, transform.position, transform.rotation);
            Destroy(gameObject);
	}
}
