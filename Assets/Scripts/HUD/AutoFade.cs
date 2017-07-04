using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AutoFade : MonoBehaviour
{
	private static AutoFade m_Instance = null;
	private Material m_Material = null;
	private string m_LevelName = "";
	private int m_LevelIndex = 0;
	private bool m_Fading = false;

	private static AutoFade Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = (new GameObject("AutoFade")).AddComponent<AutoFade>();
			}
			return m_Instance;
		}
	}
	public static bool Fading
	{
		get { return Instance.m_Fading; }
	}

	private void Awake()
	{
		DontDestroyOnLoad(this);
		m_Instance = this;
		m_Material = Resources.Load<Material>("Plane_No_zTest");
		#if UNITY_EDITOR
		if (m_Material == null)
		{
			var resDir = new System.IO.DirectoryInfo(System.IO.Path.Combine(Application.dataPath, "Resources"));
			if (!resDir.Exists)
				resDir.Create();
			Shader s = Shader.Find("Plane/No zTest");
			if (s == null)
			{
				string shaderText = "Shader \"Plane/No zTest\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off Fog { Mode Off } BindChannels { Bind \"Color\",color } } } }";
				string path = System.IO.Path.Combine(resDir.FullName, "Plane_No_zTest.shader");
				Debug.Log("Shader missing, create asset: " + path);
				System.IO.File.WriteAllText(path, shaderText);
				UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.ForceSynchronousImport);
				UnityEditor.AssetDatabase.LoadAssetAtPath<Shader>("Resources/Plane_No_zTest.shader");
				s = Shader.Find("Plane/No zTest");
			}
			var mat = new Material(s);
			mat.name = "Plane_No_zTest";
			UnityEditor.AssetDatabase.CreateAsset(mat, "Assets/Resources/Plane_No_zTest.mat");
			m_Material = mat;

		}
		#endif
	}

	private void DrawQuad(Color aColor,float aAlpha)
	{
		aColor.a = aAlpha;
		m_Material.SetPass(0);
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Begin(GL.QUADS);
		GL.Color(aColor);   // moved here, needs to be inside begin/end
		GL.Vertex3(0, 0, -1);
		GL.Vertex3(0, 1, -1);
		GL.Vertex3(1, 1, -1);
		GL.Vertex3(1, 0, -1);
		GL.End();
		GL.PopMatrix();
	}

	private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		float t = 0.0f;
		while (t<1.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
			DrawQuad(aColor,t);
		}
		if (m_LevelName != "")
			SceneManager.LoadScene(m_LevelName);
		else
			SceneManager.LoadScene(m_LevelIndex);
		while (t>0.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
			DrawQuad(aColor,t);
		}
		m_Fading = false;
	}
	private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		m_Fading = true;
		StartCoroutine(Fade(aFadeOutTime, aFadeInTime, aColor));
	}

	public static void LoadLevel(int aLevelIndex,float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		if (Fading) return;
		switch(aLevelIndex)
		{
			case 2:
				Instance.m_LevelName = "Splash - TrapNCatch";
				break;

			case 8:
				Instance.m_LevelName = "Mission - 0";
				break;
			case 9:
				Instance.m_LevelName = "Mission - 1";
				break;
			case 10:
				Instance.m_LevelName = "Mission - 2";
				break;
			case 11:
				Instance.m_LevelName = "Mission - 3";
				break;
			case 12:
				Instance.m_LevelName = "Mission - 4";
				break;
			case 13:
				Instance.m_LevelName = "Mission - 5";
				break;

			case 15:
				Instance.m_LevelName = "Chapitre0";
				break;
			case 16:
				Instance.m_LevelName = "Chapitre1";
				break;
			case 17:
				Instance.m_LevelName = "Chapitre2";
				break;
			case 18:
				Instance.m_LevelName = "Chapitre3";
				break;
			case 19:
				Instance.m_LevelName = "Chapitre4";
				break;
			case 20:
				Instance.m_LevelName = "Chapitre5";
				break;

			case 7:
				Instance.m_LevelName = "Menu - Success";
				break;
			case 3:
				Instance.m_LevelName = "Menu - Fail";
				break;
			case 14:
				Instance.m_LevelName = "Mission - Success";
				break;

			case 6:
				Instance.m_LevelName = "Menu - Solo";
				break;
			case 4:
				Instance.m_LevelName = "Menu - Multi";
				break;
			case 5:
				Instance.m_LevelName = "Menu - Options";
				break;
            case 24:
                Instance.m_LevelName = "JoinMenu";
                break;
            case 25:
                Instance.m_LevelName = "Multi - Jardin";
                break;
            case 26:
                Instance.m_LevelName = "Selection";
                break;
            case 27:
                Instance.m_LevelName = "timer test";
                break;
            case 28:
                Instance.m_LevelName = "Instable_Client";
                break;
            case 29:
                Instance.m_LevelName = "CatSelect";
                break;
            default:
				Instance.m_LevelName = "Menu - Main";
				break;
		}
		Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
	}
}