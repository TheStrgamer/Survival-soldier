using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerCamera : NetworkBehaviour
{

    Vector3 offset = Vector3.zero;
    public float maxOffsetTime = 2f;
    private float offsetTime = 0f;

    private Transform player;
    private float camDistanceMultiplier = 0;

    [Client]
    public IEnumerator Shake(float duration, float magnitude)
    {
        offsetTime = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float zOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            elapsedTime += Time.deltaTime;
            offset = new Vector3(xOffset, yOffset, zOffset);
            yield return null;
        }
        offset = Vector3.zero;
    }

    [Client]
    public void givePosition(Transform player, float camDistanceMultiplier)
    {
        this.player = player;
        this.camDistanceMultiplier = camDistanceMultiplier;
    }

    [Client]
    private void Update()
    {
        if (player == null) { return; }
        transform.position = new Vector3(player.position.x, 10 + 10 * camDistanceMultiplier, player.position.z - 9.22f - camDistanceMultiplier * 5)+offset;

    }
}
