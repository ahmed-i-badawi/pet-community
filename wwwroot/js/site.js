


var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
connection.on("ResieveMessage", function (fromUser, message) {

    var msg = fromUser + ":   " + message;
    var span1 = document.createElement("span");
    span1.textContent = fromUser;
    var span2 = document.createElement("p");
    var span3 = document.createElement("span");
    span2.textContent = message;

    
    span2.style.backgroundColor = "#f8f8f8";
    span2.style.borderRadius = "5px";
    span2.style.padding = "5px";
    span2.style.marginTop = "5px";
    span2.style.color = "green";


    span1.style.color = "blue";
    span1.style.fontFamily = "tahoma";
    span1.style.fontSize = "13px";
    var today = new Date();
    var hours = today.getHours().toString();
    var minutes = today.getMinutes().toString();
    var seconds = today.getSeconds().toString();

    var time = `      ${hours} :${minutes} :${seconds}`
    
    span3.append(time);
    span3.style.float = "right";
    span3.style.color = "grey";
    span3.style.fontSize = "10px";
    span2.append(span3);
    $("#list").append(span1);
    $("#list").append(span2);
});


connection.start();

$("#btnSend").on("click", function () {
    if ($("#txtMessage").val() == "") {
        var fromUser = $("#txtUser").val();
        var message = ".";
    } else {
        var fromUser = $("#txtUser").val();
        var message = $("#txtMessage").val();
    }
    connection.invoke("SendMessage", fromUser, message);
});



//$("#btnSend").on("click", function () {
//    if ($("#txtMessage").val() == "") {
//        var fromUser = $("#senderInput").val();
//        var message =".";
//    } else {
//        var fromUser = $("#senderInput").val();
//        var message = $("#txtMessage").val();
//    }

//    connection.invoke("SendMessage", fromUser, message);
//});















//document.getElementById("btnSend").disabled = true;

//connection.start().then(function () {
//    document.getElementById("btnSend").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});



//$("#btnSend").on("click", function () {
//    var sender = document.getElementById("senderInput").value;
//    var receiver = document.getElementById("receiverInput").value;
//    var message = document.getElementById("txtMessage").value;

//    if (receiver != "") {

//        connection.invoke("SendMessageToGroup", sender, receiver, message).catch(function (err) {
//            return console.error(err.toString());
//        });
//    }
//    else {
//        connection.invoke("SendMessage", sender, message).catch(function (err) {
//            return console.error(err.toString());
//        });
//    }

//});


//===================== Reviews ======================================
