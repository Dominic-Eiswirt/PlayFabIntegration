using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum EnemyType { Sarge = 0, Gunner = 1, Demon = 2 }
public class RandomizeEnemyAndAnimator : MonoBehaviour
{
    private EnemyType myType;
    private EnemyBehaviour myBehaviour;
    private Animator anim;
    void Start()
    {
        myBehaviour = GetComponentInParent<EnemyBehaviour>();
        anim = GetComponent<Animator>();
        myBehaviour.OnEnemyDeath += PlayDeathAnim;
        myBehaviour.OnEnemyAttack += PlayAttackAnim;
        myType = (EnemyType)Random.Range(0, 3);
        myBehaviour.SetEnemy(myType);
        anim.SetInteger("EnemyType", (int)myType);        
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
