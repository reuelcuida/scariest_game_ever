using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Configurações de UI")]
    [SerializeField] private GameObject painelPause; // Arraste seu painel de UI aqui

    // Variável estática (global) que pode ser lida por QUALQUER outro script
    public static bool jogoPausado = false;

    public string sceneName = "MenuInicio";

    void Start()
    {
        // Garante que o painel comece desativado quando o jogo iniciar
        if (painelPause != null)
        {
            painelPause.SetActive(false);
        }

        // Garante que as variáveis de estado comecem limpas no início do jogo
        Time.timeScale = 1f;
        jogoPausado = false;

        TravarMouse();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        jogoPausado = true; // Avisa globalmente que o jogo pausou

        if (painelPause != null)
        {
            painelPause.SetActive(true); // Ativa o painel de pause
        }

        LiberarMouse();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        jogoPausado = false; // Avisa globalmente que o jogo despausou

        if (painelPause != null)
        {
            painelPause.SetActive(false); // Esconde o painel de pause
        }

        TravarMouse();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        jogoPausado = false; // Reseta o estado estático antes de mudar de cena
        SceneManager.LoadScene(sceneName);
    }

    // Métodos auxiliares para o controle do Cursor do mouse
    private void LiberarMouse()
    {
        Cursor.lockState = CursorLockMode.None; // Destrava o mouse do centro da tela
        Cursor.visible = true;                  // Torna o ponteiro visível
    }

    private void TravarMouse()
    {
        Cursor.lockState = CursorLockMode.Locked; // Trava o mouse no centro da tela
        Cursor.visible = false;                   // Esconde o ponteiro
    }
}
