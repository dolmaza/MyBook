$(function () {
    $("#add-new").click(function () {
        UserGrid.AddNewRow();
        return false;
    });
});

function OnIsActiveCheckBoxInit(s, e) {
    if (UserGrid.IsNewRowEditing()) {
        s.SetValue(false);
    }
}