import { Utilities } from "../Classes/Utilities";

const deleteCategoryButton: HTMLButtonElement = document.querySelector("#DeleteCategoryButton") as HTMLButtonElement;

deleteCategoryButton.addEventListener("click", deleteCategory);

interface category {
    categoryString: string
}

function deleteCategory() {
    if (confirm("Are you sure you want to delete this category?\
    \nAll of its projects will be permanently deleted.")) {
      } else {
        return;
      }
    let url = window.location.href + "&handler=DeleteCategory";
    let requestVerificationToken = Utilities.GetRequestVerificationToken()

    let category = fetch(url, {
        method: "POST",
        headers: {
            "RequestVerificationToken": requestVerificationToken
        },
    }).then(_ => {
        window.location.href = "/Categories";
    });

    
};