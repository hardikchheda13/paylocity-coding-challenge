$(document).ready(() => {
    $(".submitEmployerID").click(submitEmployerIDHandler);
    $("#addEmployeeButton").click(addEmployeeButtonHandler);
    $("#submitEmployeeButton").click(submitEmployeeHandler);;
    $("#addDependentDiv").click(addDependentClickHandler);

});

function submitEmployerIDHandler(eventObject: any) {
    const employerID: number = parseInt($(".employerID").first().val().toString());
    validateEmployerID(employerID);
    $.getJSON(`api/employerCost/${employerID}`).done(result => {
        displayResult(result);
    })
}

function displayResult(result: any) {
    $(".resultDisplay").text(`Total annual cost for all your employees is ${result.TotalCost} `)
    $(".resultDisplay").removeClass("d-none");
}

function addEmployeeButtonHandler(addEmployeeButtonHandler: any) {
    const employerID: number = parseInt($(".employerID").first().val().toString());

    $(".newEmployee .employeeDetails").removeClass("d-none");
    $("#addEmployeeButton").addClass("d-none");
    $("#submitEmployeeButton").removeClass("d-none");
    $("#addDependentDiv").removeClass("d-none");
}

function validateEmployerID(employerID: number): boolean
{
    if (!employerID || isNaN(employerID)) {
        alert("Enter valid valid employer ID");
        return;
    }
}

function addDependentClickHandler(addDependentClickHandler: any) {
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
    `
    $(".dependents").append(dependentHTML);
}

function submitEmployeeHandler() {
    const employerID: number = parseInt($(".employerID").first().val().toString());
    validateEmployerID(employerID);
    let addEmployeeRequest: AddEmployeeRequest

    let dependents: Array<Beneficiary> = [];
    const employeeFirstName: string = $(".newEmployee .employeeDetails .firstName").first().val().toString();
    if (!employeeFirstName) {
        alert("Enter Employee First Name");
        return;
    }
    const employeeLastName: string = $(".newEmployee .employeeDetails .lastName").first().val().toString();
    if (!employeeLastName) {
        alert("Enter Employee Last Name");
        return;
    }
    $(".dependentDetails").each((index, item) => {
        let dependentFirstName: string = $(item).find('input.firstName').val().toString();
        let dependentLastName: string = $(item).find('input.lastName').val().toString();
        if (!dependentFirstName && !dependentLastName) {
            alert("Enter all dependents value");
            return;
        }
        dependents.push(new Beneficiary(dependentFirstName, dependentLastName));
    })
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
    })
    
}

class Beneficiary {
    firstName: string;
    lastName: string;

    constructor(firstName: string, lastName: string) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}

class AddEmployeeRequest extends Beneficiary {
    employerID: number;
    dependents: Beneficiary[];

    constructor(firstName: string, lastName: string, employerID: number, dependents: Beneficiary[]) {
        super(firstName, lastName);
        this.dependents = dependents;
        this.employerID = employerID;
    }
}