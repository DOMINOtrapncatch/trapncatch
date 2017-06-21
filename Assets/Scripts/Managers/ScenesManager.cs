using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
	static ScenesManager m_Instance;
    static ScenesManager Instance {
        get
        {
            if(m_Instance == null)
            {
                GameObject scenesManager = new GameObject("ScenesManager");

				Canvas canvas = scenesManager.AddComponent<Canvas>();
				canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvas.sortingOrder = 1;

                scenesManager.AddComponent<CanvasScaler>();
                scenesManager.AddComponent<GraphicRaycaster>();

                m_Instance = scenesManager.AddComponent<ScenesManager>();
			}

            return m_Instance;
        }
    }

	Image m_fadeImage;
    Image fadeImage {
        get
        {
            if(m_fadeImage == null)
			{
				GameObject fadeImage = new GameObject("FadeImage");

				// Set parent
				fadeImage.transform.SetParent(m_Instance.transform);

				RectTransform rectTransform = fadeImage.AddComponent<RectTransform>();
				rectTransform.anchorMin = new Vector2(0, 0);
				rectTransform.anchorMax = new Vector2(1, 1);
				rectTransform.offsetMin = new Vector2(0, 0);
				rectTransform.offsetMax = new Vector2(0, 0);

				m_fadeImage = fadeImage.AddComponent<Image>();
                m_fadeImage.color = new Color(0, 0, 0, 0);
            }

            return m_fadeImage;
        }
    } 

	Color fadeColorAlpha;
    Color fadeColor;
    static Coroutine fadeEffect;
    public static bool isInTransition { get { return fadeEffect != null; } }
    float transition;
    bool isShowing;
	float duration;

	static string lastScene;
	static string currentScene = "Start";

	void Awake()
	{
		DontDestroyOnLoad(this);
		m_Instance = this;
        fadeImage.enabled = false;
	}

    static void ChangeScene(string sceneName)
    {
        lastScene = currentScene;
        currentScene = sceneName;
	}

	public static void LoadFirst(string sceneName, float fadeIn, Color color)
	{
		Instance.StartFadeScene(sceneName, 0, fadeIn, color);
	}

	public static void LoadLast(float fadeOut, float fadeIn, Color color)
	{
		Instance.StartFadeScene(lastScene, fadeOut, fadeIn, color);
	}

	public static void LoadSplash(string sceneName, float fadeOut, float fadeIn, Color color)
	{
		Instance.StartFadeSplash(sceneName, fadeOut, .8f, fadeIn, color);
	}

	public static void LoadScene(string sceneName, float fadeOut, float fadeIn, Color color)
	{
        Instance.StartFadeScene(sceneName, fadeOut, fadeIn, color);
	}

	void StartFadeScene(string sceneName, float fadeOut, float fadeIn, Color color)
	{
		ChangeScene(sceneName);
		fadeEffect = StartCoroutine(FadeScene(sceneName, fadeOut, fadeIn, color));
	}

	IEnumerator FadeScene(string sceneName, float fadeOut, float fadeIn, Color color)
	{
		// Store color
		fadeImage.enabled = true;
		fadeColorAlpha = new Color(color.r, color.g, color.b, 0);
		fadeColor = color;

		// Fade Out
		isShowing = true;
		duration = fadeOut;
		transition = 0;

		// Change Scene when animation ended
		yield return new WaitUntil(() => transition >= 1);

		// Wait for the user to see the image
        SceneManager.LoadScene(sceneName);

		// Fade In
		isShowing = false;
		duration = fadeIn;
		transition = 1;

		// Wait for the end of the animation
		yield return new WaitUntil(() => transition <= 0);
        fadeImage.enabled = false;
        fadeEffect = null;
	}

	void StartFadeSplash(string sceneName, float watchTime, float fadeOut, float fadeIn, Color color)
	{
		ChangeScene(sceneName);
        fadeEffect = StartCoroutine(FadeSplash(sceneName, watchTime, fadeOut, fadeIn, color));
	}

	IEnumerator FadeSplash(string sceneName, float watchTime, float fadeOut, float fadeIn, Color color)
	{
        // Wait the amount of time we want to see the image (taking in count scene fadeIn duration)
        yield return new WaitForSeconds(duration + watchTime);

        StartFadeScene(sceneName, fadeOut, fadeIn, color);
	}

	void Update()
    {
        if (!isInTransition) return;

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(fadeColorAlpha, fadeColor, transition);
    }

    public static void ForceLoad(string sceneName)
    {
        Instance.StartForceLoad(sceneName);
	}

	void StartForceLoad(string sceneName)
	{
        if(fadeEffect != null)
            StopCoroutine(fadeEffect);
        
        SceneManager.LoadScene(sceneName);
	}
}