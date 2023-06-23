using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; } = null;
    public event EventHandler<int> event_ValueChanged_Gold;
    public event EventHandler<int> event_ValueChanged_Gem;

    [SerializeField] int startingGold = 10, startingGem = 10;

    int _gold, _gem;
    GridMaker _grid;
    public int Gold => _gold;
    public int Gem => _gem;
    public GridMaker Grid => _grid;

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

        _gold = startingGold;
        _gem = startingGem;

        Invoke("RefreshEvents", 1);
    }

    public void SetGrid(GridMaker grid) => _grid = grid;
    public void RestartTheGame()
    {
        SetGold(startingGold);
        SetGem(startingGem);

        Grid.Clear();
    }
    public void RefreshEvents()
    {
        event_ValueChanged_Gold?.Invoke(this, _gold);
        event_ValueChanged_Gem?.Invoke(this, _gem);
    }
    public void AddGold(int amount)
    {
        _gold += amount;
        event_ValueChanged_Gold?.Invoke(this, _gold);
    }
    public bool TryRemoveGold(int amount)
    {
        if (_gold - amount < 0) return false;
        else
        {
            _gold -= amount; 
            event_ValueChanged_Gold?.Invoke(this, _gold);
            return true;
        }
    }
    public void AddGem(int amount)
    {
        _gem += amount;
        event_ValueChanged_Gem?.Invoke(this, _gem);
    }
    public bool TryRemoveGem(int amount)
    {
        if (_gem - amount < 0) return false;
        else
        {
            _gem -= amount;
            event_ValueChanged_Gem?.Invoke(this, _gem);
            return true;
        }
    }
    public void TryRemoveGemForButtons(int amount) => TryRemoveGem(amount);
    public void TryRemoveGoldForButtons(int amount) => TryRemoveGold(amount);

    void SetGold(int amount)
    {
        _gold = amount;
        event_ValueChanged_Gold?.Invoke(this, _gold);
    }
    void SetGem(int amount)
    {
        _gem = amount;
        event_ValueChanged_Gem?.Invoke(this, _gem);
    }
}
