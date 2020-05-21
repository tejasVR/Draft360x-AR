using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Draft360
{
    public class GridVisibility : MonoBehaviour
    {
        private bool visible = true;

        public void ToggleGridVisibility()
        {
            if (visible)
            {
                visible = false;
            }
            else
            {
                visible = true;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(visible);
            }
        }
    }
}


