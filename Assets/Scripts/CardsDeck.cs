using UnityEngine;

public class CardsDeck : MonoBehaviour {
    [SerializeField] private BalanceManager _balanceManager;

    [SerializeField] private Card[] _cards;
    [SerializeField] private int _deckSize;

    public void ShowDeck() {
        CardsDeckUpdate();
    }

    private void CardsDeckUpdate() {
        int diamondsAmount = 0;

        for (int i = 0; i < _deckSize; ++i) {
            CardType cardType = _cards[i].InitializeCard(_balanceManager.WinChance, _balanceManager.WinValue);
            if (cardType == CardType.Diamond) diamondsAmount += 1;
        }

        if (diamondsAmount >= 3) _balanceManager.CalculateDiamondsPrize(diamondsAmount);

        _balanceManager.SetRoundBalance(_deckSize);
    }
}