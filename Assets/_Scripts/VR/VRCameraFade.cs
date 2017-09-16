using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRCameraFade : MonoBehaviour
{
    public event Action OnFadeComplete;                             


    [SerializeField] private Image fadeImage;                    
    [SerializeField] private Color fadeColor = Color.black;       
    [SerializeField] private float fadeDuration = 2.0f;           
    [SerializeField] private bool fadeInOnSceneLoad = false;      
    [SerializeField] private bool fadeInOnStart = false;          


    private bool isFading;                                        
    private float fadeStartTime;                                  
    private Color fadeOutColor;                                   


    public bool IsFading { get { return isFading; } }


    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += HandleSceneLoaded;

        fadeOutColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
        fadeImage.enabled = true;
    }


    private void Start()
    {
        if (fadeInOnStart)
        {
            fadeImage.color = fadeColor;
            FadeIn();
        }
    }


    private void HandleSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
    {
        if (fadeInOnSceneLoad)
        {
            fadeImage.color = fadeColor;
            FadeIn();
        }
    }


    public void FadeOut()
    {
        FadeOut(fadeDuration);
    }


    public void FadeOut(float duration)
    {
        if (isFading)
            return;
        StartCoroutine(BeginFade(fadeOutColor, fadeColor, duration));
    }


    public void FadeIn()
    {
        FadeIn(fadeDuration);
    }


    public void FadeIn(float duration)
    {
        if (isFading)
            return;
        StartCoroutine(BeginFade(fadeColor, fadeOutColor, duration));

    }


    public IEnumerator BeginFadeOut()
    {
        yield return StartCoroutine(BeginFade(fadeOutColor, fadeColor, fadeDuration));
    }


    public IEnumerator BeginFadeOut(float duration)
    {
        yield return StartCoroutine(BeginFade(fadeOutColor, fadeColor, duration));
    }


    public IEnumerator BeginFadeIn()
    {
        yield return StartCoroutine(BeginFade(fadeColor, fadeOutColor, fadeDuration));
    }


    public IEnumerator BeginFadeIn(float duration)
    {
        yield return StartCoroutine(BeginFade(fadeColor, fadeOutColor, duration));
    }


    private IEnumerator BeginFade(Color startCol, Color endCol, float duration)
    {
        isFading = true;

        float timer = 0f;
        while (timer <= duration)
        {
            fadeImage.color = Color.Lerp(startCol, endCol, timer / duration);

            timer += Time.deltaTime;
            yield return null;
        }

        isFading = false;

        if (OnFadeComplete != null)
            OnFadeComplete();
    }

    void OnDestroy()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= HandleSceneLoaded;
    }
}

