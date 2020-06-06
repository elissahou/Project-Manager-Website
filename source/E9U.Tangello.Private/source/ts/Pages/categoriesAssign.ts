import { Utilities } from "../Classes/Utilities";

const assignButton: HTMLButtonElement = document.querySelector("#AssignButton") as HTMLButtonElement;
const unassignedCategories: HTMLUListElement = document.querySelector("#categoriesList") as HTMLUListElement;
const assignedCategories: HTMLUListElement = document.querySelector("#categoriesListAssigned") as HTMLUListElement;

interface CategoryToProjectType {
    category: string,
    projectType: string,
}

class CategoriesProgram {
    static Main(){
        assignButton.addEventListener("click", CategoriesProgram.AssignButtonHandler);
    }
    static AssignButtonHandler() {
        var unassignedCategoriesElements = unassignedCategories.getElementsByTagName("li");
        var assignedCategoriesElements = assignedCategories.getElementsByTagName("li");

        let categoryInput: HTMLInputElement = document.querySelector("#category");
        let inputProjectTypeInput: HTMLInputElement = document.querySelector("#inputProjectType");

        let category = categoryInput.value;
        let inputProjectType = inputProjectTypeInput.value;

        let formData = new FormData();

        formData.append("category", category);
        formData.append("inputProjectType", inputProjectType);

        let url = window.location.href + "?handler=Assign";
        let requestVerificationToken = Utilities.GetRequestVerificationToken();

        let variable = fetch(url,{
            method: "POST",
            headers: {
                "RequestVerificationToken": requestVerificationToken
            },
            body: formData
        }).then(response => response.json())
        .then(json =>
            {
                console.log(json);
                let temp = json as CategoryToProjectType;
                console.log(temp.category);
                console.log(temp.projectType);

                var categoryName = document.createTextNode(temp.category);
                var projectTypeName = document.createTextNode(temp.projectType);

                var a = document.createElement("a");
                a.setAttribute("href", "/ProjectNames?categoryName=" + temp.category);
                a.appendChild(categoryName);

                var span = document.createElement("span");
                span.setAttribute("class", "text-muted");
                span.setAttribute("style", "font-style:italic; float:right");
                span.appendChild(projectTypeName);

                var li = document.createElement("li");
                li.setAttribute("class", "list-group-item");
                li.appendChild(a);
                li.appendChild(span);

                // Add Category to assigned list, in correct alphabetical place.
                if (assignedCategoriesElements.length == 0) {
                    assignedCategories.appendChild(li)
                }

                for (var i = 0; i < assignedCategoriesElements.length; i++) {
                    var assignedCategory = assignedCategoriesElements[i].getElementsByTagName("a")[0];
                    var assignedCategoryText = assignedCategory.textContent.toUpperCase();
                    var newCategoryText = temp.category.toUpperCase();
                    
                    if (i == assignedCategoriesElements.length - 1) {
                        if (newCategoryText > assignedCategoryText) {
                            assignedCategories.appendChild(li);
                            break;
                        }
                    }

                    if (newCategoryText < assignedCategoryText) {
                        assignedCategories.insertBefore(li, assignedCategoriesElements[i])
                        break;
                    }
                }

                // Remove Category from unassigned list.
                for (var i = 0; i < unassignedCategoriesElements.length; i++) {
                    var unassignedCategory = unassignedCategoriesElements[i];
                    if (unassignedCategory.innerText == temp.category) {
                        unassignedCategories.removeChild(unassignedCategory);
                        break;
                    }
                }

                // Empty the input.
                $( ":text" ).val("");

                // Disable the button.
                $("#AssignButton").attr("disabled", "disabled");
            });
    }
}

CategoriesProgram.Main();