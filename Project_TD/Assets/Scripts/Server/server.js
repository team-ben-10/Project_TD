var net = require('net');

var allClients = [];

var server = net.createServer(function(socket) {
	socket.pipe(socket);
});

server.listen(35555, '127.0.0.1');

console.log("Server running!");

server.on("connection", function (socket) {
    console.log("New Client " + socket.address().address);
    allClients.push(socket);
    socket.on("close", function () {
        allClients = allClients.filter(function (s) {
            return s.address().address != socket.address().address;
        });
        console.log("Client disconnected! " + allClients.length);
    });
    socket.on("data", function (data) {
        var s = data.toString("UTF8");
        console.log(s);
        var command = s.split(" ");
        if (command[0] == "$SendBlocker") {
            allClients.forEach(function (s) {
                if (s.address().address != socket.address().address) {
                    s.write("%RecieveBlocker " + command[1]);
                }
                else {
                    s.write("%SentBlocker " + command[1]);
                }
            });
        }
    });
});

