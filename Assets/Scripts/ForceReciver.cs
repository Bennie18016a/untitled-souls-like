using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciver : MonoBehaviour
{
    public Vector3 movement => Vector3.up * verticalVelocity;
    [SerializeField] CharacterController cc;
    private float verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && cc.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
