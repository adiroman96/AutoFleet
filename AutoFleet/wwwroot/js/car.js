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
    let insurancesTbl = document.getElementById("insuracesTable");

    for (var i = 0; i < insurances.length; i++) {
        console.log(insurances[i].typeOfInsurance + " " + insurances[i].lastRenewal + " " + insurances[i].expirationDate);

        var element = document.createElement("div");
        element.innerHTML = createLeftCell(insurances[i].typeOfInsurance, insurances[i].lastRenewal).toString().trim();

        insurancesTbl.appendChild(element)
    }
}

function createLeftCell(typeOfInsurance, lastRenewal) {
    return  "<div class=\"col-md-6\">" +
                "<div class=\"form-group\">" +
        "<label class=\"control-label\">Ultimul " + typeOfInsurance + "</label>" +
        "<input id=\"" + typeOfInsurance + "LastRenewalInput\" class=\"form-control\" value=" + lastRenewal + "/>" +
                    "<span class=\"text-danger\"></span>" +
            "</div >" +
            "</div >";    
}


