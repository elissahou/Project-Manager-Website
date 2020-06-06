const myInputCategories : HTMLInputElement = document.querySelector('#myInputCategories');
myInputCategories.addEventListener("keyup", searchInCategoriesUL);

function searchInCategoriesUL() {
    // Declare variables
    var input, filter, ul, ul2, li, li2, a, i, txtValue;
    input = document.getElementById('myInputCategories');

    filter = input.value.toUpperCase();
    ul = document.getElementById("categoriesList");
    ul2 = document.getElementById("categoriesListAssigned");
    console.log(ul);
    console.log(ul2);
    li = ul.getElementsByTagName('li');
    li2 = ul2.getElementsByTagName('li');

    // Loop through all list items, and hide those who don't match the search query

    // Unassigned categories.
    for (i = 0; i < li.length; i++) {
        a = li[i];
        txtValue = a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }

    // Assigned categories.
    for (i = 0; i < li2.length; i++) {
        a = li2[i].getElementsByTagName("a")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li2[i].style.display = "";
        } else {
            li2[i].style.display = "none";
        }
    }
}