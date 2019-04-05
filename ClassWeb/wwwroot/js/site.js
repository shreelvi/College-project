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
$(document).ready(function () {
    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
});