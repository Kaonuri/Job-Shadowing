using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class MediaController : MonoBehaviour
{

    public MediaPlayer _mediaPlayer;

    private void Start()
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
                break;
        }
    }
}
