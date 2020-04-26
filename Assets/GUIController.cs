using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{

    public GameObject CubeImageContainer = null;
    public GameObject cubeContainer = null;
    public GameObject cube = null;
    public GameObject imageHead = null;
    public Dropdown dropdown = null;
    public Toggle buttonActivatorToggle = null;
    public Toggle buttonOpenerToggle = null;
    public GameObject magnifyButton = null;
    public GameObject minifyButton = null;
    public float scaleDelta = 0;
    private bool isButtonUsable = true;


    public void ChangeColor()
    {
        Debug.Log("Change Color.");
        switch (dropdown.value)
        {
            case 0:
                cube.GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
            case 1:
                cube.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case 2:
                cube.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 3:
                cube.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            default:
                break;
        }
    }


    public void ChangeHeight(Slider slider)
    {
        // Debug.Log(slider.value);
        cubeContainer.transform.localScale =
         new Vector3(cubeContainer.transform.localScale.x,
                 slider.value * 5, cubeContainer.transform.localScale.z);

        //设置头像跟随底下盒子的拉伸而升高
        imageHead.transform.position =
        new Vector3(imageHead.transform.position.x,
        slider.value * 5, imageHead.transform.position.z);
    }


    public void SetRotation(Scrollbar scrollbar)
    {
        Debug.Log(scrollbar.value);
        CubeImageContainer.transform.eulerAngles = new Vector3(0, scrollbar.value * 180, 0);
    }

    public void ActiveButtons()
    {
        if (buttonActivatorToggle.isOn)
        {
            magnifyButton.SetActive(true);
            minifyButton.SetActive(true);
        }
        else
        {
            magnifyButton.SetActive(false);
            minifyButton.SetActive(false);
        }
    }

    public void OpenButtons()
    {
        Debug.Log(buttonOpenerToggle.isOn);
        isButtonUsable = buttonOpenerToggle.isOn;
    }

    public void ButtonMagnify()
    {
        if (isButtonUsable)
        {
            if (!scaleDelta.Equals(0))
            {
                imageHead.transform.localScale =
                new Vector3(imageHead.transform.localScale.x + scaleDelta,
                imageHead.transform.localScale.y + scaleDelta,
                imageHead.transform.localScale.z + scaleDelta);
            }
            else
            {
                scaleDelta = 0.01f;
            }
        }
        else
        {
            Debug.Log("Button is not usable!");
        }
    }

    public void ButtonMinify()
    {
        if (isButtonUsable)
        {
            if (!scaleDelta.Equals(0) && (imageHead.transform.localScale.x > scaleDelta))
            {
                imageHead.transform.localScale =
                new Vector3(imageHead.transform.localScale.x - scaleDelta,
                imageHead.transform.localScale.y - scaleDelta,
                imageHead.transform.localScale.z - scaleDelta);
            }
            else
            {
                scaleDelta = 0.01f;
            }
        }
        else
        {
            Debug.Log("Button is not usable!");
        }
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }


    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
	Application.Quit();
#endif
    }


#if UNITY_EDITOR
    //设置单向拉伸的巧妙方式（本质原理与添加父物体后拉伸父物体一致）
    [MenuItem("Tools/ResetCenterPosition")]
    public static void ResetCenterPosition()
    {
        //选中的对象
        Transform transform = Selection.activeTransform;
        Bounds bounds = transform.GetComponent<Collider>().bounds;

        GameObject newObj = new GameObject();
        newObj.name = transform.name;
        //  bounds.min = bounds.center - bounds.extents
        //  extents：边界框的范围。这总是Bounds大小的一半。
        //  center ：边界框的中心。    
        newObj.transform.position = bounds.min;
        transform.SetParent(newObj.transform);
    }
#endif
}
