
function EditData(ID, Controller, Action) {
    $.ajax({
        type: "POST", 
        url: "/" + Controller + "/" + Action,
        data: { UniqueID: ID },
        success: function (data) {
            console.log(data);

            if (data.length > 0) {
                OnSuccessOfEdit(data);
                // Assuming response contains data like: response.data.PropertyName
                //$('#txtName').val(response.data.Name);
                //$('#txtDescription').val(response.data.Description);
                //// Populate other fields as needed
                //$('#hdnId').val(ID); // Store ID for update
                //$('#modalEdit').modal('show'); // Show modal if editing in a modal
            } else {
                //alert("Error: " + response.message);
            }
        },
        error: function (xhr, status, error) {
           
            //console.error("Error fetching data for Edit:", error);
            alert("An error occurred while fetching data.");
        }
    });
}

function DeleteData() {

}
