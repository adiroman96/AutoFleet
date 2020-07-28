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
                response.json().then(function (data) {
                    registrationNumberInput.value = data.carRegistrationNumber;
                    manufacturingYearInput.value = data.carManufacturingYear;
                    prepareDriverDropdown(data.idDriver, data.driverName, data.driverEmail);
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