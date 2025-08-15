using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CharacterExpressionManager : MonoBehaviour
{
    [SerializeField] private Image characterImage; 
    [SerializeField] private List<CharacterExpressions> characterSprites;

    [System.Serializable]
    public class CharacterExpressions
    {
        public string characterName;
        public List<ExpressionSprite> expressions;
    }

    [System.Serializable]
    public class ExpressionSprite
    {
        public string expressionName; 
        public Sprite sprite;
    }

    public void SetExpression(string character, string expression)
    {
        var characterData = characterSprites.Find(c => c.characterName == character);
        if (characterData == null) return;

        var expr = characterData.expressions.Find(e => e.expressionName == expression);
        if (expr == null) return;

        characterImage.sprite = expr.sprite;
    }
}

