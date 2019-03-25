// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
$("#EditedText").on('keypress', function (e) {
    if (e.which === 13) {
        var text = editor_js.getValue();
        alert(text);
    }
});
$("td").one("click mouseenter", function () {
    var tabledata = $("td");
    tabledata.foreach(function (tabledata) {
        $("FileName").removeAttr('hidden');
    });
  
    var link = "<span class='glyphicon glyphicon-edit'> @Html.ActionLink('Save', 'Save', new { FileName = Model[i].Name })</span>";
 //   $("#FileName").append(link);
});
$("#SaveFile").click(function () {
});



$(document).ready(function () {
    $("#SaveFile").click(function () {
        var div = $(".Fileaction");
        var obj = {};
        obj.FileName = $("#FileName").text();
        obj.Data = $("#EditedText").text();
        alert(obj.Data);
        if ($("#EditedText").val.trim === null || $("#EditedText").val.trim === "") {
            alert("Empty File Cannot be saved");
        } else {
            $.ajax({
                url: "SaveEditedFile"
                , type: "POST"
                , dataType: "json"
                , data: "Content=" + JSON.stringify(obj)
                , sucesss: function () {
                    var span = $("<span class='bg-sucess'></span>").text("File Succesfully Uploaded");
                    div.append(span);
                }
                , error: function () {
                    alert("There is an error");
                }
            });
        } 
    });

});
*/