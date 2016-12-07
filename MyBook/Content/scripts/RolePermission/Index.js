var UpdateRolePermissionsUrl = "@Model.UpdateRolePermissionsUrl";
var GetRolePermissionsUrl = "@Model.GetRolePermissionsUrl";
$(function () {
    $(".save").click(function () {
        var roleID = RolesGrid.GetRowKey(RolesGrid.GetFocusedRowIndex());
        var permissions = PermissionsTree.GetVisibleSelectedNodeKeys();

        console.log(roleID);
        console.log(permissions);

        $.ajax({
            type: "POST",
            url: updateRolePermissionsUrl,
            data: {
                RoleID: roleID,
                Permissions: permissions
            },
            dataType: "json",
            success: function (response) {
                if (response.IsSuccess) {

                } else {
                    alert("Error");
                }
            }
        });

        return false;
    });
});

function OnGridViewFocusedRowChanged(s, e) {
    OnGetRowKey(RolesGrid.GetRowKey(RolesGrid.GetFocusedRowIndex()));
}

function OnGetRowKey(ID) {
    $.ajax({
        type: "POST",
        url: getRolePermissionsUrl,
        data: {
            ID: ID
        },
        dataType: "json",
        success: function (response) {
            if (response.IsSuccess) {
                var nodes = PermissionsTree.GetVisibleNodeKeys();

                $.each(nodes, function (index, node) {
                    PermissionsTree.SelectNode(node, false);
                });

                $.each(response.Data.Permissions, function (index, permission) {
                    PermissionsTree.SelectNode(permission.ID, true);
                });

            } else {
                alert("Error");
            }
        }
    });
}