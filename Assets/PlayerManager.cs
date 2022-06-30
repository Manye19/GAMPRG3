using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }

            return _instance;

        }
    }

    public GameObject player;
    public PlayerMovement playerMovement;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
