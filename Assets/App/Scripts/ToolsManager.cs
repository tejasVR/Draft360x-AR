using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Draft360
{
    public class ToolsManager : MonoBehaviour
    {
        private ToolButton[] tools;

        private void Awake()
        {
            GetAllToolButtons();
        }

        private void OnEnable()
        {
            foreach (var tool in tools)
            {
                tool.OnSelected += ActivateTool;
            }
        }

        private void OnDisable()
        {
            foreach (var tool in tools)
            {
                tool.OnSelected -= ActivateTool;
            }
        }

        private void GetAllToolButtons()
        {
            tools = FindObjectsOfType<ToolButton>();
        }

        private void ActivateTool(ToolButton _toolToActivate)
        {
            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] != _toolToActivate)
                {
                    tools[i].Unselect();
                }
            }

            _toolToActivate.Select();
        }
    }
}


