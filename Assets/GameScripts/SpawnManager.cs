using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    private float SpawnRangeZ = 17;
    //private float SpawnPosX = 45;
    private float startDelay = 0.5f;
    private float SpawnInterval = 1.5f;
    private float FastSpawnInterval = 0.5f;
    private float SlowSpawnInterval = 6.0f; // New variable for "Shield" items
    private PlayerController playerController;

    public GameObject[] itemPrefabs;

    private bool isShieldActive = false; // New variable to track shield status

    public bool isGameStarted = false;
    public TMP_Text countdownText;

    public GameObject countdownCanvas;

   private static bool restarting = false;
    public GameObject beginningCanvas;   // drag the intro canvas here

    public GameObject UIStat;
    
    void Awake()
    {
        // If we came from a restart, skip intro UI
        if (restarting)
        {
            beginningCanvas.SetActive(false);
            UIStat.SetActive(true);
        }
    }
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        if (restarting)
        {
            // launch straight into countdown
            StartCoroutine(StartCountdown());
            restarting = false;   // reset for future runs
        }
    }

      public void HandleRestart()
    {
        restarting = true;   // remember intent
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartRunning()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(StartCountdown());
    }
    


    private IEnumerator StartCountdown()
    {
        int countdown = 3;
        countdownCanvas.SetActive(true);
        countdownText.gameObject.SetActive(true);

        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);
        countdownCanvas.SetActive(false);
        isGameStarted = true;

        // Existing start logic
        InvokeRepeating("SpawnRandomItems", startDelay, SpawnInterval);
    }
    void SpawnRandomItems()
    {
        if (playerController.gameOver == true)
        {
            return;
        }
        if (isShieldActive)
        {
            // If shield is active, do not spawn Shield items
            Invoke("SpawnRandomItems", SpawnInterval);
            return;
        }

        int objectIndex = Random.Range(0, itemPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(6, 45), 1, Random.Range(14, -SpawnRangeZ));


        if (itemPrefabs[objectIndex].tag == "Obstacle")
        {
            Instantiate(itemPrefabs[objectIndex], spawnPos, itemPrefabs[objectIndex].transform.rotation);
        }
        else if (itemPrefabs[objectIndex].tag == "Shield")
        {
            Invoke("SpawnRandomItems", SlowSpawnInterval);
            Instantiate(itemPrefabs[objectIndex], spawnPos, itemPrefabs[objectIndex].transform.rotation);

            return;
        }
        else
        {
            Instantiate(itemPrefabs[objectIndex], spawnPos, itemPrefabs[objectIndex].transform.rotation);
        }

        // Use the regular interval for all spawns
        Invoke("SpawnRandomItems", SpawnInterval);
    }

    public void TriggerFasterObstacleSpawn()
    {
        // Trigger faster obstacle spawn
        CancelInvoke("SpawnRandomItems"); // Cancel existing invoke
        InvokeRepeating("SpawnRandomItems", 0, FastSpawnInterval); // Start repeating with a faster interval
    }

    public void SetShieldActive(bool active)
    {
        isShieldActive = active;
    }



}
