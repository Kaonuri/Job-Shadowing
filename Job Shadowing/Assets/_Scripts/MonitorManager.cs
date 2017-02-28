﻿using RenderHeads.Media.AVProVideo;
using UnityEngine;

public class MonitorManager : MonoBehaviour
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
                GameManager.Instance.GazeTriggerManager.gameObject.SetActive(false);
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                break;
            case MediaPlayerEvent.EventType.MetaDataReady:
                break;
            case MediaPlayerEvent.EventType.FinishedPlaying:
                {
                    GameManager.Instance.GazeTriggerManager.gameObject.SetActive(true);
                    _mediaPlayer.CloseVideo();
                    if (GameManager.Instance.GazeTriggerManager.IsAllTrigerInteracted())
                    {
                        GameManager.Instance.readyToEnding = true;
                    }
                }                
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
