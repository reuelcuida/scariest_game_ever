using UnityEngine;

public class NextbotLookAtPlayer : MonoBehaviour
{
    private Transform playerCamera;

    void Start()
    {
        if (Camera.main != null)
            playerCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (playerCamera == null)
        {
            if (Camera.main != null)
                playerCamera = Camera.main.transform;

            if (playerCamera == null)
                return;
        }

        Vector3 direction = playerCamera.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}