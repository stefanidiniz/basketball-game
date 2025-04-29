using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public Button[] menuButtons;
    private int currentIndex = 0;
    private float inputCooldown = 0.3f;
    private float lastInputTime;

    private UIinput inputActions;
    void Awake()
    {
        inputActions = new UIinput();
        inputActions.UI.Navigate.performed += ctx => Navigate(ctx.ReadValue<Vector2>());
        inputActions.UI.Submit.performed += ctx => Select();
    }
    void OnEnable()
    {
        inputActions.Enable();
        HighlightCurrentButton();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }
    void Navigate(Vector2 direction)
    {
        if (Time.time - lastInputTime < inputCooldown)
            return;

        if (Mathf.Abs(direction.y) > 0.5f)
        {
            currentIndex += direction.y < 0 ? 1 : -1;
            currentIndex = Mathf.Clamp(currentIndex, 0, menuButtons.Length - 1);
            HighlightCurrentButton();
            lastInputTime = Time.time;
        }
    }
    void HighlightCurrentButton()
    {
        menuButtons[currentIndex].Select();
    }

    void Select()
    {
        menuButtons[currentIndex].onClick.Invoke();
    }
}
