using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Draft360
{
    public class ChangeColor : MonoBehaviour
    {
        public event Action<ChangeColor> OnSelected = delegate { };

        [SerializeField] Image circleImage;
        [SerializeField] Image selectedIndicator;
        [SerializeField] Color drawingColor;

        [Space(7)]
        [SerializeField] bool selectedOnStart;

        private bool selected;

        private void Awake()
        {

        }

        private void Start()
        {
            circleImage.color = drawingColor;
            
            if (selectedOnStart)
                Pressed();
        }

        public void Pressed()
        {
            DrawingManager.Instance.ChangeColor(drawingColor);

            OnSelected(this);
        }

        public void Select()
        {
            selectedIndicator.enabled = true;
        }

        public void Unselect()
        {
            selectedIndicator.enabled = false;
        }

    }
}
