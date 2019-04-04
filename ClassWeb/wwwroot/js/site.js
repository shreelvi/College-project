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
$("#FileUpload").change(function () {
    var str = "";
    $("select option:selected").each(function () {
        if ($(this).text() === "Folder Upload") {
            $("#file").attr("webkitdirectory", true);
            $("#file").attr("mozdirectory", true);
        }
        if ($(this).text() === "Multiple File Upload") {
            $("#file").removeAttr("webkitdirectory");
            $("#file").removeAttr("mozdirectory");
            $("#file").attr("multiple",true);
        }
        if ($(this).text() === "File Upload") {
            $("#file").removeAttr("webkitdirectory");
            $("#file").removeAttr("mozdirectory");
            $("#file").removeAttr("multiple");
        }
    });
});
$(CreateFolder).on('shown.bs.modal', function (e) {
    $('[autofocus]', e.target).focus();

});
$("#UserSearch").on("KeyUp", function (e) {
    var text = $("#UserSearch").val;
    window.location.replace("/Users/index?SearchString="+text);
});