using System;
using System.Collections;
using UnityEngine;

public class StreamVideo : MonoBehaviour
{
    public string mUrl = "http://192.168.178.67/html/cam_pic.php";
    public Renderer mRenderer = null;
    public Material mMaterialSource = null;
    private DateTime mTimer = DateTime.MinValue;
    private Material mMaterialInstance = null;
    private int mFrameRate = 0;
    private int mFrameCount = 0;

    IEnumerator Start()
    {
        if (string.IsNullOrEmpty(mUrl))
        {
            Debug.LogError("Error: URL not set!");
            yield break;
        }

        if (mMaterialSource)
        {
            mMaterialInstance = Instantiate<Material>(mMaterialSource);
        }
        else
        {
            Debug.LogError("Error: Material not set!");
            yield break;
        }

        if (mRenderer)
        {
            mRenderer.material = mMaterialInstance;
        }
        else
        {
            Debug.LogError("Error: Renderer not set!");
            yield break;
        }

        Texture2D textureOld = null;

        while (true)
        {
            WWW www = new WWW(mUrl);
            yield return www;

            Texture2D textureNew = www.texture;

            bool error = !string.IsNullOrEmpty(www.error);
            if (!error)
            {
                if (mTimer < DateTime.Now)
                {
                    mTimer = DateTime.Now + TimeSpan.FromSeconds(1);
                    mFrameRate = mFrameCount;
                    mFrameCount = 0;
                }
                else
                {
                    ++mFrameCount;
                }

                mMaterialInstance.mainTexture = textureNew;

            }
            www.Dispose();
            if (error)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }

            if (textureOld)
            {
                DestroyImmediate(textureOld);
            }
            textureOld = textureNew;
            yield return null;
        }
    }
}
