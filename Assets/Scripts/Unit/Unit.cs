using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected float maxHealth;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        Health health = GetComponent<Health>();
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
        health.SetValues(maxHealth);
        health.Init();
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
