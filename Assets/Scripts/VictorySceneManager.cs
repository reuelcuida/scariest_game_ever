using System.Collections;
using UnityEngine;
using TMPro; // Importa a biblioteca do TextMeshPro
using UnityEngine.UI; // Importa para gerenciar componentes de botão
using UnityEngine.SceneManagement; // Importa para carregar o menu inicial

public class VictorySceneManager : MonoBehaviour
{
    [Header("Configurações de Texto (TextMeshPro)")]
    public TMP_Text textoDialogo;
    [TextArea(3, 5)]
    public string[] linhasDialogo;
    public float velocidadeEscrita = 0.04f;
    public float tempoDeLeituraPorFrase = 2.5f; // Quanto tempo o texto fica na tela após terminar de digitar

    [Header("Configurações da Imagem Final")]
    public CanvasGroup imagemVitoriaCanvasGroup;
    public float velocidadeAparecimentoImagem = 1.5f;

    [Header("Configurações de Áudio")]
    [Tooltip("Arraste o componente AudioSource aqui")]
    public AudioSource fonteAudio;
    [Tooltip("Som curto de conversa ('blip' ou 'mumble') que toca a cada caractere")]
    public AudioClip somConversa;
    [Tooltip("Som de impacto ou transição que toca quando a imagem de vitória surge")]
    public AudioClip somImagemVitoria;

    [Header("Configurações do Menu")]
    [Tooltip("Arraste o botão UI do menu inicial aqui")]
    public Button botaoMenuInicial;
    [Tooltip("Nome exato da sua cena de Menu Inicial no Build Settings")]
    public string nomeCenaMenu = "MenuInicial";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Garante que a imagem comece totalmente invisível
        if (imagemVitoriaCanvasGroup != null)
            imagemVitoriaCanvasGroup.alpha = 0f;

        textoDialogo.text = "";

        // Oculta o botão no início
        if (botaoMenuInicial != null)
        {
            botaoMenuInicial.gameObject.SetActive(false);
            // Configura a função do botão via código para evitar esquecer no Inspector
            botaoMenuInicial.onClick.AddListener(IrParaMenuInicial);
        }

        // Inicia a sequência automática de vitória
        StartCoroutine(SequenciaAutomaticaVitoria());
    }

    private IEnumerator SequenciaAutomaticaVitoria()
    {
        // 1. Roda todos os diálogos automaticamente
        foreach (string frase in linhasDialogo)
        {
            textoDialogo.text = "";

            // Efeito máquina de escrever caractere por caractere
            foreach (char letra in frase.ToCharArray())
            {
                textoDialogo.text += letra;

                // Toca o som "pique Bad Parents" apenas se não for um espaço em branco
                if (somConversa != null && fonteAudio != null && !char.IsWhiteSpace(letra))
                {
                    // PlayOneShot permite sobrepor pequenos sons perfeitamente sem cortar o anterior
                    fonteAudio.PlayOneShot(somConversa);
                }

                yield return new WaitForSeconds(velocidadeEscrita);
            }

            // Espera o tempo configurado para o jogador conseguir ler a frase inteira
            yield return new WaitForSeconds(tempoDeLeituraPorFrase);
        }

        // 2. Apaga o texto suavemente antes da imagem aparecer (Fade Out do Texto)
        float tempoTexto = 0;
        while (tempoTexto < 0.5f)
        {
            tempoTexto += Time.deltaTime;
            textoDialogo.alpha = Mathf.Lerp(1f, 0f, tempoTexto / 0.5f);
            yield return null;
        }
        textoDialogo.text = "";

        // Toca o som de impacto/revelação da imagem de vitória no exato início do Fade In
        if (somImagemVitoria != null && fonteAudio != null)
        {
            fonteAudio.PlayOneShot(somImagemVitoria);
        }

        // 3. Faz a imagem de vitória aparecer (Fade In da Imagem)
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / velocidadeAparecimentoImagem;
            imagemVitoriaCanvasGroup.alpha = alpha;
            yield return null;
        }
        imagemVitoriaCanvasGroup.alpha = 1f;

        // 4. Aguarda 2 segundos após a imagem preencher a tela inteira e exibe o botão
        yield return new WaitForSeconds(2.0f);

        if (botaoMenuInicial != null)
        {
            botaoMenuInicial.gameObject.SetActive(true);
        }
    }

    // Função acionada ao clicar no botão
    public void IrParaMenuInicial()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }
}
