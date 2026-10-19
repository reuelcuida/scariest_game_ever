using UnityEngine;
using UnityEngine.AI;

public class NextBot : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    private NavMeshAgent agent;

    [Header("Detection")]
    public float detectionRange = 30f;

    [Header("Movement")]
    public float chaseSpeed = 8f;
    public float updateRate = 0.2f;

    private bool isChasing = false;
    private float nextUpdate;
    [Header("Audio")]
    public AudioClip chaseSound;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;

        // A rotação será controlada manualmente
        agent.updateRotation = false;
    }

    void Update()
    {
        if (player == null || agent == null)
            return;

        // ==========================================
        // OLHAR PARA O JOGADOR
        // ==========================================

        Vector3 direction = player.position - transform.position;

        // Impede o Nextbot de inclinar para cima/baixo
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0f, 90f, 0f);
        }

        // ==========================================
        // DISTÂNCIA DO JOGADOR
        // ==========================================

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // ==========================================
        // DETECÇÃO
        // ==========================================

        if (distance <= detectionRange && !isChasing)
        {
            isChasing = true;

            // Inicia o som de perseguição
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StartChase(chaseSound);
            }
        }

        // ==========================================
        // PERSEGUIÇÃO
        // ==========================================

        if (isChasing && Time.time >= nextUpdate)
        {
            agent.SetDestination(player.position);

            nextUpdate = Time.time + updateRate;
        }
    }
}