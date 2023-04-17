using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchKnifeAttackState : BossBaseState
{
    private float _previousFrameTime;
    bool hasnotshot;
    GameObject kunais;
    float time;
    int num;

    public WitchKnifeAttackState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime("Targeting Knives", 0.1f);
        kunais = new GameObject();

        for (int i = 0; i != 3; i++)
        {
            Vector3 pos = GameObject.Find("KunaiSpawn").transform.position;
            GameObject kunai = (GameObject)GameObject.Instantiate(Resources.Load("Kunai"), pos, Quaternion.identity);
            kunai.transform.parent = kunais.transform;
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        float normalizedTime = GetNormalizedTime(_stateMachine.Animator);

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            Debug.Log("Attacking");
        }
        else
        {
            _stateMachine.SwitchState(new BossChaseState(_stateMachine));
        }

        if (normalizedTime >= _previousFrameTime && normalizedTime > .5f && !hasnotshot)
        {
            if (time > 0 && num == 0)
            {
                kunais.transform.GetComponentInChildren<KunaiBehaviour>().Shoot();
                num++;
            }
            if (time > .5f && num == 1)
            {
                kunais.transform.GetComponentInChildren<KunaiBehaviour>().Shoot();
                num++;
            }
            if (time > .7f && num == 2)
            {
                kunais.transform.GetComponentInChildren<KunaiBehaviour>().Shoot();
                num++;
            }
            Debug.Log(num);

            time += 1 * deltaTime;
        }

        _previousFrameTime = normalizedTime;

    }
    public override void Exit()
    {
        GameObject.Destroy(kunais);
    }
}