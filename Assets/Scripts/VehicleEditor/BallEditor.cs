using TMPro;
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
    [SerializeField] private TMP_Dropdown myDropdown = null;
    [SerializeField] private Material[] myBallMaterials = null;

    // PRIVATE VARIABLES
    private float myRendererRed;
    private float myRendererGreen;
    private float myRendererBlue;
    private float myRendererAlpha;

    private void Start()
    {
        myRendererRed = myRenderer.material.GetColor("_Color").r;
        myRendererGreen = myRenderer.material.GetColor("_Color").g;
        myRendererBlue = myRenderer.material.GetColor("_Color").b;
        myRendererAlpha = myRenderer.material.GetColor("_Color").a;

        myRedSlider.value = myRendererRed;
        myGreenSlider.value = myRendererGreen;
        myBlueSlider.value = myRendererBlue;
        myAlphaSlider.value = myRendererAlpha;
    }

    public void ChangeColor()
    {
        myRenderer.material.SetColor("_Color", new Color(myRedSlider.value, myGreenSlider.value, myBlueSlider.value, myAlphaSlider.value));
    }

    public void RandomColor()
    {
        var randomMaterial = Random.Range(0, myBallMaterials.Length);

        myRenderer.material = myBallMaterials[randomMaterial];
        myDropdown.value = randomMaterial;

        var randomRedColor = Random.Range(0f, 1f);
        var randomGreenColor = Random.Range(0f, 1f);
        var randomBlueColor = Random.Range(0f, 1f);
        var randomAlphaColor = Random.Range(0.8f, 1f);

        myRedSlider.value = randomRedColor;
        myGreenSlider.value = randomGreenColor;
        myBlueSlider.value = randomBlueColor;
        myAlphaSlider.value = randomAlphaColor;

        myRenderer.material.SetColor("_Color", new Color(randomRedColor, randomGreenColor, randomBlueColor, randomAlphaColor));
    }

    public void ChangeBallMaterial()
    {
        if (myDropdown.value == 0)
        {
            myRenderer.material = myBallMaterials[0];
        }
        else if (myDropdown.value == 1)
        {
            myRenderer.material = myBallMaterials[1];
        }
        else if (myDropdown.value == 2)
        {
            myRenderer.material = myBallMaterials[2];
        }
        else if (myDropdown.value == 3)
        {
            myRenderer.material = myBallMaterials[3];
        }
        else if (myDropdown.value == 4)
        {
            myRenderer.material = myBallMaterials[4];
        }
        else if (myDropdown.value == 5)
        {
            myRenderer.material = myBallMaterials[5];
        }
    }
}
