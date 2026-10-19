using UnityEngine;

public class NextbotCapture : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bot pegou o jogador");
            gameManager.GameOver();
        }
    }
}
