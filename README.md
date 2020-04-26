# Unity中的UI交互

（1）  为了脚本的资源管理有效性和可复用性，本人将所有的可变节点设置为公共接口，在下次遇到使用类似的GUI功能的时候只需要在对应的接口中放入指定的界面对象进行初始化即可实现对指定接口的调用。

```c#
public GameObject cubeContainer = null;

  public GameObject cube = null;

  public GameObject imageHead = null;

  public Dropdown dropdown = null;

  public Toggle buttonActivatorToggle = null;

  public Toggle buttonOpenerToggle = null;

  public GameObject magnifyButton = null;

  public GameObject minifyButton = null; 
  
  public float scaleDelta = 0;//在这里可以通过外部输入，当用户没有在外部输入具体的缩放变化值的时候默认缩放变量为0.01。
```





（2）  在项目初期，本人尝试着结合本人正在执行的虚拟现实课设相关VR消防疏散演练项目中的内容，加入了火焰着色器、毛皮着色器和条纹着色器并在附录中予以呈现。

（3）  在改变物体颜色时候，只需要获取指定游戏对象的MeshRenderer组件并获取该组件下的material属性下的颜色属性，即可完成对该游戏对象进行颜色设定。具体实现方式为：

```c#
cube.GetComponent<MeshRenderer>().material.color = Color.gray。
```



（4）  在设置游戏对象高度的时候遇到了比较麻烦的问题——不管我怎么安置精灵头像图片和方体，在方体单向拉伸的时候总是无法实现精灵头像图片的随着方体升高而上升，于是我采用了同时控制精灵图片Y轴位移和方体单向拉伸量的方式，实现了当物体拉伸时精灵头像图片也同时向上攀升：

```C#
public void ChangeHeight(Slider slider)

  {

​    // Debug.Log(slider.value);

​    cubeContainer.transform.localScale =

​     new Vector3(cubeContainer.transform.localScale.x,

​         slider.value * 5, cubeContainer.transform.localScale.z);

 

​    //设置头像跟随底下盒子的拉伸而升高

​    imageHead.transform.position =

​    new Vector3(imageHead.transform.position.x,

​    slider.value * 5, imageHead.transform.position.z);

  }
```



（5）  设置旋转角度则相对比较简单：CubeImageContainer.transform.eulerAngles = new Vector3(0, scrollbar.value * 180, 0)即可实现。

（6）  设置缩放按钮及其有效性是通过全局开关进行，当全局开关isButtonUsabl处在开启状态时按钮点击之后才会相应，具体实现过程如下：

```c#
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
```

 

设置缩小按钮也是换汤不换药。

 

（7）  通过把目标对象挂靠在一个空的GameObject中，再把目标对象的一角对应在空对象的中心点，就可把锚点设置在对应的那个目标对象的角，进行拉伸的时候，就可以完成单侧拉伸；

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image002.jpg)

 

   但是这种方式有一个很大的坑——需要在该面板中修改父物体的Scale，直接在父物体上使用快捷键R后调整的Scale的中心锚点还会停留在原来子物体的中心上，导致无法实现单向拉伸的效果。

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image004.jpg)

 

  当然，还有更加方便的方式，通过下方的函数可以将这一过程自动化，但是本质原理与课上所述一样，也是在目标对象上面添加一个空物体作为该物体的父物体，然后进行轴的移动再拉伸:

```C#
public static void ResetCenterPosition()

  {

​    //选中的对象

​    Transform transform = Selection.activeTransform;

​    Bounds bounds = transform.GetComponent<Collider>().bounds;

 

​    GameObject newObj = new GameObject();

​    newObj.name = transform.name;

​    // bounds.min = bounds.center - bounds.extents

​    // extents：边界框的范围。这总是Bounds大小的一半。

​    // center ：边界框的中心。  

​    newObj.transform.position = bounds.min;

​    transform.SetParent(newObj.transform);

}
```



在实验中我直接将本函数作为扩展工具添加到了Unity编辑器面板中。





（8）  最终的Hirarchy面板下各个游戏对象的布置情况如下图：

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image006.jpg)

 

 

 

**5.** **运行结果**

【运行程序并记录截图于下方。】

（1）   初期正方体使用毛皮着色器、图片采用火焰着色器以及平面采用条纹着色器的效果：

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image008.jpg)

<p align="center">【正视图的效果】</p>



 

（2）   按照作业要求完成的最终效果（去除着色器后的作业效果）：

 

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image010.jpg)

<p align="center">【正视图效果】</p>

 

 

 

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image012.jpg)

<p align="center">【设置旋转的效果】</p>

 

 

 

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image014.jpg)

<p align="center">【设置单向伸长盒子并升高精灵的效果】</p>

 

 

 

![img](/C:\Users\艾孜尔江\AppData\Local\Temp\msohtmlclip1\01\clip_image016.jpg)

<p align="center">【设置单向缩短盒子并降低精灵的效果】</p>

 

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image018.jpg)

![img](/C:\Users\艾孜尔江\AppData\Local\Temp\msohtmlclip1\01\clip_image020.jpg)

<p align="center">【设置盒子颜色改变下拉选框并改变颜色的效果】</p>

 

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image022.jpg)

<p align="center">【设置点击按钮开关勾选框后按钮失效】</p>

 

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image024.jpg)

<p align="center">【设置点击放大按钮后仅放大头像精灵的效果】</p>



![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image026.jpg)

<p align="center">【设置点击缩小精灵头像后缩小的效果】</p>

注意：我函数里设置了头像缩小到最小值后不再进行缩小，以便有个异常处理机制。

 

![img](https://github.com/Ezharjan/Unity_GUI/blob/master/readme_imgs/clip_image028.jpg)

<p align="center">【点击激活开关按钮使开关消失的效果】</p>

 

 

​     

## 结语

  本次实验看似简单，实则在撰写控制脚本的时候发现有些地方需要共同控制的话单从UI上考虑是无法解决问题的，尤其是单向拉伸这一功能的实现，考察了三维空间坐标系下物体轴心的变化。尽管只是都比较容易而且早已印刻于脑海中，但是在实践的时候发现当空物体被作为父物体加上去之后直接在场景面板里面缩放父物体是无法看到单向拉伸效果，只能从Inspector面板中调节Scale值才行，这或许是Unity官方在编辑器中遗留的一个bug，或者说它们这么做是为了避免用户误操作而采取了这样的方式。在实现头像随着拉伸而上升功能时，目前我只想到了通过脚本解决的方案。

  纸上得来终觉浅，绝知此事要躬行。通过本次实验，我进一步系统地学习和排除了之前对于Unity GUI所遗留的一些知识盲区，也趁此机会整理出日后可能会用到的GUI控制器工具脚本，以便后事之需。    









<p align="right">2020年4月26日</p>

 <p align="right">Alexander Ezharjan</p>

   

 

 

 

 