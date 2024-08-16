window.onload = onLoad;

function onLoad() {
    //get model object from the html dom
    var dataFromView = document.getElementById("hiddenDiv").getAttribute('value');;
    const obj = JSON.parse(dataFromView);
    //variables
    const form = document.getElementById("updateTeacherForm");
    const fName = document.getElementById("teacherfname");
    const lName = document.getElementById("teacherlname");
    const employeeNum = document.getElementById("employeenumber");
    const teacherHireDate = document.getElementById("teacherHireDate");
    const salary = document.getElementById("salary");
    const errorMsg = document.getElementById("noChangeMessage");

    //get old hire date data
    const oldHireDate = new Date(parseInt(obj.HireDate.split("(").pop().split(')')[0]));
    const year = oldHireDate.getUTCFullYear();
    let month = oldHireDate.getUTCMonth() + 1;
    let day = oldHireDate.getUTCDate();

    if (month < 10) {
        month = `0${month}`;
    };
    if (day < 10) {
        day = `0${day}`;
    };

    const oldDateString = year + "-" + month + "-" + day;
   
    //on submit check if a change has been proposed; if not prompt the user to make a change or head back
    function onSubmit() {
        let formChange = false;
        if (obj.FirstName !== fName.value ||
            obj.LastName !== lName.value ||
            obj.EmployeeNumber !== employeeNum.value ||
            oldDateString !== teacherHireDate.value ||
            obj.Salary.toFixed(2) !== salary.value)
        {
            formChange = true;
        }
        //if no changes have been made show a error block and request the user to change something
        if (!formChange) {
            errorMsg.style.display = "block";
            return false;
        }
        e.preventDefault;
    }

    form.onsubmit = onSubmit;
}

