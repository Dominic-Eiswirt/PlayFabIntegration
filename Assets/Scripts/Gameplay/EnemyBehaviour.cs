
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//Desired behaviour - get player pos once, move to that position while shooting at the actual position of the player
public class EnemyBehaviour : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private int health = 3;
    private float movementSpeed = 10;
    private const float originalFireRate = 1.5f;
    private float fireRate = originalFireRate;
    public delegate void EnemyEvent();
    public EnemyEvent OnEnemyDeath;
    public EnemyEvent OnEnemyAttack;
    public bool move = true;
    public EnemyType myType;
 
    private WaitForSeconds waitDuration = new WaitForSeconds(2f);
    private bool alive = true;

    void Start()
    {
        fireRate = originalFireRate + Random.Range(-0.25f, 1.75f);
        target = (PlayerInput.instance.gameObject.transform.position - this.transform.position).normalized;
    }

    private void Update()
    {
        if (alive)
        {
            fireRate -= Time.deltaTime;
            if (fireRate < 0)
            {
                move = false;
                GameplayManager.instance.SpawnEnemyBullet(this.transform.position);
                fireRate = originalFireRate + Random.Range(-0.5f, 1.75f);
                if (myType == EnemyType.Gunner)
                {
                    move = false;
                    GameplayManager.instance.SpawnEnemyBullet(this.transform.position, new Vector3(Random.Range(-5, 5),
                                                                                                    Random.Range(-5, 5),
                                                                                                                    0));
                    GameplayManager.instance.SpawnEnemyBullet(this.transform.position, new Vector3(Random.Range(-5, 5), 
                                                                                                    Random.Range(-5, 5), 
                                                                                                                    0));
                }
                OnEnemyAttack.Invoke();
            }
            if (move)
            {
                this.transform.position += target * movementSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {        
        //Player bullet is a trigger
        health--;        
        other.gameObject.SetActive(false);
        if(health <= 0)
        {
            alive = false;
            GetComponent<BoxCollider>().enabled = false;
            OnEnemyDeath.Invoke();            
            GameplayManager.instance.coreGameData.score++;
            StartCoroutine(WaitForDestruction());
        }
    }

    public void SetEnemy(EnemyType enemyType)
    {
        myType = enemyType;
        if(enemyType == EnemyType.Demon)
        {
            health += 3;
        }
        if (enemyType == EnemyType.Gunner)
        {
            health += 2;
        }
    }
   
    private IEnumerator WaitForDestruction()
    {
        yield return waitDuration;
        Destroy(this.gameObject);
    }
}
