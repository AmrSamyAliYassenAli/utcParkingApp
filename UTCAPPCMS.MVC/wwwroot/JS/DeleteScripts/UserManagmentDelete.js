var _ID = 0;
function setID(_id) { _ID = _id; }

$(document).ready(function () {

    $('#btn_DeleteAction').click(function () {
        var _url = '/UserManagment/Delete/' + _ID;

        $.ajax({
            url: _url,
            type: "POST",
            data: {
                'id': _ID
            },
            success: function () {
                window.location.replace("/UserManagment/Index")
            },
        });

    });
});