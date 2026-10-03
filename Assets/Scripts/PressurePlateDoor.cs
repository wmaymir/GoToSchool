using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PressurePlateDoor : MonoBehaviour
{
    [SerializeField] private List<GameObject> pressurePlates;
    [SerializeField] private DoorOpen doorOpenScript;

    void Start()
    {
        if (pressurePlates.Count == 0)
        {
            Debug.LogError("No pressure plates assigned to the PressurePlateDoor script.");
        }
        if (doorOpenScript == null)
        {
            doorOpenScript = GetComponent<DoorOpen>();
        }
    }

    void Update()
    {
        if (AreAllPressurePlatesActivated())
        {
            doorOpenScript.OpenDoor();
        }
        else if (!AreAllPressurePlatesActivated())
        {
            doorOpenScript.CloseDoor();
        }
    }

    bool AreAllPressurePlatesActivated()
    {
        foreach (var plate in pressurePlates)
        {
            if (!plate.GetComponent<PressurePlateMovement>().IsPressed)
            {
                return false;
            }
        }
        return true;
    }
}
