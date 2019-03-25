/*
    Kishor Simkhada
    Feb 06 2018
    Info-2202
    Jon Holmes
*/
alert("The script in the header has run.");
var loadLast = 'I loaded last';
var arrArray;
if (arrArray === undefined) {
    //https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Grammar_and_types#Array_literals
    arrArray = new Array(34, 5.5, 33.5, 46.5, 59, 64, 43, 64, 48, 49, 40, 59, 44, 54, 19.5, 25, 69, 23);
}
else {
     alert("Array was not defined");
}
var sum = 0;
var count = 0;
for (var i = 0; i < arrArray.length; i++) {
    sum = sum + arrArray[i];
    count++;
}
alert(sum / count);
function Name() {
    return "Kishor Simkhada";
}