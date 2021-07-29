$(document).ready(function () {
    $('#btn_Save').click(function () {
        // #Input_EnglishName    #Input_ArabicName    #Input_IsEnable
        // #chbx_View    #chbx_Add    #chbx_Edit   #chbx_Delete   #chbx_Search   #chbx_Print
        // #txt_hidden_FormPagesId  #txt_hidden_UserGroupId #txt_hidden_ParkingId
        // loop on tbody and get values to create object
        var _Id = $('#Edit_Hidden_Id').val();

        
        var Priv_tableData = new Array();

        var _View = false;
        var _Add = false;
        var _Edit = false;
        var _Delete = false;
        var _Search = false;
        var _Print = false;
        var _IsEnable = false;
        var _Id = $('#Edit_Hidden_Id').val();

        console.log("_Id : " + _Id);

        alert("_Id : " + _Id);
        /*
        var _FormPageIdHidden = $('#txt_hidden_FormPagesId').val();
        var _UserGroupIdHidden = $('#txt_hidden_UserGroupId').val();
        var _ParkingIdHidden = $('#txt_hidden_ParkingId').val();
        */
       
        var ItemIDArray = new Array();
        $('#tableBody .itemId').each(function () {
            
            ItemIDArray.push(this.value);
            alert("this.value " + this.value);
        });

        var i = 0;
        $('#PrivList tbody').find('tr').each(function (i, el) {           

            alert("Id : " + ItemIDArray[i]);
            if ($('#chbx_View').is(":checked")) {
                _View = true;
            }
            if ($('#chbx_Add').is(":checked")) {
                _Add = true;
            }
            if ($('#chbx_Edit').is(":checked")) {
                _Edit = true;
            }
            if ($('#chbx_Delete').is(":checked")) {
                _Delete = true;
            }
            if ($('#chbx_Search').is(":checked")) {
                _Search = true;
            }
            if ($('#chbx_Print').is(":checked")) {
                _Print = true;
            }
            if ($('#Input_IsEnable').is(":checked")) {
                _IsEnable = true;
            }

            var $tds = $(this).find('td');
            //var _item_id = $('#HiddenItemId').val();
            if (_Id == "") {    // Add
                var FormPageUserPrivilage = {
                    //Id: HiddenId,
                    formKey: $tds.eq(1).text(),
                    View: _View,
                    Add: _Add,
                    Edit: _Edit,
                    Delete: _Delete,
                    Search: _Search,
                    Print: _Print,

                };
            }
           
            if (_Id != "") {    // Edit
                var FormPageUserPrivilage = {
                    Id: ItemIDArray[i],
                    formKey: $tds.eq(1).text(),
                    View: _View,
                    Add: _Add,
                    Edit: _Edit,
                    Delete: _Delete,
                    Search: _Search,
                    Print: _Print,

                };
            }
            Priv_tableData.push(FormPageUserPrivilage);
            i++;
        });
        
        var model = {
            Id: _Id,
            EnglishName: $('#Input_EnglishName').val(),
            ArabicName: $('#Input_ArabicName').val(),
            IsEnable: _IsEnable,
            Priv_tableData: Priv_tableData
        };
        console.log(JSON.stringify(model));
        $.ajax({
            type: "POST",
            url: '/GroupPrivilage/Save',
            data: model,

            success: function (json) { alert(json); },
            error: function (json) {

                alert("try again");
            }
        });

        /*if (_Id != "") { // for Edit
            $.ajax({
                type: "POST",
                url: '/GroupPrivilage/Edit',
                data: model, //use id here

                success: function (json) { alert(json); },
                error: function (json) {

                    alert("try again");
                }
            });
        }
        else {  // for Add
            $.ajax({
                type: "POST",
                url: '/GroupPrivilage/Save',
                data: model, 

                success: function (json) { alert(json); },
                error: function (json) {

                    alert("try again");
                }
            });
        }*/
       
    });
});