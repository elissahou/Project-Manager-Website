$("#EditDescriptionButton").click(function() {
    $("#projectDescription").attr("disabled", false);
    $("#SaveDescriptionButton").attr("disabled", false);
    $("#EditDescriptionButton").attr("disabled", "disabled");
    $("#charactersRemaining").attr("style", "display:''");
    $("#ChangesSavedText").attr("style", "display:none");
});

$("#SaveDescriptionButton").click(function() {
    $("#projectDescription").attr("disabled", "disabled");
    $("#SaveDescriptionButton").attr("disabled", "disabled");
    $("#EditDescriptionButton").attr("disabled", false);
    $("#charactersRemaining").attr("style", "display:none");
    $("#ChangesSavedText").attr("style", "float:right;display:''");
});