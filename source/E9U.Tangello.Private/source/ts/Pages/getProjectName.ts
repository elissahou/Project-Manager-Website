const getProjectNameButton: HTMLButtonElement = document.querySelector("#GetProjectNameButton") as HTMLButtonElement
const availableProjectNames: HTMLUListElement = document.querySelector("#projectNamesUL") as HTMLUListElement;
const inUseProjectNames: HTMLUListElement = document.querySelector("#projectNamesULinUse") as HTMLUListElement;

interface ProjectNameAsMoreComplicatedObject {
    projectName: string
}

class Program {
    static Main() {
        getProjectNameButton.addEventListener("click", Program.GetProjectNameButtonHandler);
    }

    static GetProjectNameButtonHandler() {
        var availableProjectNameElements = availableProjectNames.getElementsByTagName('li');
        if (availableProjectNameElements.length == 0){
            alert("No more available project names :(");
            return;
        }

        let url = window.location.href + "&handler=GetProjectName2";
        let requestVerificationToken = Program.GetRequestVerificationToken();

        let projectName = fetch(url, {
            method: "POST",
            headers: {
                "RequestVerificationToken": requestVerificationToken,
            }
        }).then(response => response.json())
        .then(json => 
            {
                let temp = json as ProjectNameAsMoreComplicatedObject;

                console.log(temp.projectName);

                alert(`New project name is: ${temp.projectName}`);

                var li = document.createElement("li");
                li.setAttribute("class", "list-group-item");
                li.appendChild(document.createTextNode(temp.projectName));

                inUseProjectNames.appendChild(li);
                Program.sortList(inUseProjectNames);
                
                for (var i = 0; i < availableProjectNameElements.length; i++) {
                    var availableProjectName = availableProjectNameElements[i];
                    if (availableProjectName.textContent == temp.projectName) {
                        availableProjectNames.removeChild(availableProjectName);
                        break;
                    }
                }
            });
    }

    static GetRequestVerificationToken(): string {
        let requestVerificationTokenElement: HTMLInputElement = document.querySelector('input[name="__RequestVerificationToken"]');

        let requestVerificationToken = requestVerificationTokenElement.value;
        return requestVerificationToken;
    }

    static sortList(ul: HTMLUListElement) {
        var new_ul = ul.cloneNode(false);

        // Add all lis to an array
        var liArray = [];
        for (var i = ul.childNodes.length; i--;) {
            if (ul.childNodes[i].nodeName === 'LI')
                liArray.push(ul.childNodes[i]);
        }
    
        liArray.sort();
    
        // Add them into the ul in order
        for(var i = 0; i < liArray.length; i++)
            new_ul.appendChild(liArray[i]);
        ul.parentNode.replaceChild(new_ul, ul);
    }
}

Program.Main();