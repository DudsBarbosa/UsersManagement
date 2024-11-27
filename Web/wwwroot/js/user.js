// Users related functions and variables are defined here
function addAsync() {
    var user = $("#userForm").serialize();
    console.log(user);
    $.ajax({
        url: 'Create',
        type: 'POST',
        data: user
    });
}

function updateAsync() {
    var user = $("#userForm").serialize();
    console.log(user);
    $.ajax({
        url: 'Edit',
        type: 'POST',
        data: user
    });
}

function deleteAsync() {
    var user = $("#userForm").serialize();
    console.log(user);
    $.ajax({
        url: 'Delete',
        type: 'POST',
        data: user
    });
}