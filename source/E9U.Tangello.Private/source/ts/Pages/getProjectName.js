var getProjectNameButton = document.querySelector("#GetProjectNameButton");
var availableProjectNames = document.querySelector("#projectNamesUL");
var inUseProjectNames = document.querySelector("#projectNamesULinUse");
var Program = /** @class */ (function () {
    function Program() {
    }
    Program.Main = function () {
        getProjectNameButton.addEventListener("click", Program.GetProjectNameButtonHandler);
    };
    Program.GetProjectNameButtonHandler = function () {
        var availableProjectNameElements = availableProjectNames.getElementsByTagName('li');
        if (availableProjectNameElements.length == 0) {
            alert("No more available project names :(");
            return;
        }
        var url = window.location.href + "&handler=GetProjectName2";
        var requestVerificationToken = Program.GetRequestVerificationToken();
        var projectName = fetch(url, {
            method: "POST",
            headers: {
                "RequestVerificationToken": requestVerificationToken,
            }
        }).then(function (response) { return response.json(); })
            .then(function (json) {
            var temp = json;
            console.log(temp.projectName);
            alert("New project name is: " + temp.projectName);
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
    };
    Program.GetRequestVerificationToken = function () {
        var requestVerificationTokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
        var requestVerificationToken = requestVerificationTokenElement.value;
        return requestVerificationToken;
    };
    Program.sortList = function (ul) {
        var new_ul = ul.cloneNode(false);
        // Add all lis to an array
        var liArray = [];
        for (var i = ul.childNodes.length; i--;) {
            if (ul.childNodes[i].nodeName === 'LI')
                liArray.push(ul.childNodes[i]);
        }
        liArray.sort();
        // Add them into the ul in order
        for (var i = 0; i < liArray.length; i++)
            new_ul.appendChild(liArray[i]);
        ul.parentNode.replaceChild(new_ul, ul);
    };
    return Program;
}());
Program.Main();
//# sourceMappingURL=getProjectName.js.map