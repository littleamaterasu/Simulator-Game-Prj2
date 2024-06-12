using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageSwitchScene : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    [SerializeField]
    Button switchScene;
    [SerializeField]
    Button cancel;
    [SerializeField]
    Image background;
    [SerializeField]
    Texture2D arrow;

    private GameObject arrowObject;
    private RectTransform arrowRectTransform;
    private Image arrowImage;
    private bool isHovering = false;
    private GameObject currentHoverObject = null;

    void Start()
    {
        // Initially hide UI elements
        switchScene.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
        background.gameObject.SetActive(false);

        // Add listeners for buttons
        switchScene.onClick.AddListener(OnSwitchSceneClicked);
        cancel.onClick.AddListener(OnCancelClicked);

        // Create an arrow object and initially hide it
        arrowObject = new GameObject("Arrow");
        arrowObject.transform.SetParent(GameObject.Find("Canvas").transform); // Assuming there's a Canvas in the scene

        arrowRectTransform = arrowObject.AddComponent<RectTransform>();
        arrowImage = arrowObject.AddComponent<Image>();
        arrowImage.sprite = Sprite.Create(arrow, new Rect(0, 0, arrow.width, arrow.height), new Vector2(0.5f, 0.5f));

        // Scale the arrow to 1/5 of its original size
        arrowRectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        arrowObject.SetActive(false);
    }

    void OnSwitchSceneClicked()
    {
        PlayerPrefs.SetString("nextScene", sceneName);
        SceneManager.LoadScene("Loading");
    }

    void OnCancelClicked()
    {
        // Hide UI elements when cancel button is clicked
        switchScene.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
    }

    void Update()
    {
        // Perform a raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the raycast hit an object with the SwitchScene component
            SwitchScene switchSceneComponent = hit.transform.GetComponent<SwitchScene>();
            if (switchSceneComponent != null)
            {
                // Show arrow when mouse is over the object
                if (!isHovering || currentHoverObject != hit.transform.gameObject)
                {
                    isHovering = true;
                    currentHoverObject = hit.transform.gameObject;
                    arrowObject.SetActive(true);
                    PositionArrow();
                }

                // Check for mouse click to show UI elements and set sceneName
                if (Input.GetMouseButtonDown(0))
                {
                    sceneName = switchSceneComponent.sceneName;
                    
                    switchScene.gameObject.SetActive(true);
                    cancel.gameObject.SetActive(true);
                    background.gameObject.SetActive(true);
                }
            }
            else
            {
                // Hide arrow if the mouse is not over the object with SwitchScene component
                if (isHovering)
                {
                    isHovering = false;
                    currentHoverObject = null;
                    arrowObject.SetActive(false);
                }
            }
        }
        else
        {
            // Hide arrow if the mouse is not over any object
            if (isHovering)
            {
                isHovering = false;
                currentHoverObject = null;
                arrowObject.SetActive(false);
            }
        }

        // Ensure arrow position is updated if active
        if (arrowObject.activeSelf)
        {
            PositionArrow();
        }
    }

    private void PositionArrow()
    {
        // Convert world position to screen position
        Vector3 screenPos = Camera.main.WorldToScreenPoint(currentHoverObject.transform.position);
        arrowRectTransform.position = new Vector3(screenPos.x, screenPos.y + 50, screenPos.z);
    }
}
