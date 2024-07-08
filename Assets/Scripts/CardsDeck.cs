using UnityEngine;

public class CardsDeck : MonoBehaviour {
    [SerializeField] BalanceManager _balanceManager;

    [SerializeField] Card[] _cards;
    [SerializeField] int _deckSize;

    private int _diamond = 1;

    public int WinChance {get; set;}
    public int WinValue {get; set;}

    public void ShowDeck() {
        CardsDeckUpdate();
    }

    private void CardsDeckUpdate() {
        int diamondsAmount = 0;

        for (int i = 0; i < _deckSize; ++i) {
            int cardType = _cards[i].InitializeCard(WinChance, WinValue);
            if (cardType == _diamond) diamondsAmount += 1;
        }

        if (diamondsAmount >= 3) _balanceManager.CalculateDiamondsPrize(diamondsAmount);
    }
}
