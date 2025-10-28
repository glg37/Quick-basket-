using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInStart : MonoBehaviour
{
    [Header("Fade de Início")]
    public Image fadeImage; // imagem preta na tela
    public float fadeDuration = 1f; // tempo da animação

    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}
