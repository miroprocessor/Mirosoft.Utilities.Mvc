# Mirosoft.Utilities.Mvc
HtmlHelper methods to extend the power of ASP.Net MVC 

# Installation
- Nuget Package  
  ```Install-Package Mirosoft.Utilities.Mvc```
  
# Methods List
- **CheckBoxList**    
  It renders the input collection as checkbox list   
  ```C#
  @Html.CheckBoxList("Products", Model.ProductsList, "Name", "Id")
  //<Name> is the property name that will be rendered as the text of the checkbox
  //<Id> is the property name that will be rendered as the value of the checkbox
  ```
  
  - **IconActionLink**   
    It renders the action link with icon, it based on Bootstrap CSS classes and fonts.
    ```C#
    @Html.IconActionLink("Link Text","ActionName","ControllerName",new {id = Model.Id},
                        new { @class="btn btn-info"}, "glyphicon glyphicon-pencil")
    //Or
    @Html.IconActionLink("Link Text","RouteName", new {id = Model.Id},
                          new { @class="btn btn-default"}, "glyphicon glyphicon-pencil")
    ```
