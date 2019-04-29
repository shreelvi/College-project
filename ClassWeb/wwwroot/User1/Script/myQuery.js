/*  Kishor Simkhada
    Mar 27 2018
    Info-2202
    Jon Holmes
*/
$(document).ready(function () {
    $("#btnChangeSpan").click(function () {
        $("#spnOne").text("New Text for the Span");
    });
    $("#btnAppendToSpan").click(function () {
        //var text = "==Text at the back";
        $("#spnOne").append(" ==Text at the back");
        //   alert($("#spanOne").append(text));
    });
    $("#btnPrependToSpan").click(function () {
        $("#spnOne").prepend("Text at the front -- ")
    });
    $("#btnBeforeSpan").click(function () {
        $("#spnOne").before("Text Before ++ ")
    });
    $("#btnAfterSpan").click(function () {
        $("#spnOne").after("@@ Text After");
    });
    $("#btnShowSpan").click(function () {
        alert($("#spnOne").text());
    });
    /*  https://stackoverflow.com/questions/8701650/jquery-loop-through-each-div?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa */
    $("#btnNumberPs").click(function () {
        var i = 1;
        //for (var i = 1; i < $("#btnNumberPs").length; i++) {
        //    $("#divNumbers p").prepend(i+". ");
        //}

        // $("#divNumbers p").each(function () {
        //var par = $(this).find("p");
        //    /* this is creats some awesome bug
        //    var strValue = i + ". " + $("p").text();
        //        $("#divNumbers p").text(strValue);
        //    */
        var strValue = i + ". ";
        //$("#divNumbers p").text(strValue);
        $("#divNumbers").find("p").prepend(strValue);
        i = i + 1;
        // });
    });
    $("#btnClassPSuccess").click(function () {
        $("#divClass").find("p").addClass("sucess");
    });
    //https://www.w3schools.com/jquery/html_toggleclass.asp
    $("#btnClassPError").click(function () {
        $("#divClass").find("p:even").removeClass("sucess");
        $("#divClass").find("p:even").addClass("error");
        //  $("divClass").find("p:even").addClass("error");
    });
    $("#btnClassPWarning").click(function () {
        $("#divClass").find("p").toggleClass();
        $("#divClass").find("p").addClass("warning");
    });
    $("#btnAddLastNames").click(function () {
        $(".bff").prev().append("<strong>Filintstone </strong>");
        $(".bff").append("<em>Rubble </em>");
        $(".bff").next().append("<del>the Dinosaur </del>");
    });
    $("#btnRemoveLarry").click(function () {
        //   https://api.jquery.com/remove/
        $("ul").find("li").first().remove();
    });
    $("#btnClearCurly").click(function () {
        // i am not sure which you want me to do do i commented the other one
        //  $("ul").find("li").first().next().empty();
        $("ul").find("li").first().empty();
    });
    $("#btnRemoveMoeLastName").click(function () {
        $("ul").find("li span").first().remove();
    });
    $("#btnRemoveShempLastName").click(function () {
        $(".third").next().find("span").first().remove();
        // $("ul").find("li").first().next().remove("span");
    });
    $("#btnChangeText").click(function () {
        //  https://www.w3schools.com/jquery/jquery_traversing_filtering.asp
        $("#spnTwo").parents().nextUntil("Change Me").find("span").first().next().text("I was changed");
    });
    //  https://www.w3schools.com/jquery/tryit.asp?filename=tryjquery_hide_p
    $("#btnTogglePs").on("click", function () {
        $("p").toggle(function () {
        });
    });  
});
