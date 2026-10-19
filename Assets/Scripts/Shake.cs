using UnityEngine;
using System.Collections.Generic;

public class Shake : MonoBehaviour
{
    [Header("Referências")]
    public List<Transform> nextbots = new List<Transform>();

    [Header("Configuração do Shake")]
    public float shakeDistance = 6f;
    public float maxShake = 0.18f;
    public float shakeSpeed = 25f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        float closestDistance = Mathf.Infinity;

        foreach (Transform bot in nextbots)
        {
            if (bot == null)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                bot.position
            );

            if (distance < closestDistance)
                closestDistance = distance;
        }

        float intensity = 0f;

        if (closestDistance < shakeDistance)
        {
            intensity = 1f - Mathf.Clamp01(
                closestDistance / shakeDistance
            );
        }

        if (intensity <= 0f)
        {
            transform.localPosition = originalPosition;
            return;
        }

        float shakeX = (Mathf.PerlinNoise(
            Time.time * shakeSpeed, 0f) - 0.5f) * 2f;

        float shakeY = (Mathf.PerlinNoise(
            0f, Time.time * shakeSpeed) - 0.5f) * 2f;

        Vector3 shakeOffset = new Vector3(
            shakeX, shakeY, 0f
        ) * maxShake * intensity;

        transform.localPosition = originalPosition + shakeOffset;
    }

    void OnDisable()
    {
        transform.localPosition = originalPosition;
    }
}