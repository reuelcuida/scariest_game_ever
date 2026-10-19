using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    public string sceneName = "Backrooms";

    private bool transitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (transitioning || !other.CompareTag("Player"))
            return;

        transitioning = true;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopChase();
        }

        SceneManager.LoadScene(sceneName);
    }
}