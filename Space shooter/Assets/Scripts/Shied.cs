using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shied : MonoBehaviour
{
    public bool IsActivate { get; private set; } = false;

    public float Time = 2f;
    public float FlashDelay = 0.2f;

    public UnityEvent OnActivate;
    public UnityEvent OnDeactivate;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        if (IsActivate) return;

        IsActivate = true;
        OnActivate?.Invoke();

        StartCoroutine(StartShield());
        StartCoroutine(StartFlash());
    }

    public void Deactivate()
    {
        if (!IsActivate) return;

        IsActivate = false;
        OnDeactivate?.Invoke();

        var c = spriteRenderer.color;
        c.a = 1;
        spriteRenderer.color = c;

        StopAllCoroutines();
    }

    private IEnumerator StartFlash()
    {
        while(true)
        {
            var c = spriteRenderer.color;
            c.a = c.a == 0.5f ? 1 : 0.5f;
            spriteRenderer.color = c;

            yield return new WaitForSeconds(FlashDelay);
        }
    }
    private IEnumerator StartShield()
    {
        yield return new WaitForSeconds(Time);

        Deactivate();
    }
}
