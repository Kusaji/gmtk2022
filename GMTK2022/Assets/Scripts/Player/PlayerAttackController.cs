using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerComponents components;
    
    [Space(10)]


    [Header("Basic Attack")]
    public float basicAttackDamage;
    public float basicAttackCooldown;

    [Space(10)]

    [Header("Bools")]
    public bool basicAttackReady;

    //Coroutines
    private IEnumerator namedBasicAttackRoutine;

    // Start is called before the first frame update
    void Start()
    {
        basicAttackReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BasicAttack();
        }
    }

    public void BasicAttack()
    {
        if (basicAttackReady)
        {
            namedBasicAttackRoutine = BasicAttackRoutine();
            StartCoroutine(namedBasicAttackRoutine);
        }
    }

    public IEnumerator BasicAttackRoutine()
    {
        basicAttackReady = false;
        components.animationController.playerAnimator.SetTrigger("Attack1");
        components.audioController.PlayAttackSound();

        yield return new WaitForSeconds(basicAttackCooldown);
        basicAttackReady = true;
    }
}
