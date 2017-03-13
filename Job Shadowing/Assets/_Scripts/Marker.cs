using DG.Tweening;
using UnityEngine;

public class Marker : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.DOFade(0f, 1f).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(0f, 1f));
        transform.LookAt(Camera.main.transform);
    }
}
