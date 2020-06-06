// Get all Unassigned Categories as an array.
var unassignedCategoriesArray = [];

$('ul#categoriesList li').each(function(){
    unassignedCategoriesArray.push($(this).text());
});


// Get all Assigned Categories as an array.
var assignedCategoriesArray = [];

$('ul#categoriesListAssigned li a').each(function(){
    assignedCategoriesArray.push($(this).text());
});


// Add Category button.
$("#inputCategoryName").keyup(function() {
    var newCategoryName = $('#inputCategoryName').val();

    if ( newCategoryName.length < 1 ) {
        $("#addCategoryButton").attr("disabled", "disabled");
        $("#AddCategoryMessage").html("Enter the name of a new category.");
    } else if (unassignedCategoriesArray.indexOf(newCategoryName) > -1) {
        $("#AddCategoryMessage").html(newCategoryName + " is already an unassigned category.");
        $("#addCategoryButton").attr("disabled", "disabled");
    } else if (assignedCategoriesArray.indexOf(newCategoryName) > -1) {
        $("#AddCategoryMessage").html(newCategoryName + " is already an assigned category.");
        $("#addCategoryButton").attr("disabled", "disabled");
    } else {
        $("#AddCategoryMessage").html("Add " + newCategoryName + " to unassigned categories.");
        $("#addCategoryButton").attr("disabled", false);
    }
});

// Assign Category Button.
$("#inputProjectType").keyup(enableButtonIfValid);
$("#category").keyup(enableButtonIfValid);

$("#AssignButton").click(function() {
    $("#AssignButton").attr("disabled", "disabled");
    $("#AssignCategoryMessage").html("Enter a project type and unassigned category name.");
});

// Enable or disable the assign function, based on valid Category and ProjectType inputs.
function enableButtonIfValid() {
    var categoryInputted = $('#category').val();
    var categoryInputtedLength = categoryInputted.length;

    var projectTypeInputted = $('#inputProjectType').val();
    var projectTypeLength = projectTypeInputted.length;

    var unassignedProjectNameExists = (unassignedCategoriesArray.indexOf(categoryInputted) > -1);

    if ( (categoryInputtedLength < 1) && (projectTypeLength < 1) ) {
        $("#AssignCategoryMessage").html("Enter a project type and unassigned category name.");
    } else if (!unassignedProjectNameExists) {
        $("#AssignCategoryMessage").html(categoryInputted + " is not a valid unassigned category name.");
        $("#AssignButton").attr("disabled", "disabled");
    } else if (projectTypeLength < 1) {
        $("#AssignCategoryMessage").html("Project Type cannot be empty.");
        $("#AssignButton").attr("disabled", "disabled");
    } else {
        $("#AssignButton").attr("disabled", false);
        $("#AssignCategoryMessage").html("Assign "+ categoryInputted + " to " + projectTypeInputted);
    }
}