$(document).ready(function () {
    SearchText();
});
function SearchText() {
    $("#txtEmpName").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Default.aspx/GetEmployeeName",
                data: "{'empName':'" + document.getElementById('txtEmpName').value + "'}",
                dataType: "json",
                success: function (data) {
                    response(data.d);
                },
                error: function (result) {
                    alert("No Match");
                }
            });
        }
    });
}  