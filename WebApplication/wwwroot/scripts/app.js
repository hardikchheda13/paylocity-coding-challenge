$(document).ready(() => {
    $(".submitEmployerID").click(submitEmployerIDHandler);
    $("#addEmployeeButton").click(addEmployeeButtonHandler);
    $("#submitEmployeeButton").click(submitEmployeeHandler);
    ;
    $("#addDependentDiv").click(addDependentClickHandler);
});
function submitEmployerIDHandler() {
    const employerID = parseInt($(".employerID").first().val().toString());
    validateEmployerID(employerID);
    $.getJSON(`api/employerCost/${employerID}`).done(result => {
        displayResult(result);
    });
}
function displayResult(result) {
    $(".resultDisplay").text(`Total annual cost for all your employees is ${result.TotalCost} `);
    $(".resultDisplay").removeClass("d-none");
}
function addEmployeeButtonHandler() {
    const employerID = parseInt($(".employerID").first().val().toString());
    $(".newEmployee .employeeDetails").removeClass("d-none");
    $("#addEmployeeButton").addClass("d-none");
    $("#submitEmployeeButton").removeClass("d-none");
    $("#addDependentDiv").removeClass("d-none");
}
function validateEmployerID(employerID) {
    if (!employerID || isNaN(employerID)) {
        alert("Enter valid employer ID");
        return;
    }
}
function addDependentClickHandler() {
    const dependentHTML = `
                        <div class="row dependentDetails">
                    <div class="col firstNameDiv">
                        <label>Enter Dependent First Name</label>
                        <input type="text" class="w-auto firstName">
                    </div>
                    <div class="col lastNameDiv">
                        <label>Enter Dependent Last Name</label>
                        <input type="text" class="w-auto lastName">
                    </div>
                </div>
    `;
    $(".dependents").append(dependentHTML);
}
function submitEmployeeHandler() {
    const employerID = parseInt($(".employerID").first().val().toString());
    validateEmployerID(employerID);
    let addEmployeeRequest;
    let dependents = [];
    const employeeFirstName = $(".newEmployee .employeeDetails .firstName").first().val().toString();
    if (!employeeFirstName) {
        alert("Enter Employee First Name");
        return;
    }
    const employeeLastName = $(".newEmployee .employeeDetails .lastName").first().val().toString();
    if (!employeeLastName) {
        alert("Enter Employee Last Name");
        return;
    }
    $(".dependentDetails").each((index, item) => {
        let dependentFirstName = $(item).find('input.firstName').val().toString();
        let dependentLastName = $(item).find('input.lastName').val().toString();
        if (!dependentFirstName && !dependentLastName) {
            alert("Enter Dependents' details");
            return;
        }
        dependents.push(new Beneficiary(dependentFirstName, dependentLastName));
    });
    addEmployeeRequest = new AddEmployeeRequest(employeeFirstName, employeeLastName, employerID, dependents);
    $.ajax({
        url: 'api/employee',
        type: "POST",
        data: JSON.stringify(addEmployeeRequest),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
    }).done(s => {
        displayResult(s);
    }).fail(s => {
        console.log(s);
    });
}
class Beneficiary {
    constructor(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}
class AddEmployeeRequest extends Beneficiary {
    constructor(firstName, lastName, employerID, dependents) {
        super(firstName, lastName);
        this.dependents = dependents;
        this.employerID = employerID;
    }
}
//# sourceMappingURL=app.js.map