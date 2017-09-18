using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class MonitorManager : MonoBehaviour
{
    public MediaPlayer mediaPlayer;
    public CanvasGroup canvasGroup;

    private void Awake()
    {
        mediaPlayer.Events.AddListener(OnMediaPlayerEvent);
        canvasGroup.alpha = 0f;
    }

    private void Update()
    {
        if (mediaPlayer.Control.IsPlaying())
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || AirVRInput.GetDown(GameManager.Instance.AirVRCameraRig, AirVRInput.Touchpad.Button.Back))
            {
            mediaPlayer.Control.Seek(mediaPlayer.Info.GetDurationMs() - 1f);

            }
        }
    }

    public void OnMediaPlayerEvent(MediaPlayer mediaPlayer, MediaPlayerEvent.EventType eventType, ErrorCode errorCode)
    {
        switch (eventType)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                break;
            case MediaPlayerEvent.EventType.Started:
                canvasGroup.alpha = 1f;
                GameManager.Instance.BGM.Pause();
                GameManager.Instance.GazeTriggerManager.gameObject.SetActive(false);
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                {
                    GameManager.Instance.GazeTriggerManager.gameObject.SetActive(true);
                    GameManager.Instance.BGM.UnPause();
                    mediaPlayer.CloseVideo();
                    if (GameManager.Instance.GazeTriggerManager.IsAllTrigerInteracted())
                    {
                        GameManager.Instance.readyToEnding = true;
                    }
                    canvasGroup.alpha = 0f;
                }                
                break;
        }
    }

    public void LoadVideo(string filePath)
    {
        if (!mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, filePath, mediaPlayer.m_AutoStart))
        {
            Debug.LogError("Failed to open video!");
        }
    }
}
