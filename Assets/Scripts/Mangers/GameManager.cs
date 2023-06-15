using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } = null;
    public event EventHandler<int> event_GoldValueChanged;
    public event EventHandler<int> event_GemValueChanged;


    int _gold = 10, _gem = 10;
    public int Gold => _gold;
    public int Gem => _gem;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    public void AddGold(int amount)
    {
        _gold += amount;
        event_GoldValueChanged?.Invoke(this, _gold);
    }
    public bool TryRemoveGold(int amount)
    {
        if (_gold - amount < 0) return false;
        else
        {
            _gold -= amount; 
            event_GoldValueChanged?.Invoke(this, _gold);
            return true;
        }
    }
    public void AddGem(int amount)
    {
        _gem += amount;
        event_GemValueChanged?.Invoke(this, _gem);
    }
    public bool TryRemoveGem(int amount)
    {
        if (_gem - amount < 0) return false;
        else
        {
            _gem -= amount;
            event_GemValueChanged?.Invoke(this, _gem);
            return true;
        }
    }
}
