using TMPro;
using UnityEngine;

public class Card : MonoBehaviour {
    [SerializeField] private SpriteManager _spriteManager;
    [SerializeField] private BalanceManager _balanceManager;

    [SerializeField] private TextMeshPro _winAmountLabel;

    private SpriteRenderer _cardSprite;

    private int _jockerWinChance = 25;
    private int _diamondWinChance = 20;   

    private int _maximumWinValue = 15;

    public CardType InitializeCard(int winChance, int winValueMultiplier, int diamondsAmount) {
        int winAmount = WinValueRoll(winValueMultiplier);

        CardType cardType = CardRoll(winChance, diamondsAmount);
        _cardSprite.sprite = _spriteManager.SetCardSprite(cardType);
        SetWinAmountLabel(winAmount, cardType);

        if (cardType == CardType.Jocker || cardType == CardType.Diamond ) {
            _balanceManager.CalculateTotalPrize(cardType, winAmount);
        }
        return cardType;
    }

    public void ChangeCardState(bool cardState) {
        if (cardState) _spriteManager.OnCardSpriteChange += ChangeSpriteToBack;
        else {
            _cardSprite.sprite = _spriteManager.ChangeCardState(cardState);
            _winAmountLabel.text = "";

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

    private CardType CardRoll(int winChance, int diamondsAmount) {
        int jockerWinCondition = Random.Range(0, _jockerWinChance - winChance);
        if (jockerWinCondition == 0) return CardType.Jocker;

        if (diamondsAmount != 3) {
            int diamondWinCondition = Random.Range(0, _diamondWinChance - winChance);
            if (diamondWinCondition == 0) return CardType.Diamond;
        } 
        return CardType.Default;
    }

    private int WinValueRoll(int winValueMultiplier) {
        return Random.Range(1 * winValueMultiplier, _maximumWinValue * winValueMultiplier);
    }

    private void ChangeSpriteToBack() {
        _cardSprite.sprite = _spriteManager.ChangeCardState(true);
        _winAmountLabel.text = "";
    }

    private void SetWinAmountLabel(int value, CardType cardType) {
        _winAmountLabel.text = $"{value}";

        switch (cardType) {
            case CardType.Jocker:
                _winAmountLabel.color = Color.red;
                break;

            case CardType.Diamond:
                _winAmountLabel.color = Color.blue;
                break;

            default: 
                _winAmountLabel.color = Color.black; 
                break;
        }
    }
}