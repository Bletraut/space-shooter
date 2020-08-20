using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health")]
    public Breakable Breakable;

    [Header("Bar settings")]
    [SerializeField]
    private RectTransform Fill;
    [SerializeField]
    private Text Text;

    // Update is called once per frame
    void Update()
    {
        var h = Breakable.Health / Breakable.maxHealth;

        var scale = Fill.localScale;
        scale.x = h;
        Fill.localScale = scale;

        Text.text = $"{h * 100}%";
    }
}
