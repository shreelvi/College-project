/*
    Kishor Simkhada
    Feb 27 2018
    Info-2202
    Jon Holmes
*/
window.onload = function () {
    var sec = document.getElementById("secMouser");
    sec.onmousemove = function () {
        sec.addEventListener("mousemove", moveMouse());
    }
    sec.onmousedown = function () {
        sec.addEventListener("mousedown", mousePressed());
    }
    sec.onmouseup = function () {
        sec.addEventListener("mouseup", resetClass());
    }
    sec.onmouseover = function () {
        sec.addEventListener("mouseover", mouseHeadOver());
    }
    sec.onmouseout = function () {
        sec.addEventListener("mouseup", resetClass());
    }
    var button = document.getElementById("btnSayHello");
    button.onclick = function () {
        alert("Hello User");
    }
    button.onmousemove = function () {
        var button = document.getElementById("btnSayHello");
        button.cancelBubble = true;
        event.stopPropagation();
        var cords = "Over the button";
        document.getElementById("mouseCoords").innerText = cords;
    }
};
// sec.addEventListener("mousemove", moveMouse());
// sec.addEventListener("mousedown", mousePressed());
// sec.addEventListener("mouseup", resetClass());
//var header = sec.getElementsByTagName("header");
//for (var i = 0; i < header.length; i++) {
//    header[i].addEventListener("mouseover", mouseHeadOver());
//    header[i].addEventListener("mouseout", resetClass());
//}
//window.onmousemove = moveMouse();

//var button =button.onclick = function () {

//}
//button.onmousemove = function () {
//    e = e || window.event;
//    var cords = "Over the button";
//    document.getElementById("mouseCoords").innerText = cords;
//    e.cancelBubble = true;
//    event.stopPropagation();
//};
// header.addEventListener()


//https://plainjs.com/javascript/events/getting-the-current-mouse-position-16/
function moveMouse(e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    var x = e.clientX;
    var y = e.clientY;
    //   IE 8
    if (x === undefined) {
        x = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
        y = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
    }
    // alert("X: " + x + ",Y: " + y);
    var cord = "X: " + x + ",Y: " + y;
    document.getElementById("mouseCoords").innerText = cord;
    target.cancelBubble = true;
    event.stopPropagation();
}
if (document.attachEvent) document.attachEvent('onclick', moveMouse);
else document.addEventListener('click', moveMouse);
function mousePressed(e) {
    e = e || window.event;
    // var target = getTarget(e);
    var target = e.target || e.srcElement;
    {
        target.classList.add("pressed");
        // }
        target.cancelBubble = true;
        event.stopPropagation();
        //alert(target.classList);

    }
}
function resetClass(e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    // var target = e.target || e.srcElement;
    //if (value) {

    //}
    if (target.classList) {
        target.className = " ";
    }
    target.cancelBubble = true;
    event.stopPropagation();
}

function mouseHeadOver(e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    // var target = get(target(e));
    target.classList.add("overHead");
    target.cancelBubble = true;
    event.stopPropagation();
}