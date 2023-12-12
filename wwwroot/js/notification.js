"usestrict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();
connection.on("sendToUser", (articleHeading, articleContent) => {
    var heading = document.createElement("h3");
    heading.textContent = articleHeading;
    var p = document.createElement("p");
    p.innerText = articleContent;
    var span = document.createElement("span");
    span.innerText = "the User want to work as adoctor";
    var div = document.createElement("div");
    div.appendChild(heading);
    div.appendChild(p);
    div.appendChild(span);

    document.getElementById("articleList").appendChild(div);
    document.getElementById("articleList1").innerText = "1";

});
connection.start().catch(function (err) {
    return console.error(err.toString());
});

