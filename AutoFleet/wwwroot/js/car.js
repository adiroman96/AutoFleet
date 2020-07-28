const registrationNumberInput = document.getElementById("registrationNumberInput");
const manufacturingYearInput = document.getElementById("manufacturingYearInput");
const driverNameInput = document.getElementById("driverNameInput");
const carIdInput = document.getElementById("Id");
const driverDropdown = document.getElementById("changeDriverDropdown");
const lastITPInput = document.getElementById("lastITPInput");
const expirationDateITPInput = document.getElementById("expirationDateITPInput");
const defaultOptionForDriverDropdown = 'Alege noul sofer';

$(document).ready(function () {
    fillFields();
    getDrivers(); // populate select with all drivers
});


function fillFields() {
    fetch('/cars/cardto/' + carId)
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
                    driverNameInput.value = data.driverName;
                });
            }
        )
        .catch(function (err) {
            console.log('Fetch Error :-S', err); // ToDo: make it give a nice error on the page
        });
}

const getDrivers = async () => {

    driverDropdown.length = 0;

    let defaultOption = document.createElement('option');
    defaultOption.text = defaultOptionForDriverDropdown;

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
                        option = document.createElement('option');
                        option.title = data[i].name + " " + data[i].email;
                        option.text = data[i].name;
                        option.value = data[i].id;
                        driverDropdown.add(option);
                    }
                });
            }
        )
        .catch(function (err) {
            console.log('Fetch Error :-S', err); // ToDo: make it give a nice error on the page
        });
}

driverDropdown.onchange = function () {
    setDriver(driverDropdown.value);
}

const setDriver = async (id) => {
    if (id == defaultOptionForDriverDropdown)
        return;

    fetch('/drivers/getdriver/' + id)
        .then(
            function (response) {
                if (response.status !== 200) {// ToDo: make it give a nice error on the page
                    console.log('Looks like there was a problem. Status Code: ' +
                        response.status);
                    return;
                }

                // Examine the text in the response
                response.json().then(function (driver) {
                    console.log(driver.name);
                    driverDropdown.title = "asd";

                    let driverName = document.getElementById("driverNameInput");
                    driverName.value = driver.name;
                });
            }
        )
        .catch(function (err) {
            console.log('Fetch Error :-S', err); // ToDo: make it give a nice error on the page
        });
}