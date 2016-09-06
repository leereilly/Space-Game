using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class PlayerSpriteEditor : EditorWindow  {
    
    private Dictionary<string, string> map = new Dictionary<string, string>();

    private string _spriteName;
    
    [MenuItem("Space-Game/Player Sprite Editor")]
    public static void ShowPlayerSpriteEditor() 
    {
        var window = GetWindow<PlayerSpriteEditor>();
    }

    public void OnGUI()
    {
        GUILayout.Label("Change Player Sprite Names", EditorStyles.boldLabel);
        

        if (GUILayout.Button("Do it!"))
        {
            DoConvert();
        }
    }

    public void DoConvert()
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
    }

    private void SetupMap()
    {
        map.Clear();
        addMap("_60", "idleUp");
        addMap("_69", "idleLeft");
        addMap("_78", "idleDown");
        addMap("_87", "idleRight");

        addWalk("Up", 61, 68);
        addWalk("Left", 70, 77);
        addWalk("Down", 79, 86);
        addWalk("Right", 88, 95);
    }

    private void addWalk(string direction, int start, int end)
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

    private void addMap(string suffix, string newName)
    {
        map.Add(string.Format("{0}{1}", _spriteName, suffix), newName);
    }
}
