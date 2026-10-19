using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource chaseSource;
    public AudioSource sfxSource;

    [Header("Músicas de cada cena")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip backroomsMusic;
    public AudioClip victoryMusic;
    public AudioClip gameOverMusic;

    private bool podeTocarSonsDoJogo;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += AoCarregarCena;
    }

    private void Start()
    {
        AoCarregarCena(
            SceneManager.GetActiveScene(),
            LoadSceneMode.Single
        );
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= AoCarregarCena;
            Instance = null;
        }
    }

    private void AoCarregarCena(
        Scene cena,
        LoadSceneMode modo)
    {
        PararTodosOsSons();

        podeTocarSonsDoJogo = false;

        AudioClip musica = null;

        switch (cena.name)
        {
            case "MenuInicio":
                musica = menuMusic;
                break;

            case "JogoPrincipal":
                musica = gameMusic;
                podeTocarSonsDoJogo = true;
                break;

            case "Backrooms":
                musica = backroomsMusic;
                podeTocarSonsDoJogo = true;
                break;

            case "Vitoria":
                musica = victoryMusic;
                break;

            case "GameOverScene":
                musica = gameOverMusic;
                break;
        }

        if (musicSource != null && musica != null)
        {
            musicSource.clip = musica;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    private void PararTodosOsSons()
    {
        if (musicSource != null)
            musicSource.Stop();

        if (chaseSource != null)
            chaseSource.Stop();

        if (sfxSource != null)
            sfxSource.Stop();
    }

    public void StartChase(AudioClip som)
    {
        if (!podeTocarSonsDoJogo ||
            chaseSource == null || som == null)
            return;

        if (chaseSource.isPlaying && chaseSource.clip == som)
            return;

        chaseSource.Stop();
        chaseSource.clip = som;
        chaseSource.loop = true;
        chaseSource.Play();
    }

    public void StopChase()
    {
        if (chaseSource != null)
            chaseSource.Stop();
    }

    public float PlayCapture()
    {
        if (!podeTocarSonsDoJogo ||
            captureSound == null || sfxSource == null)
            return 0f;

        sfxSource.PlayOneShot(captureSound);
        return captureSound.length;
    }

    public AudioClip captureSound;

    public void PlaySFX(AudioClip som)
    {
        if (podeTocarSonsDoJogo &&
            som != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(som);
        }
    }
}