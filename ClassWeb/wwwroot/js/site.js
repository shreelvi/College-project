// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#FileName").on('keypress', function (e) {
    if (e.which === 13) {
        var text = $("#FileName").text();
        var tabledata = $("td");
        alert(tabledata.children().text());
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