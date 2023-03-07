using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Targeter : MonoBehaviour
{
    private List<Target> targets = new List<Target>();
    [SerializeField] private CinemachineTargetGroup targetGroup;

    public Target currentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.TryGetComponent<Target>(out Target target))
        {
            if (targets.Contains(target)) return;

            targets.Add(target);
            target.onDestroyed += RemoveTarget;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.TryGetComponent<Target>(out Target target))
        {
            targets.Remove(target);
            RemoveTarget(target);
        }
    }

    public bool SelectTarget()
    {
        //Make sure we have targets
        if (targets.Count == 0) return false;

        //Creates closestTarget and distance to infinity so that it can be any target
        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        /*For each target in the list,
        Gets the position of the player and places it inside the viewport space
        Check to see if it is inside our screen, if it is continue
        We get the center bu minusing the viewpos bu 0.5 on each axis
        fincally we check to see if it is the closest targter.
        If its the closest, we make it closestTarget
        and set the distance to how far away it is from us.*/
        foreach (Target target in targets)
        {
            Vector2 viewPos = Camera.main.WorldToViewportPoint(target.transform.position);

            if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1) { continue; }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closestTargetDistance) { closestTarget = target; closestTargetDistance = toCenter.sqrMagnitude; }
        }

        //If there is no closest target, return
        if (closestTarget == null) { return false; }

        //Sets current target to closest and adds it to the group.
        currentTarget = closestTarget;
        targetGroup.AddMember(currentTarget.transform, 1f, 3f);
        return true;
    }

    public void Cancel()
    {
        // if it isnt null, remove target from target group and set current target to null.
        if (currentTarget != null) targetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        /*if current target is out target in question
        remove it from the target group and set it to null*/
        if (currentTarget == target)
        {
            targetGroup.RemoveMember(target.transform);
            currentTarget = null;
        }

        /*We always remove the function from the action "onDestroyed"
        remov the target from list*/
        target.onDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
