using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BalanceManager))]
public class UIManager : MonoBehaviour {
    public Action OnButtonPlayPressed;

    [SerializeField] private SettingsManager _settingsManager;

    [SerializeField] private TextMeshProUGUI _totalBalanceLabel;
    [SerializeField] private TextMeshProUGUI _roundbalanceLabel;
    [SerializeField] private TextMeshProUGUI _roundPriceLabel;

    [SerializeField] private TextMeshProUGUI _ticketCostLabel;
    [SerializeField] private TextMeshProUGUI _cardsAmountLabel;

    private BalanceManager _balanceManager;

    public void ButtonPlayPressed() {
        OnButtonPlayPressed?.Invoke();
    }

    public void SettingsUIUpdate() {
        _roundPriceLabel.text = $"{_balanceManager.TotalCost}";
        _ticketCostLabel.text = $"{_balanceManager.TicketCost}";
        _cardsAmountLabel.text = $"{_settingsManager.CardsAmount}";
    }

    private void Awake() {
        _balanceManager = GetComponent<BalanceManager>();
        _balanceManager.OnBalanceUpdate += UIUpdate;

        SettingsUIUpdate();
    }

    private void OnDestroy() {
        _balanceManager.OnBalanceUpdate -= UIUpdate;
    }

    private void UIUpdate(int totalBalance, int roundBalance) {
        _totalBalanceLabel.text = $"{totalBalance}";

        _roundbalanceLabel.text = roundBalance == 0 ? "0" : $"+{roundBalance}";
        _roundbalanceLabel.color = roundBalance == 0 ? Color.white : Color.green;
    }
}