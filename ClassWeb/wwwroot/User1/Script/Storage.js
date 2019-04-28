var Peoples = [];
$(document).ready(function () {
    $("#AddPerson").click(function () {
        //var group = $("#Group").prop('checked', function (i,val) {
        //    return val;

        //});
    localStorage.setItem("People", JSON.stringify(Peoples));
        var info = {
            Name: $("#Name").val().trim(),
            Phone: $("#Phone").val().trim(),
            Age: $("#Age").val().trim(),
            Group: 1,
        }
     // Peoples.push(info);
        Peoples.concat(info);
        localStorage.People = JSON.stringify(Peoples);

    });
    if (localStorage.userName != undefined || localStorage.userName != null) {
        $("#UserName").val(localStorage.userName);
    }
    $("#btn_Login").click(function () {
        var userName = $("#UserName").val().trim();
        var password = $("#Password").val().trim();

        if (userName === "tester" && password === "tested") {
            //  https://stackoverflow.com/questions/901712/how-to-check-whether-a-checkbox-is-checked-in-jquery
            if ($("#Remember").prop('checked')) {
                // alert("checked");
                localStorage.setItem("userName", userName);
                // localStorage.setItem("password", password);
            } else {
                if (localStorage.userName != undefined || localStorage.userName != null) {

                    localStorage.removeItem("userName");
                }
                //do nothing
                //  localStorage.userName.clear();

                // alert("unchecked");
            }
            alert("You are logged in");
        }
        else {
            alert("Login Failed");
        }
        //https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_loc_reload
        location.reload(true);

    });
    $("#btn_Forget").click(function () {
        if (localStorage.userName != undefined || localStorage.userName != null) {
            localStorage.removeItem("userName");
        }
        //alert(localStorage.userName);
        // localStorage.userName = undefined;
        //localStorage.removeItem("userName");
        // alert(localStorage.userName);
    });
});