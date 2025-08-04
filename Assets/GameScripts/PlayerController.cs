using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI gemText;
    private Rigidbody playerRb;
    public float jumpForce = 10.0f;
    public float speed = 10.0f;
    public float horizontalInput;
    public ParticleSystem explosionParticle;
    private AudioSource playerAudiox;
    public ParticleSystem dirtParticle;
    public float gravityModifier;
    public bool gameOver;
    private Animator playerAnim;
    private int gemsCollected;
    private bool isShieldActive = false;
    private float shieldDuration = 5.0f;
    public AudioClip jumpSoundx;
    public AudioClip gemPickUp;
    public AudioClip crashSoundx;
    public AudioClip shieldPickUp;
    //public AudioClip sideToSide;



    public GameObject gameOverCanvas;            // parent canvas that’s hidden by default
    public TextMeshProUGUI goGemText;            // “Gems:”  on the canvas
    public TextMeshProUGUI goTimeText;           // “Time:”  on the canvas
    public TextMeshProUGUI goEfficiencyText;     // “Gems /sec” on the canvas

    private float runTime   = 0f;   // seconds survived
    private bool  handledGO = false;
    public TextMeshProUGUI runTimeText;    // << new: drag a TMP object here

    public SpawnManager spawnManager;

    public GameObject UIStats;
    public void Start()
    {

        //button = GetComponent<Button>();
        //button.onClick.AddListener(StartGame);

        gemsCollected = 0;
        UpdateGemCount();

        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudiox = GetComponent<AudioSource>();

        runTime = 0f;
        handledGO = false;
        gameOverCanvas.SetActive(false);

    }

    void Update()
    {


        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            dirtParticle.Stop();
            playerAudiox.PlayOneShot(jumpSoundx, 1.0f);
        }

        // only tick the timer while the player is alive
        if (spawnManager.isGameStarted)
        {
            if (!gameOver)
            {
                runTime += Time.deltaTime;
                UpdateRunTimeUI();
            }
            else if (!handledGO)     // make sure we run this block once
            {
                HandleGameOver();
            }
        }





    }
    
    private void UpdateRunTimeUI()
{

        
            int minutes = Mathf.FloorToInt(runTime / 60f);
    int seconds = Mathf.FloorToInt(runTime % 60f);
    runTimeText.text = $"{minutes:00}:{seconds:00}";
    

}

    public void HandleGameOver()
    {
        handledGO = true;
        UIStats.SetActive(false);

        // 1) Activate the canvas
        gameOverCanvas.SetActive(true);

        // 2) Fill in the numbers
        goGemText.text = $"Gems: {gemsCollected}";

        // Format time as M:SS
        int minutes = Mathf.FloorToInt(runTime / 60f);
        int seconds = Mathf.FloorToInt(runTime % 60f);
        goTimeText.text = $"Time: {minutes:00}:{seconds:00}";

        // Efficiency = gems per minute (or per sec if you prefer)
        float gemsPerSec = gemsCollected / Mathf.Max(runTime, 0.01f);
        goEfficiencyText.text = $"Efficiency: {gemsPerSec:F2} gems/sec";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!isShieldActive)
            {
                gameOver = true;
                Debug.Log("Game Over");
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                playerAudiox.PlayOneShot(crashSoundx, 1.0f);
            }
            else
            {
                // Destroy the obstacle on impact
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            ActivateShield();
            playerAudiox.PlayOneShot(shieldPickUp, 1.0f);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            gemsCollected++;
            playerAudiox.PlayOneShot(gemPickUp, 1.0f);
            UpdateGemCount();
            Destroy(collision.gameObject);
            Debug.Log("Gems Collected: " + gemsCollected);
            if (gemsCollected % 5 == 0)
            {
                // Trigger faster obstacle spawn when every 5 gems are collected
                SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
                if (spawnManager != null)
                {
                    spawnManager.TriggerFasterObstacleSpawn();
                }
                else
                {
                    Debug.LogError("SpawnManager not found!");
                }
            }
        }
    }

    private void ActivateShield()
    {
        // Activate the shield
        isShieldActive = true;
        Debug.Log("Shield is active ");

        // Deactivate the shield after the specified duration
        Invoke("DeactivateShield", shieldDuration);
    }

    private void DeactivateShield()
    {
        // Deactivate the shield
        isShieldActive = false;
        Debug.Log("Shield is inactive");
    }

    private void UpdateGemCount()
    {
        gemText.text = "Gems: " + gemsCollected;
    }
    

  

    


    


}
