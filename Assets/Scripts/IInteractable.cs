public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None  // Optional: for interactions with no direction requirement
}

public interface IInteractable
{
    void Interact();

    Direction RequiredDirection { get; }
}
