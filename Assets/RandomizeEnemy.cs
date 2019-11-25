using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RandomizeEnemy : MonoBehaviour
{
    EnemyBehaviour myBehaviour;
    Animator anim;
    void Start()
    {
        myBehaviour = GetComponentInParent<EnemyBehaviour>();
        anim = GetComponent<Animator>();
        myBehaviour.OnEnemyDeath += PlayDeathAnim;
        myBehaviour.OnEnemyAttack += PlayAttackAnim;
        int rollDemon = Random.Range(0, 3);
        //30% chance to be a demon
        if(rollDemon < 2)
        {
            //Human
            anim.SetBool("Demon", false);
        }
        else
        {
            //Demon
            anim.SetBool("Demon", true);
            myBehaviour.SetIsDemon();
        }
    
    }

    private void OnDisable()
    {
        myBehaviour.OnEnemyDeath -= PlayDeathAnim;
        myBehaviour.OnEnemyAttack -= PlayAttackAnim;
    }

    public void PlayDeathAnim()
    {
        anim.SetTrigger("Death");
    }

    public void PlayAttackAnim()
    {
        anim.SetTrigger("Attack");        
    }
    public void AttackAnimFinished()
    {
        myBehaviour.move = true;
    }
}
