const myInput : HTMLInputElement = document.querySelector('#myInput');
myInput.addEventListener("keyup", searchInUL);

function searchInUL() {
    // Declare variables
    var input, filter, ul, ul2, li, li2, a, i, txtValue;
    input = document.getElementById('myInput');

    filter = input.value.toUpperCase();
    ul = document.getElementById("projectNamesUL");
    ul2 = document.getElementById("projectNamesULinUse");
    console.log(ul);
    console.log(ul2);
    li = ul.getElementsByTagName('li');
    li2 = ul2.getElementsByTagName('li');

    // Loop through all list items, and hide those who don't match the search query
    for (i = 0; i < li.length; i++) {
        a = li[i];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }

    for (i = 0; i < li2.length; i++) {
        a = li2[i];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li2[i].style.display = "";
        } else {
            li2[i].style.display = "none";
        }
    }
}