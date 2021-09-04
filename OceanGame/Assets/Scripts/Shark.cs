using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Shark : MonoBehaviour
{
	public int health = 50;
    public int sightRange = 1000;
    public Transform targetPos;
    public Transform eyePos;

    public Animator sharkAnim;

    //Shark variables
    private float speed = 1;

    private void Update()
    {
        SharkEyes();
    }
    public void TakeDamage(int dmg)
	{
		health -= dmg;
		
		if (health <= 0)
			Die();
	}

    void SharkEyes()
    {
        Ray sightRay = new Ray(eyePos.position, eyePos.right);
        RaycastHit hit;

        if (Physics.Raycast(sightRay, out hit, sightRange))
            Attack();
        else
            Patrol();

    }

	void Die()
	{
		Destroy(gameObject);
	}
    void Patrol()
    {
        sharkAnim.SetBool("IsAttack", false);
        speed = 1;
        transform.position = Vector3.Lerp(transform.position, targetPos.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        sharkAnim.SetBool("IsAttack", true);
        speed = 5;
        transform.position = Vector3.Lerp(transform.position, targetPos.position, speed * Time.deltaTime);
    }
}
