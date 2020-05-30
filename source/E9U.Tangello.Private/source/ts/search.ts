const myInput : HTMLInputElement = document.querySelector('#myInput');
myInput.addEventListener("keyup", searchInUL);

function searchInUL() {
    // Declare variables
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById('myInput');

    filter = input.value.toUpperCase();
    ul = document.getElementById("projectNamesUL");
    console.log(ul)
    li = ul.getElementsByTagName('li');

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
}