//$(document).ready(function () {
//    $("#recharge-bill").submit(function (event) {
//        event.preventDefault();
//        var mobilenumber = parseInt($('#mobileNumber').val());
//        var provider = $('#provider').val();
//        var price = parseInt($('#amount').val());



//        $.ajax({
//            url: '/api/bill/new',
//            type: "POST",
//            dataType: "JSON",
//            data: {
//                MobileNumber: mobilenumber,
//                Provider: provider,
//                Price: price
//            },
//            success: function (response) {
//                window.location = 'bill.html'
//            },
//            error: function (error) {
//                console.log()
//                alert("There was an error");
//            }
//        });
//    });
//});

//V2
$(document).ready(function () {
    $("#recharge-bill").submit(function (event) {
        event.preventDefault();
        var mobilenumber = parseInt($('#mobileNumber').val());
        var provider = $('#provider').val();
        var price = parseInt($('#amount').val());
        var prepaidRadio = document.getElementById('prepaid');
        var prepost = !prepaidRadio.checked;

        $.ajax({
            url: '/api/bill/new',
            type: "POST",
            dataType: "JSON",
            data: {
                MobileNumber: mobilenumber,
                Provider: provider,
                Price: price,
                Prepost: prepost
            },
            success: function (response) {
                window.location = 'bill.html'
            },
            error: function (error) {
                console.log()
                alert("There was an error");
            }
        });
    });
});