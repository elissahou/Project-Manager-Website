// Get all Available Project Names as an array.
var availableProjectNamesArray = [];

$('ul#projectNamesUL li').each(function(){
    availableProjectNamesArray.push($(this).text());
});


// Get all In Use Project Names as an array.
var inUseProjectNamesArray = [];

$('ul#projectNamesULinUse li a').each(function(){
    inUseProjectNamesArray.push($(this).text());
});


// Get a specific project name button.
$("#inputGetProjectName").keyup(function() {
    var getProjectName = $('#inputGetProjectName').val();

    if ( getProjectName.length < 1 ) {
        $("#GetSpecificProjectNameButton").attr("disabled", "disabled");
        $("#GetProjectNameMessage").html("Enter an available project name.");
    } else if (availableProjectNamesArray.indexOf(getProjectName) < 0) {
        $("#GetProjectNameMessage").html(getProjectName + " is not an available project name.");
        $("#GetSpecificProjectNameButton").attr("disabled", "disabled");
    } else {
        $("#GetProjectNameMessage").html("Get project name " + getProjectName);
        $("#GetSpecificProjectNameButton").attr("disabled", false);
    }
});

// Add a project name button.
$("#inputProjectName").keyup(function() {
    var newProjectName = $('#inputProjectName').val();

    if ( newProjectName.length < 1 ) {
        $("#addProjectNameButton").attr("disabled", "disabled");
        $("#AddProjectNameMessage").html("Enter a new project name.");
    } else if (availableProjectNamesArray.indexOf(newProjectName) > -1) {
        $("#AddProjectNameMessage").html(newProjectName + " is already an available project name.");
        $("#addProjectNameButton").attr("disabled", "disabled");
    } else if (inUseProjectNamesArray.indexOf(newProjectName) > -1) {
        $("#AddProjectNameMessage").html(newProjectName + " is already an ongoing project.");
        $("#addProjectNameButton").attr("disabled", "disabled");
    } else {
        $("#AddProjectNameMessage").html("Add " + newProjectName + " to available project namess.");
        $("#addProjectNameButton").attr("disabled", false);
    }
});


// Rename project type button.
$("#inputProjectType").keyup(function() {
    $("#RenameProjectTypeButton").attr("disabled", false);
});