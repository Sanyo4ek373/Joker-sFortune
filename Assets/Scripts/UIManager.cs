using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BalanceManager))]
public class UIManager : MonoBehaviour {
    public Action OnButtonPlayPressed;

    [SerializeField] private SettingsManager _settingsManager;
    [SerializeField] private SpriteManager _spriteManager;

    [SerializeField] private TextMeshProUGUI _totalBalanceLabel;
    [SerializeField] private TextMeshProUGUI _roundbalanceLabel;
    [SerializeField] private TextMeshProUGUI _roundPriceLabel;

    [SerializeField] private TextMeshProUGUI _ticketCostLabel;
    [SerializeField] private TextMeshProUGUI _cardsAmountLabel;

    private BalanceManager _balanceManager;

    private bool _isBusy = false;

    private float _roundTime = 2f;

    public void ButtonPlayPressed() {
        if (_isBusy) return;

        OnButtonPlayPressed?.Invoke();
        StartCoroutine(RoundTime(_roundTime));
    }

    public void SettingsUIUpdate() {
        _roundPriceLabel.text = $"{_balanceManager.TotalCost}";
        _ticketCostLabel.text = $"{_balanceManager.TicketCost}";
        _cardsAmountLabel.text = $"{_settingsManager.CardsAmount}";
    }

    private void Awake() {
        _balanceManager = GetComponent<BalanceManager>();
        _balanceManager.OnBalanceUpdate += UIUpdate;
    }

    private void Start() {
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

    private IEnumerator RoundTime(float waitTime) {
        _isBusy = true;

        yield return new WaitForSeconds(waitTime);

        _spriteManager.ChangeCardSprite();
        _isBusy = false;
    }
}