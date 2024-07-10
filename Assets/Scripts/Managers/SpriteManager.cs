using System;
using UnityEngine;

public class SpriteManager : MonoBehaviour {
    public Action OnCardSpriteChange;

    [SerializeField] private Sprite[] _defaultSprites;
    [SerializeField] private Sprite _jockerSprite;
    [SerializeField] private Sprite _diamondSprite;
    [SerializeField] private Sprite _cardBackSprite;
    [SerializeField] private Sprite _cardInactiveSprite;

    public Sprite SetCardSprite(CardType cardType) {
        if (cardType == CardType.Default) {
            int i = UnityEngine.Random.Range(0, _defaultSprites.Length);
            return _defaultSprites[i];
        }
        else if (cardType == CardType.Jocker) {
            return _jockerSprite;
        }
        else return _diamondSprite;
    }

    public void ChangeCardSprite() {
        OnCardSpriteChange?.Invoke();
    }

    public Sprite ChangeCardState(bool cardState) {
        if (cardState) return _cardBackSprite;
        else return _cardInactiveSprite;
    }
}