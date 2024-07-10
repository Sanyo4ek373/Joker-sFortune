using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private SpriteManager _spriteManager;
    [SerializeField] private BalanceManager _balanceManager;

    private SpriteRenderer _cardSprite;

    private int _jockerWinChance = 30;
    private int _diamondWinChance = 20;   

    private int _maximumWinValue = 10;

    public CardType InitializeCard(int winChance, int winValueMultiplier) {
        int totalPrize = WinValueRoll(winValueMultiplier);
        CardType _cardType = CardRoll(winChance);

        _cardSprite.sprite = _spriteManager.SetCardSprite(_cardType);
        if (_cardType == CardType.Jocker || _cardType == CardType.Diamond ) {
            _balanceManager.CalculateTotalPrize(_cardType, totalPrize);
        }
        return _cardType;
    }

    public void ChangeCardState(bool cardState) {
        if (cardState) _spriteManager.OnCardSpriteChange += ChangeSpriteToBack;
        else {
            _cardSprite.sprite = _spriteManager.ChangeCardState(cardState);
            _spriteManager.OnCardSpriteChange -= ChangeSpriteToBack;
        }
    }

    private void Awake() {
        _cardSprite = GetComponent<SpriteRenderer>();

        _spriteManager.OnCardSpriteChange += ChangeSpriteToBack;
    }

    private void OnDestroy() {
        _spriteManager.OnCardSpriteChange -= ChangeSpriteToBack;
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

    private void ChangeSpriteToBack() {
        _cardSprite.sprite = _spriteManager.ChangeCardState(true);
    }
}