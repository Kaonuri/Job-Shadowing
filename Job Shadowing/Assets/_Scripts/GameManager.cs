using UnityEngine;

public enum Flow
{
    Connect,
    Intro,
    Interact,
    Ending,
    None
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }

    public VRManager VRManager;
    public AirVRCameraRig AirVRCameraRig;
    public GazeTriggerManager GazeTriggerManager;
    public MonitorManager MonitorManager;
    public BackgroundManager BackgroundManager;
    public AudioSource BGM;

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
        currentFlow = Flow.Connect;
        GazeTriggerManager.gameObject.SetActive(false);
    }

    private void Update()
    {
        switch (currentFlow)
        {
            case Flow.Connect:
                {
                    if (AirVRCameraRig.isBoundToClient)
                    {
                        BackgroundManager._mediaPlayer.Control.Play();
                        currentFlow = Flow.Intro;
                    }
                }
                break;

            case Flow.Intro:
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        BackgroundManager._mediaPlayer.Control.Seek(90000f);
                    }

                    if (BackgroundManager._mediaPlayer.Control.GetCurrentTimeMs() >= 90000f)
                    {
                        BackgroundManager._mediaPlayer.Control.Pause();
                        GazeTriggerManager.gameObject.SetActive(true);
                        BGM.Play();
                        currentFlow = Flow.Interact;
                    }
                }
                break;
            case Flow.Interact:
                {
                    if (GazeTriggerManager.IsAllTrigerInteracted() && readyToEnding)
                    {
                        BGM.Stop();
                        GazeTriggerManager.gameObject.SetActive(false);
                        BackgroundManager._mediaPlayer.Control.Play();
                        currentFlow = Flow.Ending;
                    }
                }
                break;
            case Flow.Ending:
                {
                    if (BackgroundManager._mediaPlayer.Control.IsFinished())
                    {
                        currentFlow = Flow.None;
                    }
                }
                break;
        }
    }
}
