import { Utilities } from "../Classes/Utilities";

const getProjectNameButton: HTMLButtonElement = document.querySelector("#GetRandomProjectNameButton") as HTMLButtonElement;
const getSpecificProjectNameButton: HTMLButtonElement = document.querySelector("#GetSpecificProjectNameButton") as HTMLButtonElement;
const availableProjectNames: HTMLUListElement = document.querySelector("#projectNamesUL") as HTMLUListElement;
const inUseProjectNames: HTMLUListElement = document.querySelector("#projectNamesULinUse") as HTMLUListElement;


interface ProjectNameAsMoreComplicatedObject {
    projectName: string
}

class Program {
    static Main() {
        getProjectNameButton.addEventListener("click", function(){ Program.GetProjectNameButtonHandler("first") });
        getSpecificProjectNameButton.addEventListener("click", function(){ Program.GetProjectNameButtonHandler("specific") });
    }

    static GetProjectNameButtonHandler(getProjectNameOption: string) {
        var inUseProjectNameElements = inUseProjectNames.getElementsByTagName('li');
        var availableProjectNameElements = availableProjectNames.getElementsByTagName('li');
        if (availableProjectNameElements.length == 0){
            alert("No more available project names :(");
            return;
        }

        let projectNameChosen = $("#inputGetProjectName").val() as string;

        let formData = new FormData();
        formData.append("getProjectNameOption", getProjectNameOption);
        formData.append("projectNameChosen", projectNameChosen);

        let url = window.location.href + "&handler=GetAvailableProjectName";
        let requestVerificationToken = Utilities.GetRequestVerificationToken();

        let projectName = fetch(url, {
            method: "POST",
            headers: {
                "RequestVerificationToken": requestVerificationToken,
            },
            body: formData
        }).then(response => response.json())
        .then(json => 
            {
                
                let temp = json as ProjectNameAsMoreComplicatedObject;

                console.log(temp.projectName);

                window.location.href = "/InUseProject?projectName=" + temp.projectName;
                
                /*
                alert(`New project name is: ${temp.projectName}`);

                

                var projectName = document.createTextNode(temp.projectName);

                var a = document.createElement("a");
                a.setAttribute("href", "/InUseProject?projectName=" + temp.projectName);
                a.appendChild(projectName);


                var li = document.createElement("li");
                li.setAttribute("class", "list-group-item");
                li.appendChild(a);

                if (inUseProjectNameElements.length == 0) {
                    inUseProjectNames.appendChild(li);
                }
                
                for (var i = 0; i < inUseProjectNameElements.length; i++) {
                    var inUseProjectName = inUseProjectNameElements[i].getElementsByTagName("a")[0];
                    var inUseProjectNameText = inUseProjectName.textContent.toUpperCase();
                    var newProjectNameText = temp.projectName.toUpperCase();

                    if (i == inUseProjectNameElements.length - 1) {
                        if (newProjectNameText > inUseProjectNameText) {
                            inUseProjectNames.appendChild(li);
                            break;
                        }
                    }

                    if (inUseProjectNameText > newProjectNameText) {
                        inUseProjectNames.insertBefore(li, inUseProjectNameElements[i]);
                        break;
                    }
                }
                
                for (var i = 0; i < availableProjectNameElements.length; i++) {
                    var availableProjectName = availableProjectNameElements[i];
                    if (availableProjectName.textContent == temp.projectName) {
                        availableProjectNames.removeChild(availableProjectName);
                        break;
                    }
                }
                */
            });
    }
}

Program.Main();