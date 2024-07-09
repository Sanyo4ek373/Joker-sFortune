using UnityEngine;

public class CardsDeck : MonoBehaviour {
    [SerializeField] BalanceManager _balanceManager;

    [SerializeField] Card[] _cards;
    [SerializeField] int _deckSize;

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