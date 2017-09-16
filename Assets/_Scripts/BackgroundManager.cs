using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    public MediaPlayer _mediaPlayer;

    private void Awake()
    {
        _mediaPlayer.Events.AddListener(OnMediaPlayerEvent);
    }

    public void OnMediaPlayerEvent(MediaPlayer mediaPlayer, MediaPlayerEvent.EventType eventType, ErrorCode errorCode)
    {
        switch (eventType)
        {
            case MediaPlayerEvent.EventType.ReadyToPlay:
                break;
            case MediaPlayerEvent.EventType.Started:
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                _mediaPlayer.CloseVideo();
                break;
        }
    }

    public void LoadVideo(string filePath)
    {
        if (!_mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder, filePath, _mediaPlayer.m_AutoStart))
        {
            Debug.LogError("Failed to open video!");
        }
    }
}
