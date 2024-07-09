using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private SpriteManager _spriteManager;
    [SerializeField] private BalanceManager _balanceManager;

    private int _jockerWinChance = 13;
    private int _diamondWinChance = 7;   

    private int _maximumWinValue = 10;

    public CardType InitializeCard(int winChance, int winValueMultiplier) {
        int totalPrize = WinValueRoll(winValueMultiplier);
        CardType _cardType = CardRoll(winChance);

        _spriteManager.SetCardSprite(_cardType);
        if (_cardType == CardType.Jocker || _cardType == CardType.Diamond ) {
            _balanceManager.CalculateTotalPrize(_cardType, totalPrize);
        }
        return _cardType;
    }

    public void ChangeCardState(bool cardState) {
        _spriteManager.ChangeCardSprite(cardState);
    }

    private CardType CardRoll(int winChance) {
        int jockerWinCondition = Random.Range(0, _jockerWinChance - winChance);
        if (jockerWinCondition == 0) return CardType.Jocker;

        int diamondWinCondition = Random.Range(0, _diamondWinChance - winChance);
        if (diamondWinCondition == 0) return CardType.Diamond;
        else return CardType.Default;
    }

    private int WinValueRoll(int winValueMultiplier) {
        return Random.Range(1 * winValueMultiplier, _maximumWinValue * winValueMultiplier);
    }
}