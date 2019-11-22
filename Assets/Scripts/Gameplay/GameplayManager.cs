
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class CoreGameData
{
    public int score;
}
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    
    public int playerHealth = 5;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject enemyBullet;
    public GameObject playerBullet;
    public GameObject[] enemySpawnLocations;
    public CoreGameData coreGameData = new CoreGameData 
    { 
        score = 0
    };
    public int enemysPerWave = 3;
    public Text gameTimerText;
    public Text scoreText;
    
    private ObjectPooler playerBulletPooler;
    private ObjectPooler enemyBulletPooler;    
    private float waveTimer = 2;
    private float gameTimer = 30;

    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        Instantiate(playerPrefab);
        enemyBulletPooler = new ObjectPooler();
        enemyBulletPooler.CreatePool(enemyBullet, 25);
        playerBulletPooler = new ObjectPooler();
        playerBulletPooler.CreatePool(playerBullet, 10);
        PlayerInput.instance.manager = this;

    }
    void Update()
    {
        gameTimer -= Time.deltaTime;
        gameTimerText.text = "Time:" + gameTimer.ToString("F1");
        scoreText.text = "Kills: " + coreGameData.score.ToString();
        
        waveTimer += Time.deltaTime;
        if(waveTimer >= 3)
        {
            for(int i = 0; i < enemysPerWave; i++)
            { 
                Instantiate(enemyPrefab, 
                            enemySpawnLocations[Random.Range(0, enemySpawnLocations.Length-1)].transform.position, 
                            enemyPrefab.transform.rotation, 
                            gameObject.transform);
            }
            waveTimer = 0;
        }

        if(gameTimer <= 0 || playerHealth <= 0)
        {
            enemyBulletPooler.NukePool();
            playerBulletPooler.NukePool();
            Destroy(PlayerInput.instance.gameObject);
            UICenter.instance.SetNewData(coreGameData);
            UICenter.instance.ChangeState(new LobbyState());
        }
    }
    public void SpawnPlayerBullet(Vector3 position)
    {
        playerBulletPooler.SpawnItem(position);
    }
    
    public void SpawnEnemyBullet(Vector3 position)
    {
        enemyBulletPooler.SpawnItem(position);
    }

    public void RegisterHit()
    {
        playerHealth--;
    }    
}
