using UnityEngine;
using UnityEngine.SceneManagement; // Biblioteca necessária para gerenciar cenas

public class TrocaCena : MonoBehaviour
{
    // Função que pode ser chamada por um botão ou por código
    public void MudarParaCena()
    {

        Time.timeScale = 1f;

        GameManager.PrepararNovaPartida();

        SceneManager.LoadScene("JogoPrincipal");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
