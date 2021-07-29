(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');
ga('create', 'UA-83990101-1', 'auto');
ga('send', 'pageview');

$("[name='durationInDays']").prop("required", true);
$("[name='DaysCount']").prop("required", true);
$("[name='HoursCount']").prop("required", true);
$("[name='Price']").prop("required", true);

$(document).ready(function ()
{
    $('btn_EditSubDur').click(
        // loop on tbody and set td values from Durationlist
    );
   
    if ($('#SubDurationTableBody').children().length === 0) {
        $('#SubDurationTable').hide();
    }

    $('#btn_Save').click(function () {
        // #HiddenSubID #HiddenSubDurationID
        // .txt_DurationDays .txt_DayCount .txt_HourseCount .txt_VechicleCount .txt_Price .txt_PriceMulti #chbx_AllDays #chbx_AllTime #chbx_IsMullti
        var Durationlist = [];
        var model = {};
        var _AllDays = false;
        var _AllTime = false;
        var _IsMullti = false;
        
        $('tr').each(function (item) {

        });
        var SubscriptionId = $('#HiddenSubID').val();
        var tablebody = $('#DurationStaticTable tbody');

        var ItemIDArray = new Array();
        $('#SubDurationTableBody .itemId').each(function () {
            ItemIDArray.push(this.value);
        });

        var durationInDaysArray = new Array();
        $('.txt_DurationDays').each(function () {
            durationInDaysArray.push(this.value);
        });
        var DayCountArray = new Array();
        $('.txt_DayCount').each(function () {
            DayCountArray.push(this.value);
        });
        var HourseCountArray = new Array();
        $('.txt_HourseCount').each(function () {
            HourseCountArray.push(this.value);
        });
        var VechicleCountArray = new Array();
        $('.txt_VechicleCount').each(function () {
            VechicleCountArray.push(this.value);
        });
        var PriceArray = new Array();
        $('.txt_Price').each(function () {
            PriceArray.push(this.value);
        });
        var PriceMultiArray = new Array();
        $('.txt_PriceMulti').each(function () {
            PriceMultiArray.push(this.value);
        });   

        var i = 0;
        tablebody.find('tr').each(function (i, el) {
            
            if ($('#chbx_AllDays').is(":checked")) {
                _AllDays = true;
            }
            if ($('#chbx_AllTime').is(":checked")) {
                _AllTime = true;
            }
            if ($('#chbx_IsMullti').is(":checked")) {
                _IsMullti = true;
            }
            
            var $tds = $(this).find('td');
            if (SubscriptionId == "") { //Add
                Durationlist.push({                    
                    durationInDays: $tds.eq(0).text(),
                    DaysCount: $tds.eq(1).text(),
                    HoursCount: $tds.eq(2).text(),
                    VechicleCount: $tds.eq(3).text(),
                    Price: $tds.eq(4).text(),
                    PriceMulti: $tds.eq(5).text(),
                    AllDays: $tds.eq(6).text(),
                    AllTime: $tds.eq(7).text(),
                    IsMullti: $tds.eq(8).text()
                }); 
                model = {
                    Name: $('#Input_Name').val(),
                    Image: "IMG",
                    IsEnable: $('#Input_IsEnable').val(),
                    Description: $('#Input_Description').val(),
                    parkingId: $('#Input_ParkingId').val(),
                    DurationList: Durationlist
                };
            }
            
            if (SubscriptionId != "") { //Edit
                Durationlist.push({
                    Id: ItemIDArray[i],
                    durationInDays: durationInDaysArray[i],
                    DaysCount: DayCountArray[i],
                    HoursCount: HourseCountArray[i],
                    VechicleCount: VechicleCountArray[i],
                    Price: PriceArray[i],
                    PriceMulti: PriceMultiArray[i],
                    AllDays: _AllDays,
                    AllTime: _AllTime,
                    IsMullti: _IsMullti
                });
                model = {
                    Id: SubscriptionId,
                    Name: $('#Input_Name').val(),
                    Image: "IMG",
                    IsEnable: $('#Input_IsEnable').val(),
                    Description: $('#Input_Description').val(),
                    parkingId: $('#Input_ParkingId').val(),
                    DurationList: Durationlist
                };
            }
            i++;
        });

       
        //var data = new FormData();
        //var files = $("#Input_Img").get(0).files;
        //if (files.length > 0) {
        //    console.log("File[0] : "+files[0]);
        //   // data.append("myfile", files[0]);
        //}
       
       

        console.log(JSON.stringify(model));

        /*
        console.log("File : " + files);
        console.log("Data : " + data);*/

        $.ajax({
            type: "POST",
            url: '/Subscription/Createnew',
            data: model,

            success: function (json) { alert(json); },
            error: function (json) {
                alert("try again");
                alert(JSON.stringify(json));
            }
        });
        // window.location.replace("/Subscription/Index")
    });
   
    $('#btn_SavePopUpModal').click(function (e) {
        e.preventDefault()

        var _AllDays = false;
        var _AllTime = false;
        var _IsMullti = false;

        if ($('#chbx_AllDays').is(":checked")) {
            _AllDays = true;
        }
        if ($('#chbx_AllTime').is(":checked")) {
            _AllTime = true;
        }
        if ($('#chbx_IsMullti').is(":checked")) {
            _IsMullti = true;
        }

        var SubDuraion = {
            durationInDays : $("input[name='durationInDays']").val(),
            DaysCount: $("input[name='DaysCount']").val(),
            HoursCount : $("input[name='HoursCount']").val(),
            VechicleCount : $("input[name='VechicleCount']").val(),
            Price : $("input[name='Price']").val(),
            PriceMulti : $("input[name='PriceMulti']").val(),
            AllDays: _AllDays,
            AllTime: _AllTime,
            IsMullti: _IsMullti
        };

        $('modalGridSystemLg').hide();

        $('#SubDurationTable').show();

        $('#SubDurationTableBody').append(
            "<tr data-durationInDays='" + SubDuraion.durationInDays +
            "' data-DaysCount='" + SubDuraion.DaysCount +
            "' data-HoursCount='" + SubDuraion.HoursCount +
            "' data-VechicleCount='" + SubDuraion.VechicleCount +
            "' data-Price='" + SubDuraion.Price +
            "' data-PriceMulti='" + SubDuraion.PriceMulti +
            "' data-AllDays='" + SubDuraion.AllDays +
            "' data-AllTime='" + SubDuraion.AllTime +
            "' data-IsMullti='" + SubDuraion.IsMullti +
            "'>" +
            "<td>" + SubDuraion.durationInDays + "</td>" +
            "<td>" + SubDuraion.DaysCount + "</td>" +
            "<td>" + SubDuraion.HoursCount + "</td>" +
            "<td>" + SubDuraion.VechicleCount + "</td>" +
            "<td>" + SubDuraion.Price + "</td>" +
            "<td>" + SubDuraion.PriceMulti + "</td>" +
            "<td id='td_Alldays'>" + SubDuraion.AllDays + "</td>" +
            "<td id='td_AllTime'>" + SubDuraion.AllTime + "</td>" +
            "<td id='td_IsMulti'>" + SubDuraion.IsMullti + "</td>" +
            "<td>" +
            "<button class='btn btn-danger btn-lg btn-delete mr-3' type='button'>Delete</button></td>" +
            "<td><button class='btn btn-info btn-lg btn-edit' type='button'>Edit</button>" +
            "</td></tr>");

        $("input[name='durationInDays']").val("");
        $("input[name='DaysCount']").val("");
        $("input[name='HoursCount']").val("");
        $("input[name='VechicleCount']").val("");
        $("input[name='Price']").val("");
        $("input[name='PriceMulti']").val("");
        $("input[name='AllDays']").val("");
        $("input[name='AllTime']").val("");
        $("input[name='IsMullti']").val("");
    });

    $('body').on('click', '.btn-delete', function () {
        $(this).parents('tr').remove();

        if ($('#SubDurationTableBody').children().length === 0) {
            $('#SubDurationTable').hide();
        }
    });

    $('body').on('click', '.btn-edit', function () {

        var durationInDays = $(this).parents('tr').attr('data-durationInDays');
        var DaysCount = $(this).parents('tr').attr('data-DaysCount');
        var HoursCount = $(this).parents('tr').attr('data-HoursCount');
        var VechicleCount = $(this).parents('tr').attr('data-VechicleCount');
        var Price = $(this).parents('tr').attr('data-Price');
        var PriceMulti = $(this).parents('tr').attr('data-PriceMulti');
        var AllDays = $('#td_Alldays').val();//$(this).parents('tr').attr('data-AllDays');
        var AllTime = $('#td_AllTime').val();//$(this).parents('tr').attr('data-AllTime');
        var IsMullti = $('#td_IsMulti').val();//$(this).parents('tr').attr('data-IsMullti');

        $(this).parents('tr').find('td:eq(0)').html("<input name='edit_durationInDays' id='Edit_input_durationInDays' value='" + durationInDays + "'>");
        $(this).parents('tr').find('td:eq(1)').html("<input name='edit_DaysCount' id='Edit_input_DaysCount' value='" + DaysCount + "'>");
        $(this).parents('tr').find('td:eq(2)').html("<input name='edit_HoursCount' id='Edit_input_HoursCount' value='" + HoursCount + "'>");
        $(this).parents('tr').find('td:eq(3)').html("<input name='edit_VechicleCount' id='Edit_input_VechicleCount' value='" + VechicleCount + "'>");
        $(this).parents('tr').find('td:eq(4)').html("<input name='edit_Price' id='Edit_input_Price' value='" + Price + "'>");
        $(this).parents('tr').find('td:eq(5)').html("<input name='edit_PriceMulti' id='Edit_input_PriceMulti' value='" + PriceMulti + "'>");
        // if AllDays == true
        if (AllDays == true) {
            $(this).parents('tr').find('td:eq(6)').html("<input name='edit_AllDays' type='checkbox' id='Edit_input_AllDays' checked='checked' value='" + AllDays + "'>");
        }
        else {
            $(this).parents('tr').find('td:eq(6)').html("<input name='edit_AllDays' type='checkbox' id='Edit_input_AllDays' value='" + AllDays + "'>");
        }
        //$(this).parents('tr').find('td:eq(6)').html("<input name='edit_AllDays' type='checkbox' id='Edit_input_AllDays' value='" + AllDays + "'>");
        $(this).parents('tr').find('td:eq(7)').html("<input name='edit_AllTime' type='checkbox' id='Edit_input_AllTime' value='" + AllTime + "'>");
        $(this).parents('tr').find('td:eq(8)').html("<input name='edit_IsMullti' type='checkbox' id='Edit_input_IsMullti' value='" + IsMullti + "'>");

        $('#Edit_input').css({
            'line-height': '0px',
            'table-layout': 'fixed',
            'overflow': 'hidden'
        });

        $('#Edit_input').parent().css({
            'line-height': '0px',
            'table-layout': 'fixed',
            'overflow': 'hidden'
        });

        $(this).parents('tr').find('td:eq(10)').prepend("<button type='button' class='btn btn-info btn-lg btn-update mr-3'>Update</button>");

        $(this).hide()
    });

    $('body').on('click', '.btn-update', function () {

        var durationInDays = $(this).parents('tr').find("input[name='edit_durationInDays']").val();
        var DaysCount = $(this).parents('tr').find("input[name='edit_DaysCount']").val();
        var HoursCount = $(this).parents('tr').find("input[name='edit_HoursCount']").val();
        var VechicleCount = $(this).parents('tr').find("input[name='edit_VechicleCount']").val();
        var Price = $(this).parents('tr').find("input[name='edit_Price']").val();
        var PriceMulti = $(this).parents('tr').find("input[name='edit_PriceMulti']").val();
        var AllDays = $(this).parents('tr').find("input[name='edit_AllDays']").val();
        var AllTime = $(this).parents('tr').find("input[name='edit_AllTime']").val();
        var IsMullti = $(this).parents('tr').find("input[name='edit_IsMullti']").val();

        $(this).parents('tr').find('td:eq(0)').text(durationInDays);
        $(this).parents('tr').find('td:eq(1)').text(DaysCount);
        $(this).parents('tr').find('td:eq(2)').text(HoursCount);
        $(this).parents('tr').find('td:eq(3)').text(VechicleCount);
        $(this).parents('tr').find('td:eq(4)').text(Price);
        $(this).parents('tr').find('td:eq(5)').text(PriceMulti);
        $(this).parents('tr').find('td:eq(6)').text(AllDays);
        $(this).parents('tr').find('td:eq(7)').text(AllTime);
        $(this).parents('tr').find('td:eq(8)').text(IsMullti);

        $(this).parents('tr').attr('data-durationInDays', durationInDays);
        $(this).parents('tr').attr('data-DaysCount', DaysCount);
        $(this).parents('tr').attr('data-HoursCount', HoursCount);
        $(this).parents('tr').attr('data-VechicleCount', VechicleCount);
        $(this).parents('tr').attr('data-Price', Price);
        $(this).parents('tr').attr('data-PriceMulti', PriceMulti);
        $(this).parents('tr').attr('data-AllDays', AllDays);
        $(this).parents('tr').attr('data-AllTime', AllTime);
        $(this).parents('tr').attr('data-IsMullti', IsMullti);

        $(this).parents('tr').find('.btn-edit').show();
        $(this).parents('tr').find('.btn-update').remove();
    });
});

/*var htmlString = $(form).html();
$('.output').text(htmlString);*/
/* if ($('#SubDurationTableBody tr td').hasClass("dataTables_empty")) {
        $('#SubDurationTable').hide();
    }
    //Check if form has required fields
    $('form').find('input').each(function () {
        if (!$(this).prop('required')) {
            console.log("NR");
        } else {
            console.log("IR");
        }
    });
    $("input[type='button']").click(function () {
        if ($(this).hasClass("disable"))
            return false;

        if ($(this).hasClass("validate")) {
            var errors = [];
            // all required input that need validation
            var input = $(this).parent().find("input[type='text'][required='required']");
            input.each(function () {
                var vType = $(this).attr("validationType");
                var value = $(this).val();
                var fName = $(this).attr("placeholder");
                switch (vType) {
                    case "notEmpty":
                        if (!value || value == "")
                            errors.push(fName + " cant be empty");
                        break;
                }
            });
            if (errors.length > 0) {
                $(this).parent().find(".submit").addClass("disable");
                alert(errors)
            }
            else {
                $(this).parent().find(".submit").removeClass("disable");
            }

        } else return true; // submit the form

    });*/