using UnityEngine;
using UnityEngine.UI;

public class BallEditor : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private Renderer myRenderer = null;
    [SerializeField] private Slider myRedSlider = null;
    [SerializeField] private Slider myGreenSlider = null;
    [SerializeField] private Slider myBlueSlider = null;
    [SerializeField] private Slider myAlphaSlider = null;

    // PRIVATE VARIABLES
    private float myRendererRed;
    private float myRendererGreen;
    private float myRendererBlue;
    private float myRendererAlpha;

    private void Start()
    {
        myRendererRed = myRenderer.material.GetColor("_EmissionColor").r;
        myRendererGreen = myRenderer.material.GetColor("_EmissionColor").g;
        myRendererBlue = myRenderer.material.GetColor("_EmissionColor").b;
        myRendererAlpha = myRenderer.material.GetColor("_Color").a;

        myRedSlider.value = myRendererRed;
        myGreenSlider.value = myRendererGreen;
        myBlueSlider.value = myRendererBlue;
        myAlphaSlider.value = myRendererAlpha;
    }

    public void ChangeColor()
    {
        myRenderer.material.SetColor("_EmissionColor", new Color(myRedSlider.value, myGreenSlider.value, myBlueSlider.value, 1));
        myRenderer.material.SetColor("_Color", new Color(1, 1, 1, myAlphaSlider.value));
    }

    public void RandomColor()
    {
        var randomRedColor = Random.Range(0f, 1f);
        var randomGreenColor = Random.Range(0f, 1f);
        var randomBlueColor = Random.Range(0f, 1f);
        var randomAlphaColor = Random.Range(0f, 1f);

        myRenderer.material.SetColor("_EmissionColor", new Color(randomRedColor, randomGreenColor, randomBlueColor, 1));
        myRenderer.material.SetColor("_Color", new Color(randomRedColor, randomGreenColor, randomBlueColor, randomAlphaColor));

        myRedSlider.value = randomRedColor;
        myGreenSlider.value = randomGreenColor;
        myBlueSlider.value = randomBlueColor;
        myAlphaSlider.value = randomAlphaColor;
    }
}
