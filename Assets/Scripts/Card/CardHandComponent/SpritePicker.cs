using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Card.CardHandComponent
{
    public class SpritePicker : MonoBehaviour
    {
        private SpriteRenderer MyRenderer { get; set; }
        public bool Ready { get; private set; }

        private void Awake()
        {
            MyRenderer = GetComponent<SpriteRenderer>();

            var sprite = MyRenderer.sprite;
            
            float height = sprite.texture.height;
            float width = sprite.texture.width;

            StartCoroutine(LoadImage("https://picsum.photos/" + width + "/" + height));
        }
        

        private IEnumerator LoadImage(string url) 
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                Ready = true;
            }
            else
            {
                MyRenderer.sprite = ConvertToSprite(((DownloadHandlerTexture) request.downloadHandler).texture);
                Ready = true;
            }
        }
        
        private Sprite ConvertToSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}