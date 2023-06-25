using UnityEngine;

[System.Serializable]
public class ScoreSaveData
{
    public int goldAmount;
    public int gemAmount;

    public ScoreSaveData(GameManager gm)
    {
        goldAmount = gm.Gold;
        gemAmount = gm.Gem;
    }
}
