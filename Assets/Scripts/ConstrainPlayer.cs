using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainPlayer : MonoBehaviour
{

    public BoxCollider2D playerConstraint;

    void Update()
    {
        if (playerConstraint == null) return;
        // if the player is outside the bounds of the playerConstraint, move them back inside
        if (!playerConstraint.bounds.Contains(transform.position))
        {
            transform.position = playerConstraint.bounds.ClosestPoint(transform.position);
        }
    }


}
