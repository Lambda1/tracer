using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArea : MonoBehaviour
{
    private bool is_goal = false;

    public bool IsGoal { get { return is_goal; } }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            is_goal = true;
        }
    }
}