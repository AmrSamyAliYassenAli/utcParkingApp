var _ID = 0;
function setID(_id) { _ID= _id; }
var _contName = "";
function setControllerName(ContName) { _contName = ContName;}
$(document).ready(function () {

    $('#btn_DeleteAction').click(function () {
        var _url = '/SiteLine/Delete/' + _ID;
        var URL = '/' + _contName+'/Delete/' + _ID;
        alert("Id : " + _ID );
        alert("URL : " + _url);
        alert("NEW URL : " + URL);
        $.ajax({
            url: _url, 
            type: "POST",
            data: {
                'id': _ID
            },            
            success: function () { 
                window.location.replace("/SiteLine/Index")
            },
            //failure: 
        });

    });   
});