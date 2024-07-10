using UnityEngine;

public class CardsDeck : MonoBehaviour {
    [SerializeField] private BalanceManager _balanceManager;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private SettingsManager _settingsManager;

    [SerializeField] private Card[] _cards; 

    private int _deckSize = 12;

    private void Awake() {
        _uIManager.OnButtonPlayPressed += CardsDeckUpdate;
    }

    private void OnDestroy() {
        _uIManager.OnButtonPlayPressed -= CardsDeckUpdate;
    }

    private void CardsDeckUpdate() {
        int diamondsAmount = 0;
    
        CheckDeckSize();

        for (int i = 0; i < _deckSize; ++i) {
            CardType cardType = _cards[i].InitializeCard(_balanceManager.WinChance, _balanceManager.WinValue, diamondsAmount);
            if (cardType == CardType.Diamond) diamondsAmount += 1;
        }

        if (diamondsAmount == 3) _balanceManager.CalculateDiamondsPrize(diamondsAmount);

        _balanceManager.SetRoundBalance(_deckSize);
    }

    private void CheckDeckSize() {
        if (_deckSize > _settingsManager.CardsAmount) {
            for (int i = 0; i < _deckSize - _settingsManager.CardsAmount; ++i) {
                _cards[_settingsManager.CardsAmount +i].ChangeCardState(false);
            }
        } else if (_deckSize < _settingsManager.CardsAmount) {
            for (int i = 0; i < _settingsManager.CardsAmount - _deckSize; ++i) {
                _cards[_deckSize + i].ChangeCardState(true);
            }
        }

        _deckSize = _settingsManager.CardsAmount;
    }
}