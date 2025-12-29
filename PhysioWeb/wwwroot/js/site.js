
function EditData(ID, Controller, Action) {
    $.ajax({
        type: "POST", 
        url: "/" + Controller + "/" + Action,
        data: { UniqueID: ID },
        success: function (data) {
           
            if (data != "") {
                OnSuccessOfEdit(data);
                
            } else {
            }
        },
        error: function (xhr, status, error) {
           
            //console.error("Error fetching data for Edit:", error);
            alert("An error occurred while fetching data.");
        }
    });
}

function DeleteData(ID, Controller, Action) {
    
    $.ajax({
        type: "POST",
        url: "/" + Controller + "/" + Action,
        data: { UniqueID: ID },
        success: function (response) {
            if (response) {
               // notyf.success("Property Type Deleted");
                OnSuccessOfDelete();
                $("#TableList").DataTable().ajax.reload(null, false);
            } else {
                notyf.error(response.message || "Failed to delete.");
            }
        },
        error: function (xhr) {
            console.error(xhr);
            notyf.error("Server error: " + (xhr.responseText || "Unknown error"));
        }
    });
}

function populateDropdown(selector, items) {
    let $dropdown = $(selector);
    $dropdown.empty();
    $.each(items, function (index, item) {
        $dropdown.append($('<option>', {
            value: item.Value,
            text: item.Text
        }));
    });
}

function validateMobileNo(MobNo) {
    var mobileRegex = /^[6-9]\d{9}$/;
    var value = $(MobNo).val().trim();  

    if (!mobileRegex.test(value)) {
        notyf.error("Please enter a valid mobile number");
        $(MobNo).val("");   
        $(MobNo).focus();   
        return false;
    }
    return true;
}

function validateEmailID(emailid) {
    var email = $(emailid).val().trim();

    const emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;
    if (!emailRegex.test(email)) {
        notyf.error("Please enter a valid email address");
        $(emailid).val("");
        $(emailid).focus();
    }
}

function validateWesiteUrl(weburl) {
   
    var url = $(weburl).val().trim();
    var pattern = /^(https?:\/\/)?([\w\-]+\.)+[\w]{2,}(\/.*)?$/;

    if (url === "") {
        notyf.error("Website URL is required.");
    } else if (!pattern.test(url)) {
        notyf.error("Invalid website URL. Example: https://example.com");
        $(weburl).val(""); // clear the input if invalid
    }
   
}

function validatePinCode(pin) {
   
    var pincode = $(pin).val().trim();

    var pattern = /^[1-9][0-9]{5}$/;

    if (pincode === "") {
        notyf.error("Pincode is required.");
    } else if (!pattern.test(pincode)) {
        notyf.error("Invalid Pincode. Example: 110001");
        $(pin).val("");
    }
}

function validateReraRegNo(rerano) {
    var rera = $(rerano).val().trim();

    var pattern = /^[A-Za-z0-9\/\-]{5,20}$/;

    if (rera === "") {
        notyf.error("RERA Registration No is required.");
    } else if (!pattern.test(rera)) {
        notyf.error("Invalid RERA Registration No. Example: MAHARERA/PN12345/2025");
        $(rerano).val("");
    }
}

function validatepancard(panno) {
    var pan = $(panno).val().trim().toUpperCase();

    var pattern = /^[A-Z]{5}[0-9]{4}[A-Z]{1}$/;

    if (pan === "") {
        notyf.error("PAN is required.");
    } else if (!pattern.test(pan)) {
        notyf.error("Invalid PAN. Example: ABCDE1234F");
        $(panno).val("");
    } else {
        $(panno).val(pan); 
    }
}

function validateGst(gstno) {
    var gst = $(gstno).val().trim().toUpperCase();

    var pattern = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$/;

    if (gst === "") {
        notyf.error("GST Number is required.");
    } else if (!pattern.test(gst)) {
        notyf.error("Invalid GST Number. Example: 22AAAAA0000A1Z5");
        $(gstno).val("");
    } else {
        $(gstno).val(gst);
    }
}

function validatePassword(password) {
    var password = $(password).val();

    // ✅ At least 8 chars, 1 uppercase, 1 lowercase, 1 number, 1 special char
    var pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;

    if (password === "") {
        notyf.error("Password is required.");
    } else if (!pattern.test(password)) {
        notyf.error("Invalid Password. Must contain:\n- Min 8 characters\n- Uppercase & lowercase letters\n- At least 1 number\n- At least 1 special character");
        $(password).val("");
    }
}

$('.alphabetic-only').on('input', function () {
    this.value = this.value.replace(/[^a-zA-Z]/g, '');
});