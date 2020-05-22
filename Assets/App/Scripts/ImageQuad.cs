using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Draft360
{
    public class ImageQuad : MonoBehaviour
    {
        private MeshRenderer rend;

        private void Awake()
        {
            rend = GetComponentInChildren<MeshRenderer>();
        }

        public void Initialize(Texture texture)
        {
            rend.material.mainTexture = texture;
        }

    }
}


