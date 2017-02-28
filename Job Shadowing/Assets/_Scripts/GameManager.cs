using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Flow
{
    Intro,
    Interact,
    Ending,
    None
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public VRManager VRManager;
    public GazeTriggerManager GazeTriggerManager;
    public MonitorManager MonitorManager;
    public BackgroundManager BackgroundManager;

    public Flow currentFlow { private set; get; }
    public bool readyToEnding;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        readyToEnding = false;
    }

    private void Start()
    {
        currentFlow = Flow.Intro;
        GazeTriggerManager.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (currentFlow)
        {
            case Flow.Intro:
                {
                    if (BackgroundManager._mediaPlayer.Control.GetCurrentTimeMs() >= 5000f)
                    {
                        BackgroundManager._mediaPlayer.Control.Pause();
                        GazeTriggerManager.gameObject.SetActive(true);
                        currentFlow = Flow.Interact;
                    }
                }
                break;
            case Flow.Interact:
                {
                    if (GazeTriggerManager.IsAllTrigerInteracted() && readyToEnding)
                    {
                        currentFlow = Flow.Ending;
                        GazeTriggerManager.gameObject.SetActive(false);
                        BackgroundManager._mediaPlayer.Control.Play();
                    }
                }
                break;
            case Flow.Ending:
                {
                }
                break;

        }
    }
}
