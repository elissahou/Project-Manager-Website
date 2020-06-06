import { Utilities } from "../Classes/Utilities";

const descriptionTextArea: HTMLTextAreaElement = document.querySelector("#projectDescription") as HTMLTextAreaElement;
const saveChangesButton: HTMLButtonElement = document.querySelector("#SaveDescriptionButton") as HTMLButtonElement;

saveChangesButton.addEventListener("click", saveChanges)

function saveChanges() {
    let description = descriptionTextArea.value;
    
    let formData = new FormData();
    formData.append("description", description);
    
    let url = window.location.href + "&handler=SaveChanges";
    let requestVerificationToken = Utilities.GetRequestVerificationToken()

    let changeDescription = fetch(url, {
        method: "POST",
        headers: {
            "RequestVerificationToken": requestVerificationToken
        },
        body: formData
    });
}