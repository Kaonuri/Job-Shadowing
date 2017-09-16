using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SelectionRadial : MonoBehaviour
{
    public event Action OnSelectionComplete;                                               

    public float selectionDuration = 2f;
    public bool hideOnStart = true;
    public Image selectionImage;

    Coroutine selectionFillRoutine;                                               
    bool isSelectionRadialActive;                                                 
    bool radialFilled;                                                            


    public float SelectionDuration { get { return selectionDuration; } }

    void Start()
    {
        selectionImage.fillAmount = 0f;

        if (hideOnStart)
            Hide();
    }


    public void Show()
    {
        selectionImage.gameObject.SetActive(true);
        isSelectionRadialActive = true;
    }


    public void Hide()
    {
        selectionImage.gameObject.SetActive(false);
        isSelectionRadialActive = false;

        selectionImage.fillAmount = 0f;
    }


    IEnumerator FillSelectionRadial()
    {
        radialFilled = false;

        float timer = 0f;
        selectionImage.fillAmount = 0f;

        while (timer < selectionDuration)
        {
            selectionImage.fillAmount = timer / selectionDuration;

            timer += Time.deltaTime;
            yield return null;
        }

        selectionImage.fillAmount = 1f;

        isSelectionRadialActive = false;

        radialFilled = true;

        if (OnSelectionComplete != null)
            OnSelectionComplete();
    }


    public IEnumerator WaitForSelectionRadialToFill()
    {
        radialFilled = false;

        Show();

        while (!radialFilled)
        {
            yield return null;
        }

        Hide();
    }


    public void HandleGazeStart()
    {
        if (isSelectionRadialActive)
        {
            selectionFillRoutine = StartCoroutine(FillSelectionRadial());
        }
    }


    public void HandleGazeCancel()
    {
        if (isSelectionRadialActive)
        {
            if (selectionFillRoutine != null)
                StopCoroutine(selectionFillRoutine);

            selectionImage.fillAmount = 0f;
        }
    }
}

