function fileUpload(fileName) {
    var inputFile = document.getElementById(fileName);
    var files = inputFile.files;
    var fData = new FormData();
    for (var i = 0; i != files.length; i++) {
        fData.append("files", files[i]);

    }
    $.ajax(
        {
<<<<<<< HEAD
            url:"/Assignments",
=======
            url:"/Assignment",
>>>>>>> Elvis
            data: fData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                alert("Your file is successfully Uploaded");
            }
        }

    );
}