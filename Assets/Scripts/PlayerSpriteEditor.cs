using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlayerSpriteEditor : EditorWindow  {
    
    private static Dictionary<string, string> map = new Dictionary<string, string>();
    private static string _spriteName;
    
    
    [MenuItem("Space-Game/Player Sprite Tools/Rename Player Sprite Parts")]
    public static void ShowPlayerSpriteEditor()
    {
        Reset();
        DoConvert();
    }

    private static void DoConvert()
    {
        Object selectedObj = Selection.activeObject;

        if (!AssetDatabase.Contains(selectedObj))
        {
            Debug.Log("Invalid object or something. No conversion for you!");
            return;
        }

        TextureImporter ti = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(selectedObj)) as TextureImporter;
        if (ti == null)
        {
            Debug.Log("Texture Importer issue. Not converting.");
            return;
        }
        
        SpriteMetaData[] sprites = ti.spritesheet;
        if (sprites.Length > 0)
        {
            _spriteName = sprites[0].name.Substring(0, sprites[0].name.Length - 2);
        }

        SetupMap();

        if (sprites == null || sprites.Length < map.Count)
        {
            Debug.Log("Spritesheet issue. Not converting");
            return;
        }
        
        for (int x = 0; x < sprites.Length; x++)
        {
            if (map.ContainsKey(sprites[x].name))
            {
                sprites[x].name = map[sprites[x].name];
            }
        }
        ti.spritesheet = sprites;

        EditorUtility.SetDirty(ti);
        ti.SaveAndReimport();
        Debug.Log("Looks like everything worked!");
    }

    private static void Reset()
    {
        map.Clear();
        _spriteName = string.Empty;
    }

    private static void SetupMap()
    {
        addMap("_60", "idleUp");
        addMap("_69", "idleLeft");
        addMap("_78", "idleDown");
        addMap("_87", "idleRight");

        addWalk("Up", 61, 68);
        addWalk("Left", 70, 77);
        addWalk("Down", 79, 86);
        addWalk("Right", 88, 95);
    }

    private static void addWalk(string direction, int start, int end)
    {
        if (end < start)
        {
            return;
        }

        int dCount = 0;
        for (int x = start; x <= end; x++){
            addMap(string.Format("_{0}", x), string.Format("walk{0}{1}", direction, dCount++));
        }
    }

    private static void addMap(string suffix, string newName)
    {
        map.Add(string.Format("{0}{1}", _spriteName, suffix), newName);
    }
}
