using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallEditor : MonoBehaviour
{
    private enum MaterialType
    {
        DragonScale,
        Fire,
        Lightning,
        Lava,
        Chrome,
        Water
    }

    // INSPECTOR VARIABLES
    [SerializeField] private GameObject myPlayerBall = null;
    [SerializeField] private Renderer myOnScreenBall = null;
    [SerializeField] private Slider myRedSlider = null;
    [SerializeField] private Slider myGreenSlider = null;
    [SerializeField] private Slider myBlueSlider = null;
    [SerializeField] private Slider myAlphaSlider = null;
    [SerializeField] private TMP_Dropdown myDropdown = null;
    [SerializeField] private Material[] myBallMaterials = null;

    // LOCAL VARIABLES
    private Renderer myRenderer;
    private string myVehicleName;
    private float myRendererRed;
    private float myRendererGreen;
    private float myRendererBlue;
    private float myRendererAlpha;
    private Material myRendererMaterial;

    public float GetMaterialRed => myRendererRed;
    public float GetMaterialGreen => myRendererGreen;
    public float GetMaterialBlue => myRendererBlue;
    public float GetMaterialAlpha => myRendererAlpha;
    public Material GetMaterial => myRendererMaterial;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        myRenderer = myPlayerBall.GetComponentInChildren<Renderer>();
        myVehicleName = "";
        myRendererMaterial = myRenderer.sharedMaterial;

        myRendererRed = myOnScreenBall.material.GetColor("_Color").r;
        myRendererGreen = myOnScreenBall.material.GetColor("_Color").g;
        myRendererBlue = myOnScreenBall.material.GetColor("_Color").b;
        myRendererAlpha = myOnScreenBall.material.GetColor("_Color").a;

        myRendererRed = myRenderer.sharedMaterial.GetColor("_Color").r;
        myRendererGreen = myRenderer.sharedMaterial.GetColor("_Color").g;
        myRendererBlue = myRenderer.sharedMaterial.GetColor("_Color").b;
        myRendererAlpha = myRenderer.sharedMaterial.GetColor("_Color").a;

        myRedSlider.value = myRendererRed;
        myGreenSlider.value = myRendererGreen;
        myBlueSlider.value = myRendererBlue;
        myAlphaSlider.value = myRendererAlpha;
    }

    public void ChangeColor()
    {
        myRendererRed = myRedSlider.value;
        myRendererGreen = myGreenSlider.value;
        myRendererBlue = myBlueSlider.value;
        myRendererAlpha = myAlphaSlider.value;

        myRenderer.sharedMaterial.SetColor("_Color", new Color(myRedSlider.value, myGreenSlider.value, myBlueSlider.value, myAlphaSlider.value));
        myOnScreenBall.material.SetColor("_Color", new Color(myRedSlider.value, myGreenSlider.value, myBlueSlider.value, myAlphaSlider.value));
    }

    public void RandomColor()
    {
        myRendererRed = myRedSlider.value;
        myRendererGreen = myGreenSlider.value;
        myRendererBlue = myBlueSlider.value;
        myRendererAlpha = myAlphaSlider.value;

        var randomMaterial = Random.Range(0, myBallMaterials.Length);

        myRendererMaterial = myBallMaterials[randomMaterial];
        myRenderer.sharedMaterial = myBallMaterials[randomMaterial];
        myOnScreenBall.material = myBallMaterials[randomMaterial];
        myDropdown.value = randomMaterial;

        var randomRedColor = Random.Range(0f, 1f);
        var randomGreenColor = Random.Range(0f, 1f);
        var randomBlueColor = Random.Range(0f, 1f);
        var randomAlphaColor = Random.Range(0.8f, 1f);

        myRedSlider.value = randomRedColor;
        myGreenSlider.value = randomGreenColor;
        myBlueSlider.value = randomBlueColor;
        myAlphaSlider.value = randomAlphaColor;

        myRenderer.sharedMaterial.SetColor("_Color", new Color(randomRedColor, randomGreenColor, randomBlueColor, randomAlphaColor));
        myOnScreenBall.material.SetColor("_Color", new Color(randomRedColor, randomGreenColor, randomBlueColor, randomAlphaColor));
    }

    public void ChangeBallMaterial()
    {
        switch (myDropdown.value)
        {
            case (int)MaterialType.DragonScale:
                myRendererMaterial = myBallMaterials[(int)MaterialType.DragonScale];
                myRenderer.sharedMaterial = myBallMaterials[(int)MaterialType.DragonScale];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.DragonScale];
                break;
            case (int)MaterialType.Fire:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Fire];
                myRenderer.sharedMaterial = myBallMaterials[(int)MaterialType.Fire];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Fire];
                break;
            case (int)MaterialType.Lightning:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Lightning];
                myRenderer.sharedMaterial = myBallMaterials[(int)MaterialType.Lightning];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Lightning];
                break;
            case (int)MaterialType.Lava:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Lava];
                myRenderer.sharedMaterial = myBallMaterials[(int)MaterialType.Lava];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Lava];
                break;
            case (int)MaterialType.Chrome:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Chrome];
                myRenderer.sharedMaterial = myBallMaterials[(int)MaterialType.Chrome];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Chrome];
                break;
            case (int)MaterialType.Water:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Water];
                myRenderer.sharedMaterial = myBallMaterials[(int)MaterialType.Water];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Water];
                break;
        }
    }
}
