using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject cameraObj;
    private Transform targetPos;
    public Transform camPointMechanic, camPointSecurity, camPointMain, camPointBoss, camPointPlay;
    public float moveSpeed = 8f, rotateSpeed = 3f;

    [Header("UI_Dependencies")]
    //Scrap(Armour)
    public GameObject[] armourCounters;
    public Text scrapCounter;
    private int armourLevel;
    private int scrapCount;

    //Crate(Damage)
    public GameObject[] damageCounters;
    public Text crateCounter;
    private int damageLevel;
    private int crateCount;

    //Main Mission
    public GameObject[] missionCounters;
    private int missionLevel;
    public Text foodCounter;
    private int foodCount;

    private void Awake()
    {
        targetPos = camPointMain;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Escape))
            ResetCamera();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeCamera("Mechanic");
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeCamera("Security");
        if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeCamera("Boss");
        if (Input.GetKeyDown(KeyCode.X))
        {
            scrapCount = 100;
            crateCount = 100;
            foodCount = 100;
        }
        ReadPrefs();
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("PlayerArmour", 0);
        PlayerPrefs.SetInt("PlayerDamage", 0);
        PlayerPrefs.SetInt("Scrap",0);
        PlayerPrefs.SetInt("Crate",0);
        PlayerPrefs.SetInt("Food",0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeCamera(string id)
    {
        switch (id)
        {
            case "Mechanic":
                targetPos = camPointMechanic;
                break;
            case "Security":
                targetPos = camPointSecurity;
                break;
            case "Boss":
                targetPos = camPointBoss;
                break;
            case "Play":
                targetPos = camPointPlay;
                break;
            default:
                targetPos = camPointMain;
                break;
        }
    }

    void ReadPrefs()
    {
        scrapCount = PlayerPrefs.GetInt("Scrap");
        crateCount = PlayerPrefs.GetInt("Crate");
        foodCount = PlayerPrefs.GetInt("Food");
    }
    void WritePrefs()
    {
        PlayerPrefs.SetInt("PlayerArmour", armourLevel);
        PlayerPrefs.SetInt("PlayerDamage", damageLevel);
    }

    void UpdateUI()
    {
        scrapCounter.text = scrapCount.ToString();
        crateCounter.text = crateCount.ToString();
        foodCounter.text = foodCount.ToString();
    }

    public void PruchaseArmourUpgrade()
    {
        if(scrapCount >= 10 && armourLevel < 3)
        {
            armourLevel++;
            scrapCount -= 10;
            WritePrefs();

            for (int i = 0; i < armourLevel; i++)
                armourCounters[i].SetActive(true);

            Debug.Log("Test, u bough shit");
        }
    }

    public void PruchaseDamageUpgrade()
    {
        if (crateCount >= 10 && damageLevel < 3)
        {
            damageLevel++;
            crateCount -= 10;
            WritePrefs();

            for (int i = 0; i < damageLevel; i++)
                damageCounters[i].SetActive(true);

            Debug.Log("Test, u bought shit");
        }
    }

    public void PurchaseFoodUpgrade()
    {
        if (foodCount >= 10 && missionLevel < 3)
        {
            missionLevel++;
            foodCount -= 10;
            WritePrefs();

            for (int i = 0; i < missionLevel; i++)
                missionCounters[i].SetActive(true);

            Debug.Log("Test, u bough shit");
        }
    }

    public void ResetCamera()
    {
        targetPos = camPointMain;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Gameplay");
    }

    void MoveCamera()
    {
        cameraObj.transform.position = Vector3.Lerp(cameraObj.transform.position, targetPos.position, moveSpeed * Time.deltaTime);
        cameraObj.transform.rotation = Quaternion.Lerp(cameraObj.transform.rotation, targetPos.rotation, rotateSpeed * Time.deltaTime);
    }


}
