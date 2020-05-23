using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Draft360
{
    public class ChangeColorManager : MonoBehaviour
    {
        private ChangeColor[] changeColorButtons;

        private void Awake()
        {
            GetAllColorButtons();
        }

        private void OnEnable()
        {
            foreach (var color in changeColorButtons)
            {
                color.OnSelected += ActivateColor;
            }
        }

        private void OnDisable()
        {
            foreach (var color in changeColorButtons)
            {
                color.OnSelected -= ActivateColor;
            }
        }

        private void GetAllColorButtons()
        {
            changeColorButtons = FindObjectsOfType<ChangeColor>();
        }

        private void ActivateColor(ChangeColor _changeColorButton)
        {
            for (int i = 0; i < changeColorButtons.Length; i++)
            {
                if (changeColorButtons[i] != _changeColorButton)
                {
                    changeColorButtons[i].Unselect();
                }
            }

            _changeColorButton.Select();
        }
    }
}

