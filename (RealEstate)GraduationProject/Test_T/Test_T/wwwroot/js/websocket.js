var socket = new WebSocket("ws://localhost:5000/ws");

socket.onopen = function (event) {
    console.log("WebSocket connection opened.");
};

socket.onmessage = function (event) {
    var message = event.data;
    console.log("Received message from WebSocket: " + message);
    // Display received message in UI
    var messagesDiv = document.getElementById("messages");
    messagesDiv.innerHTML += "<p>" + message + "</p>";
};

socket.onerror = function (error) {
    console.error("WebSocket error: " + error);
};

socket.onclose = function (event) {
    console.log("WebSocket connection closed.");
};
