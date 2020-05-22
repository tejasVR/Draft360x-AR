using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NativeGallery;

namespace Draft360
{
    public class GalleryManager : MonoBehaviour
    {
		[SerializeField] GameObject imageQuadPrefab;

		private void Start()
		{
			//CreateImageQuad();
		}

		public void BringUpGallery()
        {
			PickImage(512);
		}

		private void PickImage(int maxSize)
		{
			NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
			{
				Debug.Log("Image path: " + path);
				if (path != null)
				{
					// Create Texture from selected image
					Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
					if (texture == null)
					{
						Debug.Log("Couldn't load texture from " + path);
						return;
					}

					// Assign the image texture to the ImageQuad prefab in the Resources folder
					//var imagePrefab = Resources.Load<ImageQuad>("ImageQuad");
					//imagePrefab.Initialize(texture);

					var imagePrefab = Instantiate(imageQuadPrefab);
					imagePrefab.GetComponent<ImageQuad>().Initialize(texture);

					imagePrefab.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
					imagePrefab.transform.forward = Camera.main.transform.forward;
					imagePrefab.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);
					
					PrefabCreator.Instance.SetARPrefab(imagePrefab);

					imagePrefab.SetActive(false);

					/*
					// Assign texture to a temporary quad and destroy it after 5 seconds
					GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
					quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
					quad.transform.forward = Camera.main.transform.forward;
					quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

					Material material = quad.GetComponent<Renderer>().material;
					if (!material.shader.isSupported) // happens when Standard shader is not included in the build
						material.shader = Shader.Find("Legacy Shaders/Diffuse");

					material.mainTexture = texture;
					*/

					//PrefabCreator.Instance.SetARPrefab(Resources.Load<ImageQuad>("ImageQuad").gameObject);

					// Don't destroy quad
					//Destroy(quad, 5f);

					// If a procedural texture is not destroyed manually, 
					// it will only be freed after a scene change
					//Destroy(texture, 5f);
				}
			}, "Select a PNG image", "image/png");

			Debug.Log("Permission result: " + permission);
		}

		private void CreateImageQuad()
		{
			GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
			quad.transform.forward = Camera.main.transform.forward;
			//quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);
		}

		private void PickVideo()
		{
			NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery((path) =>
			{
				Debug.Log("Video path: " + path);
				if (path != null)
				{
					// Play the selected video
					Handheld.PlayFullScreenMovie("file://" + path);
				}
			}, "Select a video");

			Debug.Log("Permission result: " + permission);
		}
	}
}
