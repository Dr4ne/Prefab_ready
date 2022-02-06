using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Import_easy : MonoBehaviour
{
    

    [MenuItem("My Tools/Prefab_ready_SM_TooN")]
    
    static void Prefab_ready_SM_TooN()
    {
        List<GameObject> fileList = new List<GameObject>();

        foreach (GameObject o in Selection.gameObjects)
        {
            if (o.GetType() == typeof(GameObject))
            {
                fileList.Add(o);                
            }

            string tempPath = "Assets/Prefabs_temp/" + o.name + ".prefab";
            string newPath = "Assets/Prefabs_saved/" + o.name + "_Toon.prefab";
            string matPath = "Assets/Mats/M_TooN_" + o.name + ".mat";
            string baseColorTexPath = "Assets/Textures/" + o.name + "_BaseColor.png";
            string brushTexPath = "Assets/Textures/T_brush_01.png";
            string lutTexPath = "Assets/Textures/T_ToonLUT.tga";

            // Make sure the file names are unique, in case an existing Prefab has the same name, if it does, append a number.
            tempPath = AssetDatabase.GenerateUniqueAssetPath(tempPath);
            newPath = AssetDatabase.GenerateUniqueAssetPath(newPath);
            matPath = AssetDatabase.GenerateUniqueAssetPath(matPath);
            
            // Instantiate imported mesh and save it as Prefab, then destroy the instance
            var SceneObject = Instantiate(o);
            PrefabUtility.SaveAsPrefabAsset (SceneObject, tempPath);
            DestroyImmediate (SceneObject);


            // load previously created prefab in isolated scene
            var loadedPrefab = PrefabUtility.LoadPrefabContents(tempPath);

            //add Meshcollider to prefab and set it to static
            loadedPrefab.AddComponent<MeshCollider>();
            loadedPrefab.isStatic = true;
            loadedPrefab.transform.position = new Vector3(0,0,0);

            //create new material from selected Shader, set textures and save it
            Material new_material = new Material(Shader.Find("Toon/SH_Toon_Custom_02"));
            Texture2D m_MainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(baseColorTexPath, typeof(Texture2D));
            Texture2D m_Lut = (Texture2D)AssetDatabase.LoadAssetAtPath(lutTexPath, typeof(Texture2D));
            Texture2D m_Brush = (Texture2D)AssetDatabase.LoadAssetAtPath(brushTexPath, typeof(Texture2D));
            new_material.SetTexture("_Text01", m_MainTexture);
            new_material.SetTexture("_Text02", m_Brush);
            new_material.SetTexture("_LUT", m_Lut);
            AssetDatabase.CreateAsset(new_material, matPath);
            AssetDatabase.SaveAssets();
            Renderer rend = loadedPrefab.GetComponent<Renderer>();
            rend.sharedMaterial = new_material;

            // save final prefab and clean temp files / memory 
            PrefabUtility.SaveAsPrefabAsset(loadedPrefab, newPath);
            PrefabUtility.UnloadPrefabContents(loadedPrefab);
            AssetDatabase.DeleteAsset(tempPath);          
            Debug.Log("<<<<----    Prefab_Ready Toon processed: "+ o.name + "    ---->>>>");
        }
    }

    
    [MenuItem("My Tools/Prefab_ready_SM_Holo")]
    
    static void Prefab_ready_SM_Holo()
    {
        List<GameObject> fileList = new List<GameObject>();

        foreach (GameObject o in Selection.gameObjects)
        {
            if (o.GetType() == typeof(GameObject))
            {
                fileList.Add(o);                
            }

            string tempPath = "Assets/Prefabs_temp/" + o.name + ".prefab";
            string newPath = "Assets/Prefabs_saved/" + o.name + "_Holo.prefab";
            string matPath = "Assets/Mats/M_Holo_" + o.name + ".mat";
            string baseColorTexPath = "Assets/Textures/" + o.name + "_BaseColor.png";
            string hatchPath = "Assets/Textures/T_TexturesCom_MetalRollup0010_1_seamless_S.jpg";

            // Make sure the file names are unique, in case an existing Prefab has the same name, if it does, append a number.
            tempPath = AssetDatabase.GenerateUniqueAssetPath(tempPath);
            newPath = AssetDatabase.GenerateUniqueAssetPath(newPath);
            matPath = AssetDatabase.GenerateUniqueAssetPath(matPath);
            
            // Instantiate imported mesh and save it as Prefab, then destroy the instance
            var SceneObject = Instantiate(o);
            PrefabUtility.SaveAsPrefabAsset (SceneObject, tempPath);
            DestroyImmediate (SceneObject);


            // load previously created prefab in isolated scene
            var loadedPrefab = PrefabUtility.LoadPrefabContents(tempPath);

            //add Meshcollider to prefab and set it to static
            loadedPrefab.AddComponent<MeshCollider>();
            loadedPrefab.isStatic = true;
            loadedPrefab.transform.position = new Vector3(0,0,0);

            //create new material from selected Shader, set textures and float values then save it
            Material new_material = new Material(Shader.Find("Holo/SH_Holo"));
            Texture2D m_MainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(baseColorTexPath, typeof(Texture2D));
            Texture2D m_hatch = (Texture2D)AssetDatabase.LoadAssetAtPath(hatchPath, typeof(Texture2D));
            new_material.SetTexture("_Texture01", m_MainTexture);
            new_material.SetTexture("_Texture02", m_hatch);
            new_material.SetVector("_TintColor", new Vector4(0,0,110,0));
            new_material.SetFloat("_HatchSpeedX", 0f);
            new_material.SetFloat("_HatchSpeedY", 7f);
            new_material.SetFloat("_HatchSize", 5.7f);
            new_material.SetFloat("_HatchStrength", 0.17f);
            new_material.SetFloat("_HoloShakeSpeed", 35f);
            new_material.SetFloat("_HoloShakeAmp", 5f);
            new_material.SetFloat("_HoloDist", 0.002f);
            new_material.SetFloat("_HoloAmount", 1f);
            AssetDatabase.CreateAsset(new_material, matPath);
            AssetDatabase.SaveAssets();
            Renderer rend = loadedPrefab.GetComponent<Renderer>();
            rend.sharedMaterial = new_material;

            // save final prefab and clean temp files / memory 
            PrefabUtility.SaveAsPrefabAsset(loadedPrefab, newPath);
            PrefabUtility.UnloadPrefabContents(loadedPrefab);
            AssetDatabase.DeleteAsset(tempPath);          
            Debug.Log("<<<<----    Prefab_Ready Holo processed: "+ o.name + "    ---->>>>");
        }
    }
}
