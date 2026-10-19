using UnityEngine;

public class Fita : MonoBehaviour
{
    [Header("Configurações de Rotação")]
    public Vector3 velocidadeRotacao = new Vector3(0f, 50f, 0f);

    [Header("Configurações de Flutuação")]
    public float amplitude = 0.5f;
    public float frequencia = 1f;

    private Vector3 posicaoInicial;
    public string sceneName = "Vitoria";

    private bool transitioning = false;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        transform.Rotate(velocidadeRotacao * Time.deltaTime);

        float novaY = posicaoInicial.y + Mathf.Sin(Time.time * frequencia) * amplitude;
        transform.position = new Vector3(posicaoInicial.x, novaY, posicaoInicial.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transitioning || !other.CompareTag("Player"))
            return;

        transitioning = true;

        // Chama o gerenciador de transição com fade suave
        if (SceneTransitionManager.Instance != null)
        {
            SceneTransitionManager.Instance.MudarDeCena(sceneName);
        }
        else
        {
            // Fallback caso você esqueça de colocar o Manager na cena de teste
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
