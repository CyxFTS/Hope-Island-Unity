using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//图片格式设置
public class TextuseSetting : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        
        TextureImporter importer = (TextureImporter)assetImporter;
        importer.textureType = TextureImporterType.Sprite;
    }
}
