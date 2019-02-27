$(document).ready(function () {
    $("#btnSearch").click(function () {
        // alert($("#searchInput").val().trim());
        if ($("#searchInput").val().trim() == null || $("#searchInput").val().trim() == "") {
            $.ajax({
                url: "data/books.json"
                , dataType: "json"
                , success: function (books) {
                    var table = $(".tblajax");
                    var tblData = $("#trdata");
                    //var tblRow = $("<tr></tr>");
                    if (books.ok == "true") {
                        tblData.empty();
                        for (var i = 0; i < books.data.length; i++) {
                            var book = books.data[i];
                            var trRow = $("<tr></tr>");
                            var tdTitle = $("<td></td>").text(book.title);
                            var tdISBN = $("<td></td>").text(book.ISBN);
                            var tdAuthorName = $("<td></td>").text(book.author.firstName + " " + book.author.middleName + " " + book.author.lastName);
                            book.tags.length;
                            var ulTag = $("<ul></ul>");
                            for (var j = 0; j < book.tags.length; j++) {
                                var liTags = $("<li></li>");
                                text = document.createTextNode(book.tags[j])
                                liTags.append(text.data);
                                ulTag.append(liTags);
                            }
                            //var tdTags = $("<td></td>").text(book.tags);
                            //  tdTags;
                            //trRow.append(tdTitle, tdISBN, tdAuthorName, ulTag);
                            // tblData.append(trRow.append(tdTitle, tdISBN, tdAuthorName, ulTag));
                            table.append(trRow.append(tdTitle, tdISBN, tdAuthorName, ulTag))
                        }
                    }
                }
                , error: function () {
                    alert("there is error in file");
                }
            });
        }
        else {
            $.ajax({
                url: "data/books.json"
                , dataType: "json"
                , success: function (books) {
                    var table = $(".tblajax");
                    var tblData = $("#trdata");
                    var txtValue = $("#searchInput").val().trim().toLowerCase();
                    //var tblRow = $("<tr></tr>");
                    if (books.ok == "true") {
                        tblData.children().empty();
                        for (var i = 0; i < books.data.length; i++) {
                            var book = books.data[i];
                            var tag = book.tags;
                            for (var j = 0; j < tag.length; j++) {
                                if (book.title.toLowerCase().indexOf(txtValue) > -1 ||
                                    book.ISBN.toLowerCase().indexOf(txtValue) > -1 ||
                                    book.author.firstName.toLowerCase().indexOf(txtValue) > -1 ||
                                    book.author.lastName.toLowerCase().indexOf(txtValue) > -1 ||
                                    book.author.middleName.toLowerCase().indexOf(txtValue) > -1 ||
                                    book.tags[j].toLowerCase().indexOf(txtValue) > -1) {
                                    var trRow = $("<tr></tr>");
                                    var tdTitle = $("<td></td>").text(book.title);
                                    var tdISBN = $("<td></td>").text(book.ISBN);
                                    var tdAuthorName = $("<td></td>").text(book.author.firstName + " " + book.author.middleName + " " + book.author.lastName);
                                    var ulTag = $("<ul></ul>");
                                    for (var j = 0; j < book.tags.length; j++) {
                                        var liTags = $("<li></li>");
                                        text = document.createTextNode(book.tags[j])
                                        liTags.append(text.data);
                                        ulTag.append(liTags);
                                    }
                                    trRow.append(tdTitle, tdISBN, tdAuthorName, ulTag);
                                    table.append(trRow);
                                }
                            }

                        }
                    }
                }
                , error: function () {
                    alert("there is error in file");
                }
            });
        }
        return false;
    });
    return false;
});
$(document).ready(function () {
    $("#searchPeople").keypress(function () {
        $.ajax({
            url: "data/people.json"
            , dataType: "json"
            , success: function (peoples) {
                var table = $("#tblPeople");
                var tblData = $("#trpeople");
                var peopleSearch = $("#searchPeople").val().trim().toLowerCase();
                tblData.empty();
                var ulTag = $("<ul></ul>");
                for (var i = 0; i < peoples.length; i++) {
                    if (peoples[i].name.toLowerCase().indexOf(peopleSearch) > -1
                        || peoples[i].age == peopleSearch
                        || peoples[i].phone.toLowerCase().indexOf(peopleSearch) > -1
                        || peoples[i].group==peopleSearch) {

                        var ulList = $("<ul></ul>");
                        var nameList = $("<li></li>");
                        var ageList = $("<li></li>");
                        var phoneList = $("<li></li>");
                        var groupList = $("<li></li>");

                        var nameText = document.createTextNode("Name: " + peoples[i].name);
                        var ageText = document.createTextNode("Age: " + peoples[i].age);
                        var phoneText = document.createTextNode("Phone: " + peoples[i].phone);
                        var groupText = document.createTextNode("Group: " + peoples[i].group);

                        nameList.append(nameText);
                        ageList.append(ageText);
                        phoneList.append(phoneText);
                        groupList.append(groupText);

                        ulList.append(nameList, ageList, phoneList, groupList);
                        tblData.append(ulList);
                    }
                    table.append(tblData);
                }
            }
        });
    });
});