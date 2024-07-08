using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] SpriteManager _spriteManager;
    [SerializeField] BalanceManager _balanceManager;

    private int _jocker = 7;
    private int _diamond = 1;

    private int _jockerWinChance = 13;
    private int _diamondWinChance = 7;   

    private int _maxWinValue = 30;

    public int InitializeCard(int winChance, int winValueMultiplier) {
        int totalPrize = WinValueRoll(winValueMultiplier);
        int cardType = CardRoll(winChance);

        _spriteManager.SetCardSprite(cardType);
        _balanceManager.CalculateTotalPrize(cardType, totalPrize);

        return cardType;
    }

    private int CardRoll(int winChance) {
        int jockerWinCondition = Random.Range(0, _jockerWinChance - winChance);
        if (jockerWinCondition == 0) return _jocker;

        int diamondWinCondition = Random.Range(0, _diamondWinChance - winChance);
        if (diamondWinCondition == 0) return _diamond;
        else return 0;
    }

    private int WinValueRoll(int winValueMultiplier) {
        return Random.Range(1 * winValueMultiplier, _maxWinValue * winValueMultiplier);
    }
}