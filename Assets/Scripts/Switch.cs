﻿using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour, Noun 
{

    public enum SwitchType
    {
        None,
        Invert,
        Noun,
        Verb
    }
    public SwitchType switchType = SwitchType.None;

    RuleManager.Rule switchRule;
    private bool isActivated = false;

    bool needsToSpawn = false;
    float spawnTime = 2f;
    float spawnTimeCounter = 0f;
	
    void Update () 
    {
        if (needsToSpawn)
        {
            if (spawnTimeCounter >= spawnTime)
            {
                Spawn();
            }
            spawnTimeCounter += Time.deltaTime;
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("Grab1"))
        {
            if(isActivated == false && other.tag == "CanInteract")
            {
                isActivated = true;
                Debug.Log("Switch Activated");
            }
        }
    }

    public void Init(SwitchType switchType)
    {
        this.switchType = switchType;

        Spawn();
    }

    public AdjectiveType Adjective
    {
        get { return AdjectiveType.None; }
        set { }
    }

    public void Spawn()
    {
        float randX = UnityEngine.Random.Range(-2.96f, 3.06f);
        float randY = UnityEngine.Random.Range(-1.13f, 3.14f);
        this.gameObject.transform.position = new Vector3(randX, randY, 0.0f);
        gameObject.SetActive(true);
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        spawnTimeCounter = 0f;
    }

    public void Kicked(int player, Vector3 direction)
    {
        switch (switchType)
        {
            case SwitchType.Invert:
                RuleManager.instance.InvertRule();
                break;

            case SwitchType.Noun:
                RuleManager.instance.ChangeNoun();
                break;

            case SwitchType.Verb:
                RuleManager.instance.ChangeVerb();
                break;
        }
        Reset();
    }

    public void Tagged(int player) { }
    public void Grabbed(int player) { }
}
