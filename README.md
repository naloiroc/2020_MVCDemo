# 2020_MVCDemo

- 2020/03/26
  - 新增資料驗證與HtmlHelper操作方式：VailationController
  - 新增 Session 操作方式：SessionController
  - 新增 List 操作方式：ListController
  - 新增作業一程式碼：Homework1Controller

### 甚麼是MVC
- Model：負責處理資料，包含定義資料的結構與資料庫的存取。
- View：負責顯示內容，將Model產生的結果顯示於畫面上。
- Controller：負責處理請求，呼叫Model並將結果回傳View

### 特色
- 關注點分離
- 分層架構
- 易於擴展
- 習慣代替配置

### 資料夾結構
- App_Data
  - 本地資料儲存位置(XML, DB)，外部無法讀取
- App_Start
  - MVC專案功能設定
- Content
  - 放置靜態檔案(static file)，如css, image檔案
- Controllers
	- Controller程式碼
- Models(Repository)
	- 定義資料結構，資料庫存取
- Scripts
	- 放置Javascript檔案
- Views
	- 畫面顯示程式碼，與Controller名稱相同
- Global.asax
	- 執行應用程式時載入各項設定(App_Start)
- Web.config
	- 整個專案的參數檔
  
### ActionResult
- ViewResult
	- return View()		回傳 html 頁面
- PartialViewResult
	- return PartialView()	回傳部分 html 頁面
- JsonResult
	- return Json()		回傳 Json字串
- FileResult
	- return File()		回傳檔案(下載檔案)
- RedirectResult
	- return RedirectToAction()	回傳其他 Action

### 資料傳遞
- ViewData
	- 繼承IDictionary，以key-value的方式設定，以object方式儲存
- ViewBag
	- 動態型別(dynamic)，若有錯誤執行時才會知道
- TempData
	- 繼承IDictionary，跨Action傳遞，被讀取後即刪除
- ViewModel
	- View使用的Model，強型別
- Session 
	- 資料儲存於伺服器端，預設存活20分鐘

### Razor
```razor
@{
    // 區塊
    string Name = "Yuzhen";
    <p>@Name 也可以在裡面寫HTML</p>
}

@*變數*@
<p>@Name</p>

@*陳述式*@
<p>@(Name)</p>
<p>@(1 + 1)</p>

@if(Name == "")
{
  <p>Name is empty</p>
}
else
{
  <p>Hello @Name</p>
}

@{ 
    // foreach
    List<int> list = new List<int>() { 1, 2, 3 };
}
<ul>
    @foreach (int i in list)
    { 
        <li>@i</li>
    }
</ul>
```

### 資料驗證
Model
```c#
    using System.ComponentModel.DataAnnotations;
    public class Student
        {
            [Display(Name = "姓名")]
            [Required(ErrorMessage = "請輸入姓名")]
            public string Name { get; set; }

            [Display(Name = "年齡")]
            [Required(ErrorMessage = "請輸入年齡")]
            [Range(20, 100, ErrorMessage = "年齡區間為20 ~ 100")]
            public int? Age { get; set; }
        }
```
### HtmlHelper
View 一般寫法

```razor
    <form action="@Url.Action("Index")" method="post">
        <label for="Name">姓名</label>
        <input type="text" value="@Model.Name"/>
        <br />
        <label for="Name">年齡</label>
        <input type="text" value="@Model.Age" />
        <br />
        <button>送出</button>
    </form>
```
View HtmlHelper寫法
```razor
@using (Html.BeginForm("Index", "Validation", FormMethod.Post))
{ 
    @*顯示Display的名稱(姓名)*@
    @Html.LabelFor(m => m.Name)
    @Html.TextBoxFor(m => m.Name)
    @Html.ValidationMessageFor(m => m.Name)
    <br />
    @Html.LabelFor(m => m.Age)
    @Html.TextBoxFor(m => m.Age)
    @Html.ValidationMessageFor(m => m.Age)
    <br />
    <button>送出</button>
}

```
### Http Method
- GET
	- 取得資料，資訊寫在http-header上(query string)
  - 取得菜單
- POST
	- 新增資料，資訊寫在message-body上
  - 點餐
- PUT
	- 新增資料，若存在則覆蓋
  - 重新點餐
- PATHCH
	- 附加新的資料在已經存在的資料後面
  - 加點項目
- DELETE
	- 刪除資料
  - 取消點餐

### 集合式型別
- array：特定型別，固定長度，宣告時指定長度
  ```c#
	int[] intArray = new int[] { 1, 2, 3 };
  ```
- List：不固定型別(泛型)，不固定長度
  ```c#
	List<string> list = new List();
	list.Add(string);
  ```
List新增
```c#
            // 新增單筆
            List<Student> listStudent = new List<Student>();
            Student student = new Student
            {
                Name = "A",
                Age = 65,
            };
            listStudent.Add(student);
            listStudent.Add(new Student
            {
                Name = "B",
                Age = 66,
            });

            // 新增多筆
            List<Student> newListStudent = new List<Student>();
            newListStudent.AddRange(listStudent);
```
List刪除
```c#
            listStudent.RemoveAt(0);

            // 刪除 B
            Student student = null;
            foreach (Student s in listStudent)
            {
                if (s.Name == "B")
                {
                    student = s;
                }
            }
            if (student != null)
            { 
                listStudent.Remove(student);            
            }
```
List修改
```c#
            // 修改第0筆資料
            Student tmpStudent = listStudent[0];
            tmpStudent.Name = "X";

            // 修改 B
            foreach (Student s in listStudent)
            {
                if (s.Name == "B")
                {
                    s.Name = "Y";
                    break;

                }
            }

```

