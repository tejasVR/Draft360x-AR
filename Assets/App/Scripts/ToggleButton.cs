using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite selectedSprite;

    //[SerializeField] UnityEvent selectedEvent;
    //[SerializeField] UnityEvent unselectedEvent;

    [SerializeField] bool selectOnStart;

    private bool selected;
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    void Start()
    {
        if (selectOnStart)
        {
            Select();
        }
    }

    public void Toggle()
    {
        if (selected)
        {
            Unselect();
        }
        else
        {
            Select();
        }
    }

    private void Select()
    {
        selected = true;
        //OnSelected(this);

        buttonImage.sprite = selectedSprite;

        //selectedEvent?.Invoke();

    }

    private void Unselect()
    {
        selected = false;
        buttonImage.sprite = normalSprite;

        //unselectedEvent?.Invoke();
    }
}
