using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTreadAttackState : BossBaseState
{
    private float _previousFrameTime;
    bool hasPlacedParticles;

    public WitchTreadAttackState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime("Tread Carefully", 0.1f);
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

        if (normalizedTime >= _previousFrameTime && normalizedTime > .5f && !hasPlacedParticles)
        {
            ShootRaycasts();
            hasPlacedParticles = true;
        }
        _previousFrameTime = normalizedTime;

    }

    void ShootRaycasts()
    {
        float radius = 5f; // set the radius of the sphere
        int numRaycasts = 10; // set the number of raycasts to shoot
        float raycastLength = 100f; // set the length of the raycasts

        for (int i = 0; i < numRaycasts; i++)
        {
            Vector3 randomDirection = UnityEngine.Random.onUnitSphere; // get a random direction on the unit sphere
            Vector3 origin = _stateMachine.transform.position + randomDirection * radius; // set the origin of the raycast to the surface of the sphere
            RaycastHit hit;
            if (Physics.Raycast(origin, Vector3.down, out hit, raycastLength) && hit.collider.CompareTag("Floor")) // shoot the raycast downwards
            {
                Debug.DrawLine(origin, hit.point, Color.green, 10); // draw a green line to show where the raycast hit
                Quaternion objectRoation = Quaternion.Euler(0, 0, 0);
                GameObject.Instantiate(Resources.Load("Tread beam Particle"), hit.point, objectRoation);
            }
        }
    }

    public override void Exit() { _stateMachine.ForceReciver.useGravity = true; }
}