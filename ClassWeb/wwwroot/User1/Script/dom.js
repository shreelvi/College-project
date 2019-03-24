/*
    Kishor Simkhada
    Feb 20 2018
    Info-2202
    Jon Holmes
*/
arrName = new Array("Fred", "Wilma", "Pebbles", "Barney", "Betty", "BamBam");
arrLastName = new Array("Flintstone", "Flintstone", "Flintstone", "Rubble", "Rubble", "Rubble");
arrAges = new Array(36, 34, 7, 34, 32, 8);
function showInnerText() {
   var sit = document.getElementById("pOne");
    alert(sit.innerText);
}
function showInnerHTML() {
   var sih = document.getElementById("pOne");
    alert(sih.innerHTML);
}
function numberList() {
    //var ul = document.getElementsByTagName("ul");
    var li = document.getElementsByTagName("li");
    for (var i = 0; i < li.length; i++) {
        var temp = i + 1;
        if (li[i].innerText === "Item") {
            var items = li[i].innerText + " " + temp;
            li[i].innerText = items;
        }//ul.appendChild(items);
    }
    
}
function markRows() {
    var table = document.getElementById("tblMarkRows");

    var tr = table.getElementsByTagName("tr");
    for (var i = 0; i < tr.length; i++) {
        if (i % 2 === 0) {
            tr[i].className = "other";
        }
     // alert(tr[i].className) ;  
    }
}
function addRows() {
    for (var i = 0; i < arrName.length; i++) {
        var tbl = document.getElementById("tblAddRows");
        var tr = document.createElement("tr");

        var tdName = document.createElement("td");
        var tdLast = document.createElement("td");
        var tdAge = document.createElement("td"); 

        tdName.innerText = arrName[i];
        tdLast.innerText = arrLastName[i];
        tdAge.innerText = arrAges[i];

        tr.appendChild(tdName);
        tr.appendChild(tdLast);
        tr.appendChild(tdAge);
        tbl.appendChild(tr);
    }
    var tr = tbl.getElementsByTagName("tr");
    for (var i = 0; i < tr.length; i++) {
        if (i % 2 === 0) {
            tr[i].className = "other";
        }
        // alert(tr[i].className);
    }
}
function addSuccess() {
    var div = document.createElement("div");
   // var text = div.createElement("text");
    div.className = "sucess";
    div.innerText = "This is a Sucess Message";
    document.getElementById("divMessage").appendChild(div);
   // var para = document.createElement("p")
   // para.innerText = ;
   // div.appendChild(para);
}
function addWarning() {
    var div = document.createElement("div");
    div.className = "warning";
    div.innerHTML = "This is a Warning Message";
    document.getElementById("divMessage").appendChild(div);
}
function addError() {
    var div = document.createElement("div");
    div.className = "error";
    div.innerText = "This is an Error Message";
    document.getElementById("divMessage").appendChild(div);
}