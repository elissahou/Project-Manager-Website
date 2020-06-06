import { Utilities } from "../Classes/Utilities";

const inputProjectTypeElement: HTMLInputElement = document.querySelector("#inputProjectType") as HTMLInputElement;
const renameProjectTypeButton: HTMLButtonElement = document.querySelector("#RenameProjectTypeButton") as HTMLButtonElement;

renameProjectTypeButton.addEventListener("click", renameProjectType)

function renameProjectType() {
    let inputProjectType = inputProjectTypeElement.value;
    
    let formData = new FormData();
    formData.append("inputProjectType", inputProjectType);
    
    let url = window.location.href + "&handler=RenameProjectTypeForOneCategoryOnly";
    let requestVerificationToken = Utilities.GetRequestVerificationToken()

    let renameProjectType = fetch(url, {
        method: "POST",
        headers: {
            "RequestVerificationToken": requestVerificationToken
        },
        body: formData
    }).then(response => response.json())
    .then(json => {
        var newProjectType = json.projectType;

        // Update ProjectType values on page
        $('#projectTypeInHeader').text(newProjectType);
        $('#inputProjectType').attr("placeholder", "Rename " + newProjectType);

        // Empty the input
        inputProjectTypeElement.value = "";

        // Disable the button
        $("#RenameProjectTypeButton").attr("disabled", "disabled");
    })
}