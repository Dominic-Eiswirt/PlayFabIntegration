
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
    public delegate void DamageWasTaken();
    public event DamageWasTaken OnDamageEvent;
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

    private GameObject enemyBulletContainer;
    private GameObject playerBulletContainer;

    private void Awake()
    {
        if (instance == null)
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
        enemyBulletContainer = new GameObject("Enemy Bullet Container");
        playerBulletContainer = new GameObject("Player Bullet Container");
        Instantiate(playerPrefab, this.transform);
        enemyBulletPooler = new ObjectPooler(enemyBulletContainer);
        enemyBulletPooler.CreatePool(enemyBullet, 25);
        playerBulletPooler = new ObjectPooler(playerBulletContainer);
        playerBulletPooler.CreatePool(playerBullet, 25);
        PlayerInput.instance.manager = this;
        AudioCenter.instance.SetGameActive();

        
        //string jsonString = JsonUtility.ToJson(new LastGameInfo { 
        //                                        lastUsedWeaponType = CurrentWeaponLoadout.instance.selectedWeapon.myType 
        //});
        //PlayfabEntity.instance.SetWeaponBeforeGameEnd(jsonString);
        
    }
    void Update()
    {
        gameTimer -= Time.deltaTime;
        gameTimerText.text = "Time:" + gameTimer.ToString("F1");
        scoreText.text = "Kills: " + coreGameData.score.ToString();

        waveTimer += Time.deltaTime;
        if (waveTimer >= 3)
        {
            for (int i = 0; i < enemysPerWave; i++)
            {
                Instantiate(enemyPrefab,
                            enemySpawnLocations[Random.Range(0, enemySpawnLocations.Length - 1)].transform.position,
                            enemyPrefab.transform.rotation,
                            gameObject.transform);
            }
            waveTimer = 0;
        }

        if (gameTimer <= 0 || playerHealth <= 0)
        {           
            
            enemyBulletPooler.NukePool();
            playerBulletPooler.NukePool();            
            UICenter.instance.SetNewData(coreGameData);
            AudioCenter.instance.SetGameInactive();
            string jsonString = JsonUtility.ToJson(new LastGameInfo
            {
                lastUsedWeaponType = CurrentWeaponLoadout.instance.selectedWeapon.myType,
                killCount = coreGameData.score
            });
            PlayfabEntity.instance.SetWeaponBeforeGameEnd(jsonString);
            CurrentWeaponLoadout.instance.selectedWeapon = null;
            UICenter.instance.ChangeState(new LobbyState());
        }
    }

    public void SpawnPlayerBullet(Vector3 position, Vector3 modifier = default(Vector3))
    {
        playerBulletPooler.SpawnItem(position, modifier);
    }

    public void SpawnEnemyBullet(Vector3 position, Vector3 modifier = default(Vector3))
    {
        enemyBulletPooler.SpawnItem(position, modifier);
    }

    public void RegisterHit()
    {
        playerHealth--;
        OnDamageEvent.Invoke();
    }
        
}
