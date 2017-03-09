using System.Collections.Generic;
using UnityEngine;

public class GazeTriggerManager : MonoBehaviour
{
    [SerializeField] private List<GazeTrigger> gazeTriggers = new List<GazeTrigger>();
    private float currentTime = 0;

    private void Awake()
    {
        foreach (var gazeTrigger in GetComponentsInChildren<GazeTrigger>())
        {
            gazeTriggers.Add(gazeTrigger);
        }        
    }

    public bool IsAllTrigerInteracted()
    {
        foreach (var gazeTrigger in gazeTriggers)
        {
            if (!gazeTrigger.interacted)
                return false;
        }
        return true;
    }
}
