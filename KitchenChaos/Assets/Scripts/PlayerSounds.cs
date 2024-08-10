using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float stepTimer;
    private float stepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        stepTimer -= Time.deltaTime;

        if (stepTimer < 0)
        {
            stepTimer = stepTimerMax;
            if (player.IsWalking())
            {
                float volume = 3f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
                Debug.Log("Moving...");
            }
        }
    }
}
