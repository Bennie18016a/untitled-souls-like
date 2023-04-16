using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciver : MonoBehaviour
{
    [SerializeField] CharacterController cc;
    [SerializeField] UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] float drag = 0.3f;

    private float verticalVelocity;
    private Vector3 _impact;
    private Vector3 _dampingVelocity;
    public bool useGravity = true;

    public Vector3 movement => _impact + Vector3.up * verticalVelocity;


    private void Update()
    {
        if (verticalVelocity < 0f && cc.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else if (useGravity && !cc.isGrounded)
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, drag);

        if (_impact.sqrMagnitude < 0.2f * 0.2f && agent != null)
        {
            _impact = Vector3.zero;
            agent.enabled = true;
        }
    }

    public void AddForce(Vector3 force)
    {
        _impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    public void Throw(Vector3 direction, float force)
    {
        _impact += direction * 150;
        verticalVelocity += force;

    }
}
