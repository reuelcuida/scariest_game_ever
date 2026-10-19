using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [Header("Componentes de UI")]
    public CanvasGroup fadeCanvasGroup;
    public float duracaoFade = 1f;

    private void Awake()
    {
        // Garante que só exista um gerenciador e que ele não seja destruído entre as cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Começa a cena limpando a tela (Fade In)
        StartCoroutine(Fade(1f, 0f));
    }

    public void MudarDeCena(string nomeCena)
    {
        StartCoroutine(CarregarCenaComFade(nomeCena));
    }

    private IEnumerator CarregarCenaComFade(string nomeCena)
    {
        // Escurece a tela (Fade Out)
        yield return StartCoroutine(Fade(0f, 1f));

        // Carrega a nova cena
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nomeCena);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Clareia a tela na nova cena (Fade In)
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private IEnumerator Fade(float valorInicial, float valorFinal)
    {
        float tempoPosicionado = 0f;
        while (tempoPosicionado < duracaoFade)
        {
            tempoPosicionado += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(valorInicial, valorFinal, tempoPosicionado / duracaoFade);
            yield return null;
        }
        fadeCanvasGroup.alpha = valorFinal;
    }
}
