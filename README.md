<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/231085175/19.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T849111)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Angular File Uploader - How to upload images using ASP.NET Core API Controller

This example contains an Angular client-side application with DevExtreme File Uploader. The control uploads image files to the ASP.NET Core API Controller and returns the uploaded images as the base64 strings to the client. 

*Files to look at*:

* [home.component.html](./CS/FileUploaderAngular/ClientApp/src/app/home/home.component.html)
* [home.component.ts](./CS/FileUploaderAngular/ClientApp/src/app/home/home.component.ts)
* [app.module.ts](./CS/FileUploaderAngular/ClientApp/src/app/app.module.ts)
* [FileUploadController.cs](./CS/FileUploaderAngular/Controllers/FileUploadController.cs)



## Implementation:

1) Configure your Angular application as described in the [Add DevExtreme to an Angular CLI Application](https://js.devexpress.com/Documentation/Guide/Angular_Components/Getting_Started/Add_DevExtreme_to_an_Angular_CLI_Application/) article. Register the DevExtreme FileUploader module in the *app.module.ts* file.
2) Add the FileUploader component to one of your Angular components files. Specify its **name** option and set the **uploadUrl** option so that it points to your ASP.NET Core API Controller.
```html
<dx-file-uploader name='myFile' uploadUrl="/FileUpload" [multiple]="true"
  accept="*"  uploadMode="instantly">
</dx-file-uploader>
```
3) Create an API Controller in your ASP.NET Core application and a method to save an uploaded file. Note that the method's parameter name should be equal to your FileUploader's **name**.

```cs
 public async Task<IActionResult> AsyncUpload(IFormFile myFile) {
            string targetLocation = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            try {
                if (!Directory.Exists(targetLocation))
                    Directory.CreateDirectory(targetLocation);
                using (var fileStream = System.IO.File.Create(Path.Combine(targetLocation, myFile.FileName))) {
                    myFile.CopyTo(fileStream);
                }
            } catch {
                Response.StatusCode = 400;
            }
            ...
 }
```
4) (*Optional*) If you need to display an uploaded image file as a base64 string, convert it to a byte array in your Controller.Then get the base64 string from this array with **Convert.ToBase64String** and return the result from your method. 
5) (*Optional*) To display the image after uploading, handle the [onUploaded](https://js.devexpress.com/Documentation/ApiReference/UI_Widgets/dxFileUploader/Events/#uploaded) event (see [Angular - Component Configuration Syntax - Event Handling](https://js.devexpress.com/Documentation/Guide/Angular_Components/Component_Configuration_Syntax/#Event_Handling)) and append the "IMG" tag in this event handler.
```ts
export class HomeComponent {
    onUploaded(e) {
        var image = document.createElement("IMG");
        image.setAttribute("src", "data:image/jpg;base64," + e.request.responseText);
        var container = document.getElementById("imagesContainer");
        container.appendChild(image);
    }
}
```


