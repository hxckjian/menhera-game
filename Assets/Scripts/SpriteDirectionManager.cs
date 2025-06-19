using UnityEngine;

public class SpriteDirectionManager : MonoBehaviour
{
    public enum Character { MC, Yandere }
    public enum Direction { Left, Right, Up, Down }

    [System.Serializable]
    private class CharacterAnimatorData
    {
        public Animator animator;
    }

    [Header("Character Animator Setup")]
    [SerializeField] private CharacterAnimatorData MC;
    [SerializeField] private CharacterAnimatorData Yandere;

    public void Face(Character character, Direction direction)
    {
        var data = character == Character.MC ? MC : Yandere;

        Vector2 directionVector = DirectionToVector(direction);

        data.animator.SetFloat("Horizontal", directionVector.x);
        data.animator.SetFloat("Vertical", directionVector.y);
    }

    private Vector2 DirectionToVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left: return new Vector2(-1, 0);
            case Direction.Right: return new Vector2(1, 0);
            case Direction.Up: return new Vector2(0, 1);
            case Direction.Down: return new Vector2(0, -1);
            default: return Vector2.zero;
        }
    }
}
