//create an onload fxn that listens for a form submit
window.onload = onLoad;

function onLoad() {
    //variables from the HTML document
    const form = document.getElementById("newTeacherForm");
    const errorMsg = document.getElementById("formErrorMsg");
    const fName = document.getElementById("teacherfname");
    const lName = document.getElementById("teacherlname");
    const employeeNum = document.getElementById("employeenumber");
    const salary = document.getElementById("salary");

    function onSubmit() {
        //value variables
        let fNameVal = fName.value;
        let lNameVal = lName.value;
        let employeeNumVal = employeeNum.value;
        let salaryVal = salary.value;

        //regex expression
        var reg1 = RegExp(/^T\d{3}$/gm);

        //temp var to check if form is valid
        let validForm = true;
        let fNameValidator = false;
        let lNameValidator = false;
        let empNumValidator = false;
        let salValidator = false;

        //conditionals to check if each input is valid
        if (fNameVal === "") {
            fName.style.backgroundColor = "red";
            fName.focus();
            fNameValidator = false;
        } else {
            fName.style.backgroundColor = "white";
            fNameValidator = true;
        }

        if (lNameVal === "") {
            lName.style.backgroundColor = "red";
            lName.focus();
            lNameValidator = false;
        } else {
            lName.style.backgroundColor = "white";
            lNameValidator = true;
        }

        if ((reg1.test(employeeNumVal)) === false) {
            employeeNum.style.backgroundColor = "red";
            employeeNum.focus();
            empNumValidator = false;
        } else {
            employeeNum.style.backgroundColor = "white";
            empNumValidator = true;
        }

        if (salaryVal === "") {
            salary.style.backgroundColor = "red";
            salary.focus();
            salValidator = false;
        } else {
            salary.style.backgroundColor = "white";
            salValidator = true;
        }

        //check if every input field is valid before sending to sql
        if (fNameValidator && lNameValidator && empNumValidator && salValidator) {
            validForm = true;
        } else {
            validForm = false;
        }

        //display an error message within the document if there is a problem
        if (!validForm) {
            errorMsg.style.display = "block";
        } else {
            errorMsg.style.display = "none";
        }

        //prevent the form being sent unless it is accurate
        event.preventDefault;
        return validForm;
    }

    form.onsubmit = onSubmit;
}