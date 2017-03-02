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

    public void OnMediaPlayerEvent(MediaPlayer mediaPlayer, MediaPlayerEvent.EventType eventType, ErrorCode errorCode)
    {
        switch (eventType)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                break;
            case MediaPlayerEvent.EventType.Started:
                canvasGroup.alpha = 1f;
                GameManager.Instance.GazeTriggerManager.gameObject.SetActive(false);
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                {
                    GameManager.Instance.GazeTriggerManager.gameObject.SetActive(true);
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
