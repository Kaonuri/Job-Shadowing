using System.Collections.Generic;
using UnityEngine;

public class GazeTriggerManager : MonoBehaviour
{
    private List<GazeTrigger> gazeTriggers = new List<GazeTrigger>();
    private float currentTime = 0;

    private void Awake()
    {
        foreach (var gazeTrigger in GetComponentsInChildren<GazeTrigger>())
        {
            gazeTriggers.Add(gazeTrigger);
        }        
    }

    private void Update()
    {
        
    }

    public bool IsAllTrigerInteracted()
    {
        foreach (var gazeTrigger in gazeTriggers)
        {
            if (!gazeTrigger.Interacted)
                return false;
        }
        return true;
    }
}
