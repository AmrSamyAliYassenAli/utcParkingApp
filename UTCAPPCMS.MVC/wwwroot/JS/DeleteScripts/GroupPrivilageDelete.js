var _ID = 0;
function setID(_id) { _ID = _id; }

var PopUpModel = `<div id="dangerModalAlert" tabindex="-1" role="dialog" class="modal fade"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button></div><div class="modal-body"><div class="text-center"><span class="text-danger icon icon-times-circle icon-5x"></span><h3 class="text-danger">Delete</h3><p>Are You Sure You want to delete this Record ?</p><div class="m-t-lg"><button class="btn btn-danger" data-dismiss="modal" type="button" id="btn_DeleteAction">OK</button><button class="btn btn-default" data-dismiss="modal" type="button">Cancel</button></div></div></div><div class="modal-footer"></div></div></div></div>`;

$(document).ready(function () {

    $('#body').html(PopUpModel);
   
    $('#btn_DeleteAction').click(function () {
        var _url = '/GroupPrivilage/Delete/' + _ID;

        $.ajax({
            url: _url,
            type: "POST",
            data: {
                'id': _ID
            },
            success: function () {
                window.location.replace("/GroupPrivilage/Index")
            },
        });

    });
});