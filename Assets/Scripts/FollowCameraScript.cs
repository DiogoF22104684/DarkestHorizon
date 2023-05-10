using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localPosition = new Vector2(-8, -4);
    }

    public void PlayerInstantiate()
    {
        Instantiate(playerPrefab);
    }
}
