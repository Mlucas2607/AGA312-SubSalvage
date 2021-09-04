using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    public int health = 30;
    public float speed = 8f;
    public CharacterController controller;
    private Vector3 direction;
    public GameObject projectile;
    public Transform barrelPos;

    private int torpedoDamage = 10;

    public GameObject movementParticle;

    public float fireRate = 0.5f;
    private float nextAttack;

    //CamController
    public GameObject cameraObj;
    public Transform targetPos;
    public float moveSpeed = 8f;
    public float rotateSpeed = 3f;

    //Upgrades
    private int armourLevel;
    private int damageLevel;

    public GameObject[] armourTiers;

    //Loot
    public int scrapCount;
    public int crateCount;
    public int foodCount;


    //UI
    public GameObject torpedoLoaded;
    public Slider healthSlider;
    private string counterSpacer = "x";
    public Text scrapCounter;
    public Text foodCounter;
    public Text crateCounter;

    void Start()
    {
        armourLevel = PlayerPrefs.GetInt("PlayerArmour");
        damageLevel = PlayerPrefs.GetInt("PlayerDamage");

        for (int i = 0; i < armourLevel; i++)
            armourTiers[i].SetActive(true);

        torpedoDamage = torpedoDamage + (damageLevel * 10);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.X))
            ReturnToMenu();

        //UI
        healthSlider.value = health;
        scrapCounter.text = counterSpacer + scrapCount;
        foodCounter.text = counterSpacer + foodCount;
        crateCounter.text = counterSpacer + crateCount;

        if (Time.time > nextAttack)
            torpedoLoaded.SetActive(true);
        else
            torpedoLoaded.SetActive(false);
    }

    void FixedUpdate()
    {
        Move();
        MoveCamera();
    }

    void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        direction.x = hInput * speed;
        direction.y = vInput * speed;

        controller.Move(direction * Time.deltaTime);

        //Animations
        if (hInput > 0 || hInput < 0 || vInput > 0 || vInput < 0)
            movementParticle.SetActive(true);
        else
            movementParticle.SetActive(false);
    }

    void TakeDamage(int dmg)
    {
        if (health <= 0)
            Die();

        health -= dmg;
    }

    void Die()
    {
        Debug.Log("you are dead");
    }

    void Shoot()
	{
        if (Time.time < nextAttack)
            return;

        nextAttack = Time.time + fireRate;
        Vector3 spwnPos = barrelPos.position;
		Quaternion spwnRot = barrelPos.rotation;
		GameObject torpedo = Instantiate(projectile, spwnPos,spwnRot);
        torpedo.GetComponent<Spear>().dmg = torpedoDamage;
	}

    void ReturnToMenu()
    {
        int newScrapCount = PlayerPrefs.GetInt("Scrap") + scrapCount;
        int newCrateCount = PlayerPrefs.GetInt("Crate") + crateCount;
        int newFoodCount = PlayerPrefs.GetInt("food") + foodCount;

        PlayerPrefs.SetInt("Scrap", newScrapCount);
        PlayerPrefs.SetInt("Crate", newCrateCount);
        PlayerPrefs.SetInt("Food", newFoodCount);

        SceneManager.LoadScene("Menu");
    }

        void MoveCamera()
    {
        cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, targetPos.position, moveSpeed * Time.deltaTime);
        cameraObj.transform.rotation = Quaternion.Lerp(cameraObj.transform.rotation, targetPos.rotation, rotateSpeed * Time.deltaTime);
    }
}
