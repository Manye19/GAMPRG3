using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    public Animator transition;
    public float timeForTransition;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RoomEntered(Passageway p_passageway)
    {
        StartCoroutine(LoadRoom());
    }

    IEnumerator LoadRoom()
    {
        //Play anim
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(timeForTransition);
    }
}
