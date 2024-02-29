using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image bgImg;
    [SerializeField] private AnimationCurve fadeCurve;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void FadeTo(string sceneToLoad)
    {
        StartCoroutine(FadeOut(sceneToLoad));
    }
    IEnumerator FadeIn()
    {
        float t = 1f;
        while(t > 0f)
        {
            t -= Time.deltaTime;
            float currAlpha = fadeCurve.Evaluate(t);
            bgImg.color = new Color(0, 0, 0, currAlpha);
            yield return 0; //skip to next frame at this line
        }
    }
    IEnumerator FadeOut(string sceneToLoad)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            float currAlpha = fadeCurve.Evaluate(t);
            bgImg.color = new Color(0, 0, 0, currAlpha);
            yield return 0; //skip to next frame at this line
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
