$(document).ready(function () {
     //#Input_EnglishName    #Input_ArabicName    #Input_IsEnable
     //#chbx_View    #chbx_Add    #chbx_Edit   #chbx_Delete   #chbx_Search   #chbx_Print
     //#txt_hidden_FormPagesId  #txt_hidden_UserGroupId #txt_hidden_ParkingId
    //loop on tbody and get values to create object
    //Hidden Id's   #Edit_Hidden_Id     .itemId
    // #txt2_Image
    $('#btn_Save').click(function () {
        
            var _Id = $('#Edit_Hidden_Id').val();

            var Priv_tableData = new Array();
            var _View = false;
            var _Add = false;
            var _Edit = false;
            var _Delete = false;
            var _Search = false;
            var _Print = false;
            var _IsEnable = false;


            var ItemIDArray = new Array();
            $('#tableBody .itemId').each(function () {
                ItemIDArray.push(this.value);
            });

            var i = 0;
            $('#UserPrivList tbody').find('tr').each(function (i, el) {

                if ($('#chbx2_View').is(":checked")) {
                    _View = true;
                }
                if ($('#chbx2_Add').is(":checked")) {
                    _Add = true;
                }
                if ($('#chbx2_Edit').is(":checked")) {
                    _Edit = true;
                }
                if ($('#chbx2_Delete').is(":checked")) {
                    _Delete = true;
                }
                if ($('#chbx2_Search').is(":checked")) {
                    _Search = true;
                }
                if ($('#chbx2_Print').is(":checked")) {
                    _Print = true;
                }
                if ($('#txt2_IsEnable').is(":checked")) {
                    _IsEnable = true;
                }

                var $tds = $(this).find('td');
                if (_Id == "") {    // for Add
                    var FormPageUserPrivilage = {
                        formKey: $tds.eq(1).text(),
                        View: _View,
                        Add: _Add,
                        Edit: _Edit,
                        Delete: _Delete,
                        Search: _Search,
                        Print: _Print,
                    };
                }
                if (_Id != "") {    // for Edit
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

       // var file = $("#txt2_Image").val(); 
      //  var formData = new FormData();
       // var fileInput = $('#txt2_Image')[0];
        //var file = fileInput.files[0];
       // formData.append("file", file)




        var files = $('#txt2_Image').prop("files");
        //var url = "/UserManagment/img";//?handler=Input_IMG
        Input_IMG = new FormData();
        Input_IMG.append("Input_IMG", files[0]);         //formData
        Input_IMG.append("id", _Id );
        
            var model = {
                Id: _Id,
                Fullname: $('#txt2_Fullname').val(),
                Username: $('#txt2_Username').val(),
                Mobile: $('#txt2_Mobile').val(),
                Email: $('#txt2_Email').val(),
                Password: $('#confirmpassword').val(), //Password: $('#txt2_Password').val(),
                Image: $('#imgPath').val(),
                IsEnable: _IsEnable,
                UserTypeId: $('#txt2_UserTypeId').val(),
                PrivilageGroupId: $('#txt2_PrivilageGroupId').val(),
                Priv_tableData: Priv_tableData
        };

        
            console.log(JSON.stringify(model));

        $.ajax({
            type: "POST",
            url: "/UserManagment/img",//'/UserManagment/Save',
            data: Input_IMG, //{ Input_IMG: file}, //file   model 
            //dataType: 'json',

             contentType: false,
            processData: false,
            success: function (data) {
                if (data != null) {
                    model.Image = data;
                }
                
                
                
                $.ajax({
            type: "POST",
            url: '/UserManagment/Save',//'/UserManagment/Save',
            data: model, //{ Input_IMG: file}, //file   model 
            dataType: 'json',
            success: function (json) { alert(json); },
            error: function (json) {
                alert("try againcall2");
            }
            })
            },




                error: function () {
                    alert("tryvv againcall1");
                }
        });



        //$.ajax({
        //    type: "POST",
        //    url: '/UserManagment/Save',//'/UserManagment/Save',
        //    data: model, //{ Input_IMG: file}, //file   model 
        //    dataType: 'json',

            


        //    success: function (json) { alert(json); },
        //    error: function (json) {
        //        alert("try again");
        //    }
        //});

            
    });   
});