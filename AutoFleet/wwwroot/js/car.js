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


    for (var i = 0; i < insurances.length; i++) {
        var leftCell = document.createElement("div");
        leftCell.setAttribute("class", "col-md-6");
        leftCell.innerHTML = createLeftCell(insurances[i].typeOfInsurance, insurances[i].lastRenewal).toString().trim();

        var rightCell = document.createElement("div");
        rightCell.setAttribute("class","col-md-6");
        rightCell.innerHTML = createRightCell(insurances[i].typeOfInsurance, insurances[i].expirationDate).toString().trim();

        var divRow = document.createElement("div");
        divRow.setAttribute("class", "row");
        divRow.appendChild(leftCell);
        divRow.appendChild(rightCell);
        form.appendChild(divRow);
    }

    form.appendChild(createFormSubmit());
}

function createLeftCell(typeOfInsurance, date) {
    return  "<div class=\"form-group\">" +
        "<label class=\"control-label\">Ultima reinnoire pentru " + typeOfInsurance + "</label>" +
        "<input type=\"date\" id=\"" + typeOfInsurance + "LastRenewalInput\" class=\"form-control\" value=" + getDateInQuotesFromDateTime(date) + "/>" +
        "<span class=\"text-danger\"></span>" +
        "</div >";
}

function createRightCell(typeOfInsurance, date) {
    return    "<div class=\"form-group\">" +
        "<label class=\"control-label\">Expira la</label>" +
        "<input type=\"date\" readonly=\"readonly\" id=\"expirationDate" + typeOfInsurance + "\" class=\"form-control\" value=" + getDateInQuotesFromDateTime(date) + "/>" +
        "<span class=\"text-danger\"></span>" +
        "</div >";
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
    return "\"" + dateTime.split("T")[0] + "\"";
}

