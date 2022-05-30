$(document).ready(function () {
    //Supplier Registration Function
    $(".btn_supplierregister").click(function () {     
        //Set data to be sent
        var data = {
            "VendorName": $("#supplierbusinessname").val(),
            "Phonenumber": $("#phonenumber").val(),
            "Email": $("#supplieremailaddress").val(),
            "KraPin": $("#taxregistration").val(),
            "ContactName": $("#contactperson").val(),
            //"tterms": $("invalidCheck[type='checkbox']").val()
        }
        //Swal Message
        Swal.fire({
            title: "Confirm Registration?",
            text: "Are you sure you would like to proceed with submission?",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: true,
            confirmButtonText: "Yes, Proceed!",
            confirmButtonClass: "btn-success",
            confirmButtonColor: "#008000",
            position: "center"
           
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: "/Home/SupplierFirstRegistration",
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json",
                    cache: false,
                    processData: false
                }).done(function (status) {
                    var registerstatus = status.split('*');
                    status = registerstatus[0];
                    switch (status) {
                        case "success":
                            Swal.fire
                            ({
                                title: "Registration Submitted!",
                                text: registerstatus[1],
                                type: "success"
                            }).then(() => {
                                $("#feedback").css("display", "block");
                                $("#feedback").css("color", "green");
                                $('#feedback').attr("class", "alert alert-success");
                                $("#feedback").html("Your Account Creation Request have been successfully submitted.Kindly Check your Email Account for More Details!" + "  " + registerstatus[1]);
                                $("#uploadsMsg").css("display", "block");
                                $("#uploadsMsg").css("color", "green");
                                $("#uploadsMsg").html("Your Account Creation Request have been successfully submitted.Kindly Check your Email Account for More Details!" + "  " + registerstatus[1]);
                                $("#preqappl_form").reset();
                                windows
                            });
                            break;
                        default:
                            Swal.fire
                            ({
                                title: "Registration Error!!!",
                                text: "Your Account Creation Request could not been successfully submitted" +" "+ registerstatus[1],
                                type: "error"
                            }).then(() => {
                                $("#feedback").css("display", "block");
                                $("#feedback").css("color", "red");
                                $('#feedback').addClass('alert alert-danger');
                                $("#feedback").html("Your Account Creation Request could not been successfully submitted"+" "+registerstatus[1]);
                            });
                            break;
                    }
                }
                );
            } else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Registration Cancelled',
                    'You cancelled your supplier registration submission details!',
                    'error'
                );
            }
        });

    });
})