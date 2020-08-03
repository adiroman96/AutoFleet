const registrationNumberInput = document.getElementById("registrationNumberInput");
const manufacturingYearInput = document.getElementById("manufacturingYearInput");
const carIdInput = document.getElementById("Id");
const driverDropdown = document.getElementById("changeDriverDropdown");
const lastITPInput = document.getElementById("lastITPInput");
const expirationDateITPInput = document.getElementById("expirationDateITPInput");

$(document).ready(function () {

    var currentURL = window.location.href;
    var idCar = currentURL.split("/Cars/Edit/")[1]; // get id of current car

    fillFields(idCar); // fill known fields
});


function fillFields(idCar) {
    fetch('/cars/cardto/' + idCar)
        .then(
            function (response) {
                if (response.status !== 200) {// ToDo: make it give a nice error on the page
                    console.log('Looks like there was a problem. Status Code: ' +
                        response.status);
                    return;
                }

                // Examine the text in the response
                response.json().then(function (carDto) {
                    registrationNumberInput.value = carDto.carRegistrationNumber;
                    manufacturingYearInput.value = carDto.carManufacturingYear;
                    prepareDriverDropdown(carDto.idDriver, carDto.driverName, carDto.driverEmail);
                    createInsurancesTable(carDto.insurances);
                });
            }
        )
        .catch(function (err) {
            console.log('Fetch Error :-S', err); // ToDo: make it give a nice error on the page
        });
}

/*
 * Sets main option to "driverName"
 * and loads other drivers
 */
function prepareDriverDropdown(idDriver, driverName, driverEmail) {

    driverDropdown.length = 0;

    let defaultOption = document.createElement('option');
    defaultOption.text = driverName;
    defaultOption.title = driverName + " " + driverEmail;
    defaultOption.value = idDriver;

    driverDropdown.add(defaultOption);
    driverDropdown.selectedIndex = 0;

    fetch('/drivers/getall')
        .then(
            function (response) {
                if (response.status !== 200) {// ToDo: make it give a nice error on the page
                    console.log('Looks like there was a problem. Status Code: ' +
                        response.status);
                    return;
                }

                // Examine the text in the response
                response.json().then(function (myJson) {
                    const data = myJson.data; // get data from the json

                    for (let i = 0; i < data.length; i++) {

                        if (data[i].id !== idDriver) {
                            option = document.createElement('option');
                            option.title = data[i].name + " " + data[i].email;
                            option.text = data[i].name;
                            option.value = data[i].id;
                            driverDropdown.add(option);
                        }
                    }
                });
            }
        )
        .catch(function (err) {
            console.log('Fetch Error :-S', err); // ToDo: make it give a nice error on the page
        });
}

/*
 * Creates a table of <div> with car.insurance
 * each row contains: LastRenewal, ExpirationDate
 */
function createInsurancesTable(insurances) {
    let form = document.getElementById("formId");

    for (let i = 0; i < insurances.length; i++) {

        let divRow = document.createElement("div");
        divRow.setAttribute("class", "row");

        divRow.appendChild(createLeftCell(insurances[i].typeOfInsurance, insurances[i].lastRenewal) );
        divRow.appendChild(createRightCell(insurances[i].typeOfInsurance, insurances[i].expirationDate) );
        form.appendChild(divRow);
    }

    form.appendChild(createFormSubmit());
}

function createLeftCell(typeOfInsurance, date) {
    let parent = document.createElement("div");
    parent.setAttribute("class", "col-md-5 form-group");

    let lbl = document.createElement("label");
    lbl.setAttribute("class", "control-label");
    lbl.textContent = typeOfInsurance + " a fost realizat(a) la:";

    let input = document.createElement("input");
    input.setAttribute("id", "lastRenewalDate" + typeOfInsurance);
    input.setAttribute("class", "form-control");
    input.setAttribute("type", "date");
    input.setAttribute("value", getDateInQuotesFromDateTime(date));

    //for warnings:
    let span = document.createElement("span");
    span.setAttribute("class", "text-danger");

    parent.appendChild(lbl);
    parent.appendChild(input);
    parent.appendChild(span);
    return parent;
}

function createRightCell(typeOfInsurance, date) {
    let parent = document.createElement("div");
    parent.setAttribute("class", "col-md-6 form-group");

    let lbl = document.createElement("label");
    lbl.setAttribute("class", "control-label");
    lbl.textContent = "Expira la:";


    let div = document.createElement("div");
    div.setAttribute("class", "input-group mb-3");

    let input = document.createElement("input");
    input.setAttribute("id", "expirationDate" + typeOfInsurance);
    input.setAttribute("class", "form-control");
    input.setAttribute("type", "date");
    input.setAttribute("readonly", "readonly");
    input.setAttribute("value", getDateInQuotesFromDateTime(date));


    let deleteDiv = document.createElement("div");
    deleteDiv.setAttribute("class", "input-group-append pl-4");
    let button = document.createElement("button");
    button.setAttribute("id", "deleteBtn");
    button.setAttribute("class", "btn btn-danger");
    button.setAttribute("type", "button");
    button.innerText = "Sterge";
    deleteDiv.appendChild(button);

    div.appendChild(input);
    div.appendChild(deleteDiv);


    parent.appendChild(lbl);
    parent.appendChild(div);
    return parent;
}

function createFormSubmit() {
    var formSubmitElement = document.createElement("div");
    formSubmitElement.setAttribute("class", "form-group");

    var inputElement = document.createElement("input");
    inputElement.setAttribute("type", "submit");
    inputElement.setAttribute("value", "Salveaza");
    inputElement.setAttribute("class", "btn btn-primary");

    formSubmitElement.appendChild(inputElement);
    return formSubmitElement;
}

function getDateInQuotesFromDateTime(dateTime) {
    //return "\"" + dateTime.split("T")[0] + "\"";
    return dateTime.split("T")[0];

}

function createTable() {
    let form = document.getElementById("formId");

    try {
        var table = document.getElementById('dataTable');
        form.appendChild(table);

        var rowCount = table.rows.length;
        for (var i = 0; i < rowCount; i++) {
            var row = table.rows[i];
            var rowrowObj = row.cells[0].childNodes[0];
            if (rowObj.name == btnName) {
                table.deleteRow(i);
                rowCount--;
            }
        }
    }
    catch (e) {
        alert(e);
    }
}