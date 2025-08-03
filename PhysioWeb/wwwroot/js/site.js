
function EditData(ID, Controller, Action) {
    $.ajax({
        type: "POST", 
        url: "/" + Controller + "/" + Action,
        data: { UniqueID: ID },
        success: function (data) {
            console.log(data);

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
