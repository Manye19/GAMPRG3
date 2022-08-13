using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Health unitHealth;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void OnDestroy()
    {

    }

    public virtual void Init()
    {
        Health health = GetComponent<Health>();
        health.onDeathEvent.AddListener(Death);        
    }
    public virtual void Deinit()
    {
        Health health = GetComponent<Health>();
        health.onDeathEvent.RemoveListener(Death);
    }
    protected virtual void Death()
    {
        Deinit();
    }
}
