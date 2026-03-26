using UnityEngine;
using UnityEngine.UIElements;

public class RuntimeUI : MonoBehaviour
{
    private Button _button;
    private Toggle _toggle;

    private int _clickCount;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        _button = uiDocument.rootVisualElement.Q("button") as Button;
        _toggle = uiDocument.rootVisualElement.Q("toggle") as Toggle;

        _button.RegisterCallback<ClickEvent>(PrintClickMessage);

        var _inputFields = uiDocument.rootVisualElement.Q("input-message");
        _inputFields.RegisterCallback<ChangeEvent<string>>(InputMessage);
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent e)
    {
        ++_clickCount;

        Debug.Log($"{"button"}was clicked");
        Debug.Log(_toggle.value ? "Count:" + _clickCount : "");
    }

    public static void InputMessage(ChangeEvent<string> e)
    {
        Debug.Log($"{e.newValue} -> {e.target}");
    }
}
