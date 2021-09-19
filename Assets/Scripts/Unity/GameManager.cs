using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RawImage imageGrid;
    [SerializeField] private InputField inpWidth;
    [SerializeField] private InputField inpHeight;
    [SerializeField] private InputField inpThreshold;
    [SerializeField] private InputField inpRange;
    [SerializeField] private InputField inpStates;
    [SerializeField] private Toggle tog;
    [SerializeField] private Toggle tor;
    private CCA cca;
    private int[,] grid;
    private Texture2D texture;
    private int width;
    private int height;
    private int threshold;
    private int range;
    private int states;
    private Color c;
    private Color[] stateColors;
    private Color green;
    private Color yellow;
    private int q;
    //private UnityEngine.Random rdn;
    
    void Start()
    {
        ColorUtility.TryParseHtmlString("#886C5F", out c);
        //rdn= new UnityEngine.Random();
    }

    // Update is called once per frame
    void Update()
    {        
        if(texture != null)
        { 
            NextGen();
        }
    }

    public void OnPopulateClick()
    {
        if(inpWidth.text != "" && inpHeight.text != "")
        {
            width = Convert.ToInt32(inpWidth.text);
            height = Convert.ToInt32(inpHeight.text);
            threshold = Convert.ToInt32(inpThreshold.text);
            range = Convert.ToInt32(inpRange.text);

            states = Convert.ToInt32(inpStates.text);
            stateColors = new Color[states + 1];
            GenerateColors(states);

            /*for(int i = 0; i < states + 1; i++) 
            {
                stateColors[i] = rgbMixRandom(new Color32((byte)255, (byte)255, (byte)255, 1), -0.1f);
            }*/

            grid = new int[width, height];
            cca = new CCA(width, height, states, threshold, range, tog.isOn, tor.isOn);

            grid = cca.Populate(grid);

            imageGrid.rectTransform.sizeDelta = new Vector2(width, height);
            texture = new Texture2D(width, height);

            UpdateImage();
        }
    }

    private void NextGen()
    {
        cca.Next(grid);
        
        UpdateImage();
    }

    private void UpdateImage()
    {    
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                texture.SetPixel(x, y, stateColors[grid[x, y]]);
            }
        }

        texture.Apply();
        imageGrid.texture = texture;
    }

    private void GenerateColors(int numColors)
    {
        for(int i = 0; i < numColors; i++)
            stateColors[i] = UnityEngine.Random.ColorHSV(.4f, .4f, 0f, 1f, 0.5f, 1f);
    }
}
