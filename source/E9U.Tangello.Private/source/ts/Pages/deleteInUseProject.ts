import { Utilities } from "../Classes/Utilities";

const deleteInUseProjectButton: HTMLButtonElement = document.querySelector("#DeleteProjectNameButton") as HTMLButtonElement;

deleteInUseProjectButton.addEventListener("click", deleteProject);

interface category {
    categoryName: string
}

function deleteProject() {
    if (confirm("Are you sure you want to delete this project?\
    \nThe project description will be permanently deleted.\
    \nThe project name will become available again.")) {
      } else {
        return;
      }
    let url = window.location.href + "&handler=DeleteProject";
    let requestVerificationToken = Utilities.GetRequestVerificationToken()

    let category = fetch(url, {
        method: "POST",
        headers: {
            "RequestVerificationToken": requestVerificationToken
        },
    }).then(response => response.json())
    .then(json => {
        let temp = json as category;
        let categoryString = temp.categoryName;
        window.location.href = "/ProjectNames?categoryName=" + categoryString;
    })
};