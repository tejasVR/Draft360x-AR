using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Draft360
{
    public class ToolButton : MonoBehaviour
    {
        public event Action<ToolButton> OnSelected = delegate { };

        [SerializeField] Sprite normalSprite;
        [SerializeField] Sprite selectedSprite;

        [SerializeField] UnityEvent selectedEvent;
        [SerializeField] UnityEvent unselectedEvent;

        [SerializeField] bool selectOnStart;

        private Image buttonImage;
        private bool selected;

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

        public void Pressed()
        {
            OnSelected(this);
        }

        public void Select()
        {
            if (selected)
                return;

            selected = true;

            buttonImage.sprite = selectedSprite;

            selectedEvent?.Invoke();
        }

        public void Unselect()
        {
            buttonImage.sprite = normalSprite;

            unselectedEvent?.Invoke();

            selected = false;
        }
    }
}