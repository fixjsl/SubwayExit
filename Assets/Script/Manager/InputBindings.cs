using UnityEngine.InputSystem;

public static class InputBindings
{
    private static InputAction _interact;
    private static InputAction _sprint;
    private static InputAction _attack;
    private static InputAction _dodge;
    private static InputAction _guard;
    private static InputAction _crouch;
    private static InputAction _inventory;

    public static string Interact  => _interact?.GetBindingDisplayString() ?? "?";
    public static string Sprint    => _sprint?.GetBindingDisplayString() ?? "?";
    public static string Attack    => _attack?.GetBindingDisplayString() ?? "?";
    public static string Dodge     => _dodge?.GetBindingDisplayString() ?? "?";
    public static string Guard     => _guard?.GetBindingDisplayString() ?? "?";
    public static string Crouch    => _crouch?.GetBindingDisplayString() ?? "?";
    public static string Inventory => _inventory?.GetBindingDisplayString() ?? "?";

    public static InputAction GetAction(string actionName) => actionName switch
    {
        "Interact"  => _interact,
        "Sprint"    => _sprint,
        "Attack"    => _attack,
        "Dodge"     => _dodge,
        "Guard"     => _guard,
        "Crouch"    => _crouch,
        "Inventory" => _inventory,
        _           => null
    };

    public static void Init(InputSystem_Actions actions)
    {
        var map = actions.PlayerAction;
        _interact  = map.Interact;
        _sprint    = map.Sprint;
        _attack    = map.Attack;
        _dodge     = map.Dodge;
        _guard     = map.Guard;
        _crouch    = map.Crouch;
        _inventory = map.Inventory;
    }
}
