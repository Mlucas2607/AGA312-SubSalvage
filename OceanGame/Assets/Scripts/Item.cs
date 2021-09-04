using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[Header("Item Variables")]
    public string itemType;
    public float rotateSpeed = 10f;
    void Update()
    {
		transform.Rotate(transform.up, rotateSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            IncrementItem(other.gameObject);
            Destroy(gameObject);
        }
    }

    void IncrementItem(GameObject obj)
    {
        switch (itemType)
        {
            case "Food":
                obj.GetComponent<Player>().foodCount++;
                break;
            case "Scrap":
                obj.GetComponent<Player>().scrapCount++;
                break;
            case "Crate":
                obj.GetComponent<Player>().crateCount++;
                break;
            default:
                break;
        }
    }
}
