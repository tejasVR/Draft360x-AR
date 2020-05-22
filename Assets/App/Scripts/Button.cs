using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Draft360
{
    public class Button : MonoBehaviour
    {
        [SerializeField] Sprite normalSprite;
        [SerializeField] Sprite selectedSprite;

        private Image buttonImage;
                        
        void Start()
        {
            buttonImage = GetComponent<Image>();                       
        }

        // Update is called once per frame
        public void Select()
        {
            buttonImage.sprite = selectedSprite;
        }

        public void Unselect()
        {
            buttonImage.sprite = normalSprite;
        }
    }
}